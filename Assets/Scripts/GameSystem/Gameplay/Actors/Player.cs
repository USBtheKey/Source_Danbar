
using System;
using System.Collections;

using GameSystem.InputSystem;
using GameSystem.UI;
using GameSystem.Utility;
using GameSystem.GameSceneManagement;

using UnityEngine;
using UnityEngine.InputSystem;

namespace GameSystem
{
	[RequireComponent(typeof(HealthComponent))]
	public sealed class Player : MonoBehaviour, ISceneObject
	{
		private enum State { Focus, Normal, Dead };
		private State currentState = State.Normal;
		private Animator stateMachine;
		[SerializeField] private CounterIndicator ultCooldown;
		public HealthComponent Health { get; private set; }
		private SpriteRenderer[] playerRenderers;
		[SerializeField] private TrailRenderer[] trails;
		[SerializeField] private GameObject ultimateObj;
		[SerializeField] private PlayerHUD hud;
		[SerializeField] private LaserAimedMissileSystem laserSys;
		[SerializeField] private GunsSystem gunsSystem;

		private Vector2 direction;
		[SerializeField] private GameObject DeathVFX;

		[SerializeField] private float ultCDTime = 15f;
		[SerializeField] private float invincibleTime = 60f;
		[SerializeField] float flashDelay = 0.05f;
		[SerializeField] private float focusMultiplier = 0.2f;
		[SerializeField] private float speed = 500f;
		private float originalSpeed = 0f;
		private bool isMoving = false;
		private bool canUseUltimate = true;

		private Rigidbody2D rigid;
		private Collider2D coll;

		private WaitForSeconds waitForDestroyedCD;

		private bool isFlashing = false;

		private void Awake()
		{
			rigid = GetComponent<Rigidbody2D>();
			coll = GetComponentInChildren<Collider2D>(true);
			stateMachine = GetComponent<Animator>();
			playerRenderers = GetComponentsInChildren<SpriteRenderer>();
			Health = GetComponent<HealthComponent>();

			waitForDestroyedCD = new WaitForSeconds(2f);
		}

		private void OnEnable()
		{
			if (!LevelManager.Instance) return;

			coll.gameObject.layer = LayerMask.NameToLayer("Player");

			LevelManager.Instance.OnGameWon += OnWin;

			OnDestroyed += PlayerLockingBG_VFX.Instance.Stop;
			OnDestroyed += LevelManager.Instance.PlayerLevelData.IncrementPlayerDeath;
			Health.OnDeath += LevelManager.Instance.GameOver;

			InputsHandler.OnFocus += Focus;
			InputsHandler.OnMove += Move;
			InputsHandler.OnFireGun += FireGun;
			InputsHandler.OnFireMissiles += AimMissiles;
			InputsHandler.OnFireMissiles += FireMissile;
			InputsHandler.OnUseUltimate += UseUltimate;

			Health.OnHitpointsUpdate += hud.UpdateLifeText; // This is good even it doesn't make sense in English.

			OnUseUltimate += LevelManager.Instance.PlayerLevelData.IncrementUltimateUsed;

			GameSceneLoader.OnStartLoadingNextScene += DisableObj;
			GameManager.OnGamePause += ResetPlayerGunsAndState;

			Respawn();
		}

		private void Start()
		{
			coll.tag = "Player";
			originalSpeed = speed;
		}

		private void FixedUpdate()
		{
			if (isMoving && currentState == State.Normal) rigid.MovePosition(rigid.position + direction * speed * Time.fixedDeltaTime);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (Health.IsInvincible || currentState == State.Dead) return;

			if (collision.CompareTag(Tags.Enemy_Projectile)
				|| collision.CompareTag(Tags.Enemy))
			{
				StopAllCoroutines();
				EnableTrail(false);
				GameObjectPooler.Spawn(DeathVFX.name, transform.position, DeathVFX.transform.rotation).SetActive(true);
				currentState = State.Dead;
				OnDestroyed?.Invoke();
				laserSys.DisableReload();
				laserSys.StopLaserAim();
				laserSys.ClearAll();
				ultCooldown.Stop();
				ultCooldown.ClearReset();
				ResetPlayerGunsAndState();
				gunsSystem.CancelReload();
				coll.gameObject.layer = LayerMask.NameToLayer($"{Layers.PlayerRespawning}");
				EnableShipVisuals(false);
				InputsHandler.DisableInputs();
				hud.Disable();
				Health.Hitpoints -= 1;

				if (Health.IsAlive) StartCoroutine(Destroyed_Routine());
				else gameObject.SetActive(false);

				IEnumerator Destroyed_Routine()
				{
					yield return waitForDestroyedCD;
					Respawn();
				}
			}
		}

		private void OnDisable()
		{
			if (!LevelManager.Instance) return;

			GameSceneLoader.OnStartLoadingNextScene -= DisableObj;

			InputsHandler.OnFocus -= Focus;
			InputsHandler.OnMove -= Move;
			InputsHandler.OnFireGun -= FireGun;
			InputsHandler.OnFireMissiles -= AimMissiles;
			InputsHandler.OnUseUltimate -= UseUltimate;

			OnDestroyed -= PlayerLockingBG_VFX.Instance.Stop;
			OnDestroyed -= LevelManager.Instance.PlayerLevelData.IncrementPlayerDeath;

			OnUseUltimate -= LevelManager.Instance.PlayerLevelData.IncrementUltimateUsed;

			Health.OnDeath -= LevelManager.Instance.GameOver;
			LevelManager.Instance.OnGameWon -= OnWin;
		}

		private void Respawn()
		{
			var spawn = GameObject.FindGameObjectWithTag("PlayerSpawnPoint");
			rigid.position = transform.position = spawn.transform.position;

			gunsSystem.ReplenishAmmunitions();

			laserSys.EnableReload();
			InputsHandler.EnableInputs();

			StartCoroutine(RespawnRoutine(() => {
				currentState = State.Normal;
				EnableUltimateUse(true);

				StartCoroutine(FlashRoutine(() => {
					EnableShipVisuals(true);
					coll.gameObject.layer = LayerMask.NameToLayer("Player");
					isFlashing = false;
				}));
			}));

			IEnumerator RespawnRoutine(Action OnRespawned)
			{
				EnableTrail(true);
				EnableShipVisuals(true);
				hud.Display();

				var timeTaken = 0.2f;
				var destination = rigid.position + (Vector2)rigid.transform.up * 150;
				var startPosition = rigid.position;

				//Force Player to move to destination
				for (float i = 0; i <= timeTaken; i += Time.deltaTime)
				{
					rigid.position = Vector3.Lerp(startPosition, destination, Mathf.Clamp01(i / timeTaken));
					yield return null;
				}
				OnRespawned?.Invoke();
				yield break;
			}

			IEnumerator FlashRoutine(Action OnFlashOver)
			{
				isFlashing = true;
				bool toggle = false;

				for (float i = 0; i < invincibleTime; i += Time.deltaTime + flashDelay)
				{
					EnableShipVisuals(toggle = !toggle);
					yield return new WaitForSeconds(flashDelay);
				}
				OnFlashOver.Invoke();
			}
		}

		private void Move(Vector2 direction, InputActionPhase phase)
		{
			OverrideInvincibility();

			this.direction = direction;

			if (phase == InputActionPhase.Started) isMoving = true;
			else if (phase == InputActionPhase.Canceled) isMoving = false;
		}

		private void EnableShipVisuals(bool state)
		{
			Array.ForEach(playerRenderers, rend => rend.enabled = state);
		}

		private void Focus(InputActionPhase phase)
		{
			if (phase == InputActionPhase.Started)
			{
				speed *= focusMultiplier;
				stateMachine.Play($"{State.Focus}");
			}
			else if (phase == InputActionPhase.Canceled)
			{
				speed = originalSpeed;
				stateMachine.Play($"{State.Normal}");
			}
		}

		private void FireGun(InputActionPhase phase)
		{
			OverrideInvincibility();

			if (currentState != State.Dead)
			{
				if (phase == InputActionPhase.Started)
				{
					gunsSystem.Fire();
				}
				else if (phase == InputActionPhase.Canceled)
				{
					gunsSystem.HoldFire();
				}
			}
		}

		private void UseUltimate(InputActionPhase phase)
		{
			OverrideInvincibility();
			if (phase == InputActionPhase.Started && canUseUltimate && currentState != State.Dead)
			{
				EnableUltimateUse(false);
				OnUseUltimate?.Invoke();

				ultCooldown.StartCounting(ultCDTime, () => EnableUltimateUse(true));
				StartCoroutine(InvincibleRoutine());

				GameObjectPooler.Spawn(ultimateObj.name, transform.position).SetActive(true);
			}

			IEnumerator InvincibleRoutine()
			{
				Health.IsInvincible = true;
				yield return new WaitForSeconds(0.5f);
				Health.IsInvincible = false;
				yield return null;
			}
		}

		private void AimMissiles(InputActionPhase phase)
		{
			OverrideInvincibility();

			if (phase == InputActionPhase.Started && currentState != State.Dead)
			{
				laserSys.AimLaser();
				PlayerLockingBG_VFX.Instance.Play();
			}
		}

		private void FireMissile(InputActionPhase phase)
		{
			OverrideInvincibility();

			if (phase == InputActionPhase.Canceled && currentState != State.Dead)
			{
				laserSys.FireMissile();
				PlayerLockingBG_VFX.Instance.Stop();
				OnFireMissile?.Invoke();
			}
		}

		public void EnableUltimateUse(bool state) => canUseUltimate = state;

		private void OverrideInvincibility()
		{
			if (!isFlashing) return;
			StopAllCoroutines();
			isFlashing = false;
			currentState = State.Normal;
			EnableShipVisuals(true);
			coll.gameObject.layer = LayerMask.NameToLayer($"{Layers.Player}");
			gunsSystem.ReplenishAmmunitions();
		}

		public void DisableObj() => gameObject.SetActive(false);

		public void OnWin()
		{
			StartCoroutine(MoveOutOfFrameRoutine());

			IEnumerator MoveOutOfFrameRoutine()
			{

				while (transform.position.y < 1500)
				{
					transform.Translate(transform.up * speed * Time.deltaTime);
					yield return null;
				}

				DisableObj();
			}
		}

		private void ResetPlayerGunsAndState()
		{
			stateMachine.Play($"{State.Normal}");
			gunsSystem.HoldFire();
		}

		private void EnableTrail(bool state)
		{
			if (state) Array.ForEach(trails, trail => { trail.time = 0.1f; });
			else Array.ForEach(trails, trail => { trail.time = 0f; });
			Array.ForEach(trails, trail => { trail.emitting = state; trail.enabled = state; });
		}

		public event Action OnDestroyed;
		public event Action OnFireMissile;
		public event Action OnUseUltimate;
	}
}
