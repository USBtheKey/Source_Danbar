
using GameSystem.UI;
using GameSystem.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
	public class LaserAimedMissileSystem : MonoBehaviour
	{
		private LineRenderer lRend;
		[SerializeField] private GameObject lockOnObj;
		private MissilePod[] pods;
		[SerializeField] private Material laserMaterial;
		[SerializeField] private CounterIndicator missileReloaderIndicator;
		[SerializeField] private PlayerHUD hud;

		private Transform laserHitTarget;
		private const float missileLineCastMaxDistance = 1285;
		private const float laserAimSpeed = 0.05f;
		private int maxTargetCount = 6;
		private int missiles = 0;
		private const int maxMissile = 6;
		[SerializeField] private float reloadSpeed = 2f;
		private bool reloadingInProgress = false;
		private bool canReload = true;

		private WaitForSeconds waitForLaserAimCD;
		private WaitForSeconds waitForMissileFireDelay;

		private Coroutine fireMissileRoutine;
		private Coroutine aimRoutine;

		private void Awake()
		{
			waitForLaserAimCD = new WaitForSeconds(laserAimSpeed);
			waitForMissileFireDelay = new WaitForSeconds(0.05f);

			lRend = GetComponent<LineRenderer>();
			pods = GetComponentsInChildren<MissilePod>();
		}

        private void OnEnable()
        {
			if (!LevelManager.Instance) return;

			GameManager.OnGamePause += StopLaserAim;
			GameManager.OnGameResume += FireMissile;

			foreach (Weapon weap in pods)
			{
				weap.OnFire += LevelManager.Instance.PlayerLevelData.IncrementMissile;
			}

			lRend.enabled = false;
			
			ClearAll();
		}

        void Start() 
		{ 
			lRend.positionCount = 2;
			lRend.material = laserMaterial;
		}

		private void Update()
		{
			if(canReload) Reload();

			if (!lRend.enabled) return;
			
			lRend.SetPosition(0, this.transform.position);

			if (laserHitTarget)
			{
				lRend.SetPosition(1, this.transform.position + this.transform.up * Vector3.Distance(laserHitTarget.position, this.transform.position));
            }
            else //Aiming the void 
            {
				lRend.SetPosition(1, this.transform.position + this.transform.up * (missileLineCastMaxDistance - transform.position.y));
			}
		}


		private void OnDisable()
        {
			if (!LevelManager.Instance) return;

			GameManager.OnGamePause -= StopLaserAim;
			GameManager.OnGameResume -= FireMissile;

			foreach (Weapon weap in pods)
			{
				weap.OnFire -= LevelManager.Instance.PlayerLevelData.IncrementMissile;
			}

			ClearAll();
		}

		public void AimLaser()
		{
			if (aimRoutine != null) return;

			aimRoutine = StartCoroutine(AimRoutine());

			IEnumerator AimRoutine()
			{
				lRend.enabled = true;

				while (true)
				{
					//Find Laser Pointer Starting point & End point
					Vector2 startingPoint = (Vector2)(transform.position) + Vector2.up * 100f;
					Vector2 endPoint = startingPoint + (Vector2)(transform.up * (missileLineCastMaxDistance - gameObject.transform.position.y));

					RaycastHit2D[] objsHit = Physics2D.LinecastAll(startingPoint, endPoint, LayerMask.GetMask("Enemy"));
					var bossHit = Physics2D.LinecastAll(startingPoint, endPoint, LayerMask.GetMask("Boss"));
					var allObjHits = new RaycastHit2D[objsHit.Length + bossHit.Length];
					objsHit.CopyTo(allObjHits, 0);
					bossHit.CopyTo(allObjHits, objsHit.Length);

					RaycastHit2D enemyObj = Array.Find(allObjHits, hit => hit.collider.CompareTag("Enemy"));

					if (enemyObj.transform)
					{
						laserHitTarget = enemyObj.collider.transform;
						if (LockOn.InstancesCount < maxTargetCount)
						{
							var lockOn = GameObjectPooler.Spawn(lockOnObj.name, laserHitTarget.position, lockOnObj.transform.rotation);
							lockOn.SetActive(true);
							lockOn.transform.parent = laserHitTarget;
						}
					}
					else
					{
						laserHitTarget = null;
					}

					yield return waitForLaserAimCD;
				}
			}
		}

		public void StopLaserAim()
		{
			lRend.enabled = false;
			laserHitTarget = null;

			if (aimRoutine != null)
			{
				StopCoroutine(aimRoutine);
				aimRoutine = null;
			}
		}

		public void FireMissile()
		{
			StopLaserAim();
			if (fireMissileRoutine != null) return;

			fireMissileRoutine = StartCoroutine(FireMissileRoutine());

			IEnumerator FireMissileRoutine()
			{
				List<Transform> targets = new List<Transform>();

				if (LockOn.InstancesCount <= 0)
				{
					yield break;
				}
				else
				{
					foreach (LockOn lk in LockOn.GetLockedInstances)
					{
						var root = lk.GetComponentInParent<Root>();

						if (root) targets.Add(root.transform);
						else
						{
							Debug.LogError("Missile Lock On failed to find Parent Transform.", this);
						}
					}
				}

				HashSet<int> podSequence = new HashSet<int>();
				for (int i = 0; i < targets.Count; i++)
				{
					if (missiles <= 0)
					{
						fireMissileRoutine = null;
						break;
					}
					else
					{
						int rnd;

						do
						{
							rnd = UnityEngine.Random.Range(0, maxTargetCount);
						}
						while (podSequence.Contains(rnd)); //If dupe redo

						pods[rnd].Fire(targets[i]);
						hud.UpdateMissileText(--missiles);
						podSequence.Add(rnd);
					}

					yield return waitForMissileFireDelay;

				}
				OnFiredMissile?.Invoke();
				fireMissileRoutine = null;
			}
		}

		private void Reload()
        {
			if (!reloadingInProgress && missiles < maxMissile)
			{
				reloadingInProgress = true;
				missileReloaderIndicator.StartCounting(reloadSpeed, () => {
					hud.UpdateMissileText(++missiles);
					reloadingInProgress = false;
				});
			}
		}

        public void ClearAll()
        {
			missileReloaderIndicator.Stop();
			missileReloaderIndicator.ClearReset();
			hud.UpdateMissileText(missiles = 0);

			reloadingInProgress = false;

			fireMissileRoutine = null;
			aimRoutine = null;
			laserHitTarget = null;
		}

		public void EnableReload() => canReload = true;
		public void DisableReload() => canReload = false;

        public event Action OnFiredMissile;
	}
}