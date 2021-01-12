using System;
using System.Collections;
using UnityEngine;

namespace GameSystem.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private bool CHEAT_FIRE = false;
        [SerializeField] private bool CHEAT_MISSILE = false;
#endif
        [SerializeField] protected GameObject projectile;
        public Rotator Rotator { private set; get; }
        protected AudioSource audioS;

        [SerializeField] protected float initialDelay = 0f;
        [SerializeField] protected float delayBetweenWaves = 0.5f;
        [SerializeField] protected float delayBetweenWaveGroups = 0f;

        [Header("Bullets Settings")]
        [SerializeField]
        protected Projectiles.TargetTag targetTag = Projectiles.TargetTag.Player;

        [SerializeField] protected int numProjectilesPerWave = 1;
        [SerializeField] protected int numWavesPerGroup = 1;
        [SerializeField] protected int numWaveGroups = 0;
        public float InitialWaveSpeed = 100f;
        [SerializeField] protected float lastWaveSpeed;
        [SerializeField] protected bool overrideColor = false;
        [SerializeField] protected Color newColor;
        [SerializeField] protected GameObject overrideCollisionVFX;
        [SerializeField] protected GameObject overrideDestructionVFX;

        [Header("Offsets")]
        [SerializeField] 
        protected float upOffset = 0f;

        [Header("Angle Settings")]
        [SerializeField, Range(0f, 360f)] 
        protected float centralAngle = 0;

        [SerializeField, Range(0f, 360f)] 
        protected float offset = 0f;

        protected Coroutine fireCoroutine;
        protected WaitForSeconds waitForWaveGroupDelay;
        protected WaitForSeconds waitForDelayBetweenWave;

        private void Awake()
        {
            waitForWaveGroupDelay = new WaitForSeconds(delayBetweenWaveGroups);
            waitForDelayBetweenWave = new WaitForSeconds(delayBetweenWaves);

            audioS = GetComponent<AudioSource>();
            Rotator = GetComponent<Rotator>();
        }

#if UNITY_EDITOR
        private void OnEnable()
        {
            if (CHEAT_FIRE)
            {
                waitForWaveGroupDelay = new WaitForSeconds(delayBetweenWaveGroups);
                waitForDelayBetweenWave = new WaitForSeconds(delayBetweenWaves);
                fireCoroutine = null;
                if (!CHEAT_MISSILE) Fire();
                else Fire(GameObject.FindGameObjectWithTag("Player").transform);
            }
        }
#endif

        protected virtual void OnDisable()
        {
            HoldFire();
            StopAllCoroutines();
        }

        public float ROF
        {
            get => this.delayBetweenWaves;
            set
            {
                delayBetweenWaves = value < 0 ? 0 : value;
                waitForDelayBetweenWave = new WaitForSeconds(ROF);
            }
        }

        public void LoadProjectile(GameObject projectile)
        {
            this.projectile = projectile;
        }

        public void Fire()
        {
            if (fireCoroutine != null) return;

            fireCoroutine = StartCoroutine(FireRoutine());
        }

        public void Fire(Transform target)
        {
            if (fireCoroutine != null) return;

            fireCoroutine = StartCoroutine(FireRoutine(target));
        }

        public void HoldFire()
        {
            if (fireCoroutine == null) return;

            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }

        protected virtual IEnumerator FireRoutine()
        {
            //Empty
            yield break;
        }

        protected virtual IEnumerator FireRoutine(Transform target)
        {
            //Empty
            yield break;
        }

        protected virtual IEnumerator SpawnProjectileRoutine(float spreadAngle, float currentAngle, float upValueForCentering, float currentWaveGroupSpeed)
        {
            //Empty
            yield break;
        }

        public abstract event Action OnFire;
    }
}
