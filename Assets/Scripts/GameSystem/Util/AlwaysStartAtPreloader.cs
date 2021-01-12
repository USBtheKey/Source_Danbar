
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem.Utility
{
    public class AlwaysStartAtPreloader : MonoBehaviour
    {
        private void Awake()
        {
            string currSceneName = SceneManager.GetActiveScene().name;

            if (currSceneName != "PreloaderScene" && !GameManager.Instance)
            {
#if UNITY_EDITOR
                Debug.Log("Scene Started at " + currSceneName + ". Going to Preloader...");
#endif
                SceneManager.LoadScene("PreloaderScene");
            }
        }
    }
}
