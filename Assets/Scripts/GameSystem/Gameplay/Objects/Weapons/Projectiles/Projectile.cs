
using GameSystem.GameSceneManagement;
using System;
using System.Collections;
using UnityEngine;

namespace GameSystem.Weapons.Projectiles
{

    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public abstract class Projectile : MonoBehaviour, IDestructible, ISceneObject
    {

        public TargetTag Target = TargetTag.Player;
        public GameObject DestructionVFX; // When projectile is destroyed in mid air without collision.
        public GameObject OnHitVFX;
        public int Damage = 1;
        [HideInInspector] public float Speed;

        protected Collider2D coll2D;
        protected Rigidbody2D rigid;
        protected SpriteRenderer sRend;

        protected bool hasCollided = false;
        private bool canCollideWithPlayerUltimate = true;

        protected static readonly WaitForSeconds waitForTriggerDespawn = new WaitForSeconds(1f);

        protected virtual void Awake()
        {
            GetComponents();

            canCollideWithPlayerUltimate = Target != TargetTag.Enemy;
        }

        protected virtual void OnEnable()
        {
            if (!LevelManager.Instance) return;

            gameObject.layer = LayerMask.NameToLayer($"{Layers.Projectiles}");
            coll2D.enabled = true;
            coll2D.isTrigger = true;
            sRend.enabled = true;
            hasCollided = false;

            TargetSubscription();
            
            OnUnitySceneEventFunctions.Instance.OnSceneDestroyed += StopAllCoroutines;
            LevelManager.Instance.OnGameOver += Destroy;
            LevelManager.Instance.OnGameWon += Destroy;
            GameSceneLoader.OnStartLoadingNextScene += DisableObj;
            OnDestroyedByUltimate += Destroy;
        }

        protected virtual void Start()
        {
            rigid.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rigid.bodyType = RigidbodyType2D.Kinematic;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (hasCollided) return;
            
            if (collision.CompareTag(Utility.Tags.Level_Boundary_Trigger))
            {
                hasCollided = true;
                StartCoroutine(TriggerDespawnRoutine());
            }
            else
            {
                if (collision.CompareTag(Target.ToString()) && Target == TargetTag.Enemy)
                {
                    hasCollided = true;
                    OnCollisionHitTarget?.Invoke();
                    GameObjectPooler.Spawn(OnHitVFX.name, transform.position).SetActive(true);
                    StartCoroutine(DespawnRoutine());
                }
                else if(collision.CompareTag("PlayerUltimate") && canCollideWithPlayerUltimate)
                {
                    if (collision.CompareTag("PlayerUltimate"))
                    {
                        hasCollided = true;
                        OnDestroyedByUltimate?.Invoke();
                    }
                    else
                    {
                        StartCoroutine(DespawnRoutine());
                    }
                }
            }
        }

        protected virtual void OnDisable()
        {
            if (!LevelManager.Instance) return;

            if (Target == TargetTag.Enemy) OnCollisionHitTarget -= LevelManager.Instance.PlayerLevelData.IncrementBulletHitTarget;
            OnUnitySceneEventFunctions.Instance.OnSceneDestroyed -= StopAllCoroutines;
            LevelManager.Instance.OnGameOver -= Destroy;
            LevelManager.Instance.OnGameWon -= Destroy;
            GameSceneLoader.OnStartLoadingNextScene -= DisableObj;
            OnDestroyedByUltimate -= Destroy;

        }

        protected virtual void TargetSubscription()
        {
            if (Target == TargetTag.Enemy) // Player Projectile
            {
                OnCollisionHitTarget += LevelManager.Instance.PlayerLevelData.IncrementBulletHitTarget;
            }

            if (Target == TargetTag.Player) // Enemy Projectile
            {
                OnDestroyedByUltimate += LevelManager.Instance.PlayerLevelData.IncrementEnemyProjectileDestroyedByUltimate;
            }
        }

        protected virtual IEnumerator DespawnRoutine()
        {
            gameObject.layer = LayerMask.NameToLayer($"{Layers.IgnoreAll}");
            DisableObj();
            yield break;
        }

        protected virtual IEnumerator DestroyRoutine()
        {
            StopAllCoroutines();
            GameObjectPooler.Spawn(DestructionVFX.name, transform.position).SetActive(true);
            gameObject.layer = LayerMask.NameToLayer($"{Layers.IgnoreAll}");
            DisableObj();
            yield break;
        }

        protected virtual IEnumerator TriggerDespawnRoutine()
        {
            gameObject.layer = LayerMask.NameToLayer($"{Layers.IgnoreAll}");
            coll2D.enabled = false;
            yield return waitForTriggerDespawn;
            gameObject.SetActive(false);
            yield break;
        }

        /// <summary>
        /// Function that contains all the actions necessary to move the object aka pattern.
        /// </summary>
        protected abstract void Move();

        protected virtual void MoveForward()
        {
            rigid.MovePosition(rigid.position + (Vector2)transform.up * Speed * Time.fixedDeltaTime);
        }

        public void Destroy()
        {
            StartCoroutine(DestroyRoutine());
        }

        public void DisableObj()
        {
            StopAllCoroutines();
            gameObject.layer = LayerMask.NameToLayer($"{Layers.IgnoreAll}");
            gameObject.SetActive(false);
        }

        protected virtual void GetComponents()
        {
            coll2D = GetComponent<Collider2D>();
            rigid = GetComponent<Rigidbody2D>();
            sRend = GetComponentInChildren<SpriteRenderer>(true);
        }

        public event Action OnCollisionHitTarget;
        public event Action OnDestroyedByUltimate;
    }
}

