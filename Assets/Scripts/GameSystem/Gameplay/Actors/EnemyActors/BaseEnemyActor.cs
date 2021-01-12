using GameSystem.GameSceneManagement;
using GameSystem.Weapons.Projectiles;
using UnityEngine;

namespace GameSystem.Actors
{
    [RequireComponent(typeof(HealthComponent), typeof(Disabler), typeof(Rigidbody2D))]
    [RequireComponent(typeof(Root),typeof(Look2D))]
    public abstract class BaseEnemyActor : MonoBehaviour, IDestructible, ISceneObject
    {
        [SerializeField] private GameObject deathCombo;
        protected VisualInteractiveComponents VIComps;
        public HealthComponent Health { get; private set; }
        private Rigidbody2D rigid;
        public Look2D LookComp { get; private set; }
        public CheckpointMove Movement { get; private set; }
        public bool DontDisableAtFinalDestination = false;
        protected virtual void Awake()
        {
            GetComponents();
        }

        protected virtual void OnEnable()
        {
            if (!LevelManager.Instance) return;

            VIComps.gameObject.layer = LayerMask.NameToLayer("Enemy");

            LevelManager.Instance.OnGameWon += Death;
            LevelManager.Instance.OnGameOver += Death;
            GameSceneLoader.OnSceneTransitioning += DisableObj;

            AddSubscribers();
        }

        private void Start()
        {
            SetStartingValues();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Utility.Tags.Player_Projectile))
            {
               Health.Hitpoints = Health.Hitpoints - collision.GetComponent<Projectile>().Damage;
            }
            else if (collision.CompareTag(Utility.Tags.Player)
                || collision.CompareTag("PlayerUltimate"))
            {
                Death();
            }
        }

        private void OnDisable()
        {
            if (!LevelManager.Instance) return;

            LevelManager.Instance.OnGameWon -= Death;
            LevelManager.Instance.OnGameOver -= Death;
            GameSceneLoader.OnSceneTransitioning -= DisableObj;

            RemoveSubscribers();
        }

        /// <summary>
        /// Called by OnTriggerEnter2D.
        /// </summary>
        protected virtual void Death()
        {
            VIComps.gameObject.layer = LayerMask.NameToLayer($"{Layers.IgnoreAll}");

            if (EnemyPooler.Instance)
            {
                GameObjectPooler.Spawn(deathCombo.name, this.transform.position, this.transform.rotation).SetActive(true);
                DisableObj();
            }
            else
            {
                Debug.LogError("Pooler not found.", this);
            }
        }

        /// <summary>
        /// Player ultimate calls this function.
        /// </summary>
        [ContextMenu("Instant kill this enemy")]
        public void Destroy()
        {
            Health.Suicide();
        }

   
        /// <summary>
        /// Called by Awake. Used to get components on the current object.
        /// </summary>
        protected virtual void GetComponents()
        {
            LookComp = GetComponent<Look2D>();
            Movement = GetComponent<CheckpointMove>();
            Health = GetComponent<HealthComponent>();
            VIComps = GetComponentInChildren<VisualInteractiveComponents>(true);
            rigid = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Called by OnDisable. Remove subscribers.
        /// </summary>
        protected virtual void RemoveSubscribers()
        {
            Health.OnDeath -= Death;
            Health.OnDeath -= LevelManager.Instance.PlayerLevelData.IncrementMinionKilled;

            Movement.OnWaypointChange -= LookComp.LookTowards;
            if (!DontDisableAtFinalDestination) Movement.OnFinalDestinationReached -= DisableObj;
        }

        /// <summary>
        /// Called by OnEnable. Add Subscribers.
        /// </summary>
        protected virtual void AddSubscribers()
        {
            Health.OnDeath += Death;
            Health.OnDeath += LevelManager.Instance.PlayerLevelData.IncrementMinionKilled;

            Movement.OnWaypointChange += LookComp.LookTowards;
            if(!DontDisableAtFinalDestination) Movement.OnFinalDestinationReached += DisableObj;
        }

        /// <summary>
        /// Called by Start. Used to set components attributes.
        /// </summary>
        protected virtual void SetStartingValues()
        {
            tag = Utility.Tags.Enemy;
            VIComps.tag = tag;

            VIComps.Collider.isTrigger = true;

            VIComps.SRend.enabled = true;
            VIComps.SRend.color = Color.red;

            VIComps.AudioS.loop = false;
            VIComps.AudioS.enabled = false;

            rigid.bodyType = RigidbodyType2D.Kinematic;
            rigid.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
            rigid.simulated = true;
            rigid.sleepMode = RigidbodySleepMode2D.StartAwake;
            rigid.interpolation = RigidbodyInterpolation2D.None;
        }

        public void DisableObj()
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
    }
}

