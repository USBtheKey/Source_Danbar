
using UnityEngine;

namespace GameSystem
{
    public sealed class EnemyPooler: GameObjectPooler
    {
        public static EnemyPooler Instance { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            CreateInstance();
            InstantiateGameObjects(pools);
            pools = null;
        }

        protected override void OnDestroy()
        {
            Instance = null;
        }

        protected override void CreateInstance()
        {
            if (!Instance) Instance = this;
            else Destroy(this.gameObject);
        }
    }
}