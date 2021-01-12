using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;


namespace GameSystem.GameSceneManagement
{
    public class GameSceneLoader : MonoBehaviour, ISceneStarted
    {
        public static GameSceneLoader Instance;

        private AsyncOperation async;
        private WaitForSeconds waitForFadeTransition = new WaitForSeconds(SceneTransition.FadeTime);

        public float LoadingProgress { get; private set; } = 0;

        public bool MainMenu_LoadingIsDone => async.isDone;

        public bool LoadingIsDone => async.isDone;

        private void Awake()
        {
            CreateInstance();

            void CreateInstance()
            {
                if (!Instance) Instance = this;
                else Destroy(this.gameObject);
            }
        }

        public void OnSceneStarted()
        {
            if (SceneTransition.Instance) SceneTransition.Instance.FadeIn();
            else Debug.LogError("Scene Fade In instance is null.");
        }

        public void LoadSceneName(string sceneName)
        {
            GameManager.SetGameState(GameplayState.None);

            OnStartLoadingNextScene?.Invoke();

            StartCoroutine(LoadSceneRoutine());

            IEnumerator LoadSceneRoutine()
            {
                async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

                async.allowSceneActivation = false;

                while (async.progress < 0.9f)
                {
                    LoadingProgress = async.progress / 0.9f;
                    yield return null;
                }

                yield return waitForFadeTransition;

                OnSceneTransitioning?.Invoke();

                async.allowSceneActivation = true;

                yield break;
            }
        }

        /// <summary>
        /// Grabs the "next level name" from the Level Manager instance.
        /// </summary>
        public void LoadNextLevel()
        {
            if (LevelManager.Instance) LoadSceneName(LevelManager.Instance.GetNextLevelName);
            else Debug.LogError("Level Manager is null.", this);
        }
        public void ReloadCurrentScene()
        {
            LoadSceneName(SceneManager.GetActiveScene().name);
        }

        public void LoadMainMenu()
        {
            LoadSceneName("MainMenuScene");
        }

        /// <summary>
        /// Invoked when the next scene has been loaded
        /// and just before the next scene gets activated.
        /// </summary>
        public static event Action OnSceneTransitioning;

        public static event Action OnStartLoadingNextScene;
    }
}