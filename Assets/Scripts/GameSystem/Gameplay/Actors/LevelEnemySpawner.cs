using System.Collections;
using UnityEngine;
using GameSystem.Actors;

namespace GameSystem
{

    public class LevelEnemySpawner : MonoBehaviour
    {
        [Header("Set Minions")]
#if UNITY_EDITOR
        [SerializeField] private bool CHEAT_SPAWN = false;
#endif
        [SerializeField] private GameObject minion; //Obj to spawn from pooler.
        [SerializeField] private int numberToSpawn = 1;
        [SerializeField] private float delayBetweenSpawn = 0.15f;
        [SerializeField] private EnemyLookAt lookAtDirection = EnemyLookAt.Destination;
        [SerializeField] private bool dontDisableAtFinalDestination = false;
        [SerializeField] private float speed;
        private int remainingMinion = 0;

        private Path path;
        private WaitForSeconds waitForDelayBetweenSpawn;

        private void Awake()
        {
            if (minion.name != "KamikazeFigther") path = GetComponentInChildren<Path>(true);
            waitForDelayBetweenSpawn = new WaitForSeconds(delayBetweenSpawn);
        }

        private void OnEnable()
        {
            if (minion.name != "KamikazeFigther")
            {
                if (path.GetWaypoints() == null)
                {
                    Debug.LogError("waypoints missing " + this.gameObject.name, this);
                }
            }

#if UNITY_EDITOR
            if (!CHEAT_SPAWN)
            {
                GameSceneManagement.GameSceneLoader.OnSceneTransitioning += StopAllCoroutines;
                LevelManager.Instance.OnGameOver += StopAllCoroutines;
                LevelManager.Instance.OnGameWon += StopAllCoroutines;
            }
#endif
            SpawnMinion();

            if (minion.name != "KamikazeFigther") Destroy(path.gameObject);
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            if (CHEAT_SPAWN) return;
#endif

            GameSceneManagement.GameSceneLoader.OnSceneTransitioning -= StopAllCoroutines;
        }

        private void SpawnMinion()
        {
            remainingMinion = numberToSpawn;

            StartCoroutine(SpawnMinion_Routine());

            IEnumerator SpawnMinion_Routine()
            {
                for (int i = 0; i < numberToSpawn; i++)
                {
                    GameObject spawnedObj = null;

                    if (minion.name != "KamikazeFigther")
                    {
                        spawnedObj = GameObjectPooler.Spawn(minion.name, path.GetWaypoints()[0].position);

                        BaseEnemyActor BEA = spawnedObj.GetComponent<BaseEnemyActor>();
                        SingleWeaponEnemy singleWeapon = spawnedObj.GetComponent<SingleWeaponEnemy>();
                        KamikazeFighter suicideEnemy = spawnedObj.GetComponent<KamikazeFighter>();
                        MoveableWall wall = spawnedObj.GetComponent<MoveableWall>();
                        GunsDestroyer gunsDestroyer = spawnedObj.GetComponent<GunsDestroyer>();

                        if (BEA)
                        {
                            BEA.DontDisableAtFinalDestination = dontDisableAtFinalDestination;

                            BEA.Movement.SetCheckpoints(path.GetWaypoints());
                            BEA.Movement.OnFinalDestinationReached += ReduceCounter;
                            if(speed > 0) BEA.Movement.MoveSpeed = speed;
                            BEA.LookComp.LookAt = lookAtDirection;

                            BEA.Health.OnDeath += ReduceCounter;
                        }
                        else if (singleWeapon)
                        {
                        }
                        else if (suicideEnemy)
                        {
                        }
                        else if (wall)
                        {
                        }
                        else if (gunsDestroyer)
                        {
                        }
                        else
                        {
                            Debug.LogError($"Spawner: Unrecognized Actor \"{spawnedObj.name}\" ", this);
                        }
                    }
                    else // It is the Kamikaze Fighter
                    {
                        spawnedObj = GameObjectPooler.Spawn(minion.name, transform.position);
                    }

                    spawnedObj.SetActive(true);

                    if(i < numberToSpawn - 1) yield return waitForDelayBetweenSpawn;
                }
            }
        }

        private void ReduceCounter()
        {
            if (--remainingMinion <= 0) OnZeroMinion?.Invoke();
        }

        public event System.Action OnZeroMinion;
    }
}
