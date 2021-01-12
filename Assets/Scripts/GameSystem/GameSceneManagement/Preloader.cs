using System.Collections;
using UnityEngine;

namespace GameSystem.GameSceneManagement 
{
    public class Preloader : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(PreloderRoutine());

            IEnumerator PreloderRoutine()
            {
                yield return new WaitForSeconds(3f);
                GameSceneLoader.Instance.LoadSceneName($"{SceneNames.MainMenuScene}");
            }
        }
    }
}