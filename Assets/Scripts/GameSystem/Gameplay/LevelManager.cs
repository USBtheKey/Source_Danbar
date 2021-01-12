
using System;
using System.Collections;
using UnityEngine;
using GameSystem.InputSystem;
using GameSystem.Statistics;

namespace GameSystem
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }
        [SerializeField] private int musicNumberToPlay;

        [SerializeField] private GameObject boss = null;

        [SerializeField] private float delayB4WarningDisplay = 10f;
        [SerializeField] private float delayB4BossBattle = 5f;

        [SerializeField] private SceneNames nextLevelName = SceneNames.Level_1_Scene;

        private SpawnGroup[] spawnGroups;

        private Player player;
        public PlayerData PlayerLevelData { get; private set; } = new PlayerData();

        private WaitForSeconds waitForLevelEnd = new WaitForSeconds(2f);

        public string GetNextLevelName => nextLevelName.ToString();

        private void Awake()
        {
            if (!GameManager.Instance) return;

            CreateInstance();

            spawnGroups = GetComponentsInChildren<SpawnGroup>(true);
        }

        private void OnEnable()
        {
            if (!GameManager.Instance) return;

            OnLevelFinished += InGameMenuManager.Instance.OpenStatsMenu;
            OnLevelFinished += DisableWinLoseAnnouncer;
            OnLevelFinished += InputsHandler.DisableInputs;

            OnGameOver += InputsHandler.EnableMouse;

            OnGameWon += InputsHandler.EnableMouse;

            GameSceneManagement.GameSceneLoader.OnStartLoadingNextScene += StopAllCoroutines;
            GameSceneManagement.GameSceneLoader.OnStartLoadingNextScene += NullifyInstance;
            GetPlayer();
        }

        private void Start()
        {
            StartCoroutine(LevelRoutine());

            IEnumerator LevelRoutine()
            {
                GameManager.SetGameState(GameplayState.InProgress);
                MusicManager.Instance.PlayClip(musicNumberToPlay);
                InputsHandler.EnableInputs();

                LevelUIManager.Instance.LevelStartAnnouncer.Play();

                yield return new WaitForSeconds(LevelUIManager.Instance.LevelStartAnnouncer.clip.length);

                foreach (SpawnGroup group in spawnGroups)
                {
                    group.gameObject.SetActive(true);

                    yield return new WaitForSeconds(group.DelayUntilNextSpawn);
                }

                Debug.Log("END OF LEVEL COROUTINE");

                yield return new WaitForSeconds(delayB4WarningDisplay);

                LevelUIManager.Instance.BossWarningAnnouncer.Play();

                yield return new WaitForSeconds(delayB4BossBattle);

                boss.SetActive(true);

                yield break;
            }
        }

        private void OnApplicationQuit()
        {
            StopAllCoroutines();
        }

        private void OnDisable()
        {
            if (!GameManager.Instance) return;

            OnLevelFinished -= InGameMenuManager.Instance.OpenStatsMenu;
            OnLevelFinished -= DisableWinLoseAnnouncer;

            OnGameOver -= InputsHandler.EnableMouse;
            OnGameOver -= InputsHandler.DisableInputs;

            OnGameWon -= InputsHandler.EnableMouse;
            OnGameWon -= InputsHandler.DisableInputs;

            GameSceneManagement.GameSceneLoader.OnStartLoadingNextScene -= StopAllCoroutines;
            GameSceneManagement.GameSceneLoader.OnStartLoadingNextScene -= NullifyInstance;

        }

        private void OnDestroy() //Quits the scene
        {
            NullifyInstance();

            SaveSystem.SavePlayerData(PlayerLevelData);
        }

        private void CreateInstance()
        {
            if (!Instance) Instance = this;
            else if (Instance != null && Instance != this)
            {
                Destroy(Instance);
                Instance = this;
            }
        }

        public void GameOver()
        {
            StopAllCoroutines();
            StartCoroutine(GameOverRoutine());

            IEnumerator GameOverRoutine()
            {
                InputsHandler.DisableInputs();
                OnGameOver?.Invoke();
                yield return waitForLevelEnd;

                GameManager.SetGameState(GameplayState.GameOver);

                LevelUIManager.Instance.WinLoseAnnouncer.enabled = true;
                LevelUIManager.Instance.WinLoseAnnouncer.text = "Game Over";

                yield return waitForLevelEnd;

                OnLevelFinished?.Invoke();

                yield break;
            }
        }

        public void GameWon()
        {
            UnlockLevel();
            StopAllCoroutines();
            
            StartCoroutine(GameWinRoutine());

            IEnumerator GameWinRoutine()
            {
                InputsHandler.DisableInputs();
                OnGameWon?.Invoke();
                
                yield return waitForLevelEnd;
                GameManager.SetGameState(GameplayState.GameWon);
                LevelUIManager.Instance.WinLoseAnnouncer.enabled = true;
                LevelUIManager.Instance.WinLoseAnnouncer.text = "Stage Cleared";
                yield return waitForLevelEnd;
                OnLevelFinished?.Invoke();

                yield break;
            }
        }

        private void UnlockLevel()
        {
            if (nextLevelName == SceneNames.Level_2_Scene)
            {
                PlayerLevelData.UnlockLevel1();
                PlayerLevelData.UnlockLevel2();
            }
            else if(nextLevelName == SceneNames.Level_3_Scene)
            {
                PlayerLevelData.UnlockLevel3();
            }
            else if (nextLevelName == SceneNames.CreditsScene)
            {
                PlayerLevelData.UnlockLevel4();
            }
        }

        public Player GetPlayer()
        {
            if (!player) player = FindObjectOfType<Player>();

            return player;
        }

        private void DisableWinLoseAnnouncer()
        {
            LevelUIManager.Instance.WinLoseAnnouncer.gameObject.SetActive(false);
        }

        private void NullifyInstance()
        {
            Instance = null;
        }

        public event Action OnGameWon;
        public event Action OnGameOver;
        public event Action OnLevelFinished;
    }
}