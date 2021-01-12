
using UnityEngine;

namespace GameSystem
{
    public class SpawnGroup : MonoBehaviour
    {
        [SerializeField] private float delayUntilNextSpawn = 5f;
        private int spawnerCounter = 0;

        private void Awake()
        {
            LevelEnemySpawner[] spawners = GetComponentsInChildren<LevelEnemySpawner>(true);
            spawnerCounter = spawners.Length;
            foreach (LevelEnemySpawner spawner in spawners)
            {
                spawner.OnZeroMinion += ReduceSpawnerCount;
            }
        }

        private void ReduceSpawnerCount()
        {
            if (--spawnerCounter <= 0) OnZeroChild?.Invoke();
        }

        private void SelfDestroy()
        {
            Destroy(gameObject);
        }

        public float DelayUntilNextSpawn => this.delayUntilNextSpawn;


        public event System.Action OnZeroChild;
    }
}
