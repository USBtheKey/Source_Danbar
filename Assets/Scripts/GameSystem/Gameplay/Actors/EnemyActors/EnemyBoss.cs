
using GameSystem.GameSceneManagement;
using GameSystem.Utility;
using System;
using System.Collections;
using UnityEngine;

namespace GameSystem.Actors
{
    [RequireComponent(typeof(HealthComponent), typeof(Root))]
    public abstract class EnemyBoss : MonoBehaviour, ISceneObject
    {
        protected HealthComponent health { private set; get; }
        protected Animator stateMachine;
        protected SpriteRenderer sRend;
        [SerializeField] protected AudioClip deathSFX;
        protected AudioSource audioS;
        protected VisualInteractiveComponents VIComps;


        protected virtual void Awake()
        {
            VIComps = GetComponentInChildren<VisualInteractiveComponents>(true);
            audioS = GetComponentInChildren<AudioSource>(true);
            health = GetComponent<HealthComponent>();
            sRend = GetComponentInChildren<SpriteRenderer>(true);
            stateMachine = GetComponent<Animator>();
        }

        protected virtual void OnEnable()
        {
            Array.ForEach(transform.GetComponentsInChildren<Transform>(), t => t.gameObject.layer = LayerMask.NameToLayer("Boss"));

            var uiHealthBar = LevelUIManager.Instance.UI_BossHealthBar;
            OnEnabled += uiHealthBar.EnableChildren;

            OnEnabled?.Invoke();

            health.OnDeath += Death;
            health.OnDeath += StopAllCoroutines;

            health.OnDeath += LevelManager.Instance.PlayerLevelData.IncrementBossKilled;
            health.OnLifeUpdate += uiHealthBar.UpdateLife;
            health.OnHitpointsRatioUpdate += uiHealthBar.UpdateHealthRatio;
            OnDeath += uiHealthBar.DisableChildren;
            OnDeath += LevelManager.Instance.GameWon;

            LevelManager.Instance.GetPlayer().Health.OnDeath += StopAllCoroutines;
            LevelManager.Instance.OnGameOver += StopAllCoroutines;
            LevelManager.Instance.OnGameOver += DisableObj;
            LevelManager.Instance.OnGameWon += StopAllCoroutines;
            LevelManager.Instance.OnGameWon += DisableObj;

            GameSceneLoader.OnStartLoadingNextScene += DisableObj;
        }

        protected virtual void Start()
        {
            tag = Tags.Enemy;

            health.Life = health.Life; //Forces a Life update.
        }

        protected abstract void OnTriggerEnter2D(Collider2D collision);

        protected virtual void OnDisable()
        {
            GameSceneLoader.OnStartLoadingNextScene -= DisableObj;

            var uiHealthBar = LevelUIManager.Instance.UI_BossHealthBar;
            OnEnabled -= uiHealthBar.EnableChildren;

            health.OnDeath -= Death;
            health.OnDeath -= LevelManager.Instance.PlayerLevelData.IncrementBossKilled;
            health.OnLifeUpdate -= uiHealthBar.UpdateLife;
            health.OnHitpointsRatioUpdate -= uiHealthBar.UpdateHealthRatio;
            OnDeath -= uiHealthBar.DisableChildren;
            OnDeath -= LevelManager.Instance.GameWon;

            LevelManager.Instance.GetPlayer().Health.OnDeath -= StopAllCoroutines;
            LevelManager.Instance.OnGameOver -= StopAllCoroutines;
            LevelManager.Instance.OnGameOver -= DisableObj;
            LevelManager.Instance.OnGameWon -= StopAllCoroutines;
            LevelManager.Instance.OnGameWon -= DisableObj;
        }

        protected void Death()
        {
            StartCoroutine(DeathRoutine());
        }

        protected IEnumerator DeathRoutine()
        {
            Array.ForEach(transform.GetComponentsInChildren<Transform>(), t => t.gameObject.layer = LayerMask.NameToLayer("Boss"));

            sRend.enabled = false;
            //audioS.PlayOneShot(deathSFX);
            // deathAnim.Play();

            OnDeath?.Invoke();
            gameObject.SetActive(false);
           
            yield break;
        }

        private void UpdateHealthBar()
        {
            
        }

        public void DisableObj()
        {
            gameObject.SetActive(false);
        }

        public event Action OnEnabled;
        public event Action OnDeath;
    }
}


