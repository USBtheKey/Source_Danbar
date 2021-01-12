

using GameSystem.GameSceneManagement;
using System;
using System.Collections;
using UnityEngine;

namespace GameSystem
{
    public class GameManager : MonoBehaviour, ISceneStarted
    {
        public static GameplayState PlayState { get; private set; } = GameplayState.InProgress;
        public static GameManager Instance { get; private set; }
        private static GameSettings settings;


        private void Awake()
        {
            CreateInstance();

            settings = GameSettings.GetInstance();

            void CreateInstance()
            {
                if (!Instance) Instance = this;
                else Destroy(this.gameObject);
            }
        }

        private void OnApplicationQuit()
        {
            //GameData.Save_Current_Data();
        }

        public void OnSceneStarted()
        {
            if (!LevelManager.Instance) return;

            ResumeGame();
        }

        public void PauseGame()
        {
            OnGamePause?.Invoke();
            PlayState = GameplayState.Paused;
            Time.timeScale = 0f;

            MusicManager.Instance.OnPausePlay();
        }

        public void ResumeGame()
        {
            MusicManager.Instance.OnUnpausePlay();

            OnGameResume?.Invoke();
            PlayState = GameplayState.InProgress;
            Time.timeScale = 1f;

            StartCoroutine(ResumeGameRoutine());

            IEnumerator ResumeGameRoutine()
            {
                Time.timeScale = 0.7f;
                yield return new WaitForSeconds(0.3f);
                Time.timeScale = 1f;
            }
        }

        public void ExitApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public static void SetGameState(GameplayState state)
        {
            PlayState = state;
            OnStateChange?.Invoke(PlayState);

            if (state == GameplayState.None) Time.timeScale = 1f;
        }
       

        public static event Action<GameplayState> OnStateChange;
        public static event Action OnGamePause;
        public static event Action OnGameResume;
    }
}