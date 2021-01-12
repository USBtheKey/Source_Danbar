
using UnityEngine;

namespace GameSystem
{
    public class ToMainMenuButton : MonoBehaviour
    {
        public void LoadMainMenu()
        {
            GameSceneManagement.GameSceneLoader.Instance.LoadMainMenu();
        }
    }
}
