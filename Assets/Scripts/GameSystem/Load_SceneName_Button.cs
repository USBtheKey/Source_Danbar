//TEST
using GameSystem.GameSceneManagement;
using UnityEngine;

namespace GameSystem.Utility
{
    /// <summary>
    /// Used mostly for Unity Button OnClick(). "Extends the GameManager SceneManagement methods."
    /// </summary>
    public class Load_SceneName_Button : MonoBehaviour
    {
        public void ReloadCurrentScene()
        {
            GameSceneLoader.Instance.ReloadCurrentScene();
        }

        public void LoadMainMenu()
        {
            GameSceneLoader.Instance.LoadMainMenu();
        }

        /// <summary>
        /// Loads a specific scene.
        /// </summary>
        /// <param name="sceneName">The scene name you want to load.</param>
        public void LoadScene(string sceneName)
        {
            GameSceneLoader.Instance.LoadSceneName(sceneName);
        }

        /// <summary>
        /// Grabs the "next level name" from the Level Manager instance.
        /// </summary>
        public void LoadNextLevel()
        {
            GameSceneLoader.Instance.LoadNextLevel();
        }
    }
}

