using GameSystem.GameSceneManagement;
using UnityEngine;

namespace GameSystem.Actors
{
    [RequireComponent(typeof(Look2D))]
    public sealed class KamikazeFighter : SingleWeaponEnemy
    {
#if UNITY_EDITOR
        [SerializeField] private bool CHEAT_TEST = false;
#endif
        private Transform player;
        private Vector3 direction;
        [SerializeField] private float speed = 400f;

        protected override void OnEnable()
        {

#if UNITY_EDITOR
            if (CHEAT_TEST) player = GameObject.FindGameObjectWithTag($"{Layers.Player}").transform;
#endif
            if (!LevelManager.Instance) return;
            player = LevelManager.Instance.GetPlayer().transform;
            VIComps.gameObject.layer = LayerMask.NameToLayer($"{Layers.Enemy}");


            Health.OnDeath += Death;
            Health.OnDeath += LevelManager.Instance.PlayerLevelData.IncrementMinionKilled;

            GameSceneLoader.OnSceneTransitioning += DisableObj;
            LevelManager.Instance.OnGameOver += DisableObj;
            LevelManager.Instance.OnGameWon += DisableObj;
        }

        private void OnDisable()
        {
            if (!LevelManager.Instance) return;

            Health.OnDeath -= Death;
            Health.OnDeath -= LevelManager.Instance.PlayerLevelData.IncrementMinionKilled;

            LevelManager.Instance.OnGameOver -= DisableObj;
            LevelManager.Instance.OnGameWon -= DisableObj;
            GameSceneLoader.OnSceneTransitioning -= DisableObj;
        }

        private void Update()
        {
            Move();
            Look();
        }

        private void Move()
        {
            direction = player.position - transform.position;

            if (direction.magnitude <= 5f) return;
            if (direction.magnitude <= 450 && direction.magnitude >= 440) gun.Fire();

            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }

        private void Look()
        {
            if (direction.magnitude <= 5f) return;

            LookComp.Track(player.position);
        }
    }
}
