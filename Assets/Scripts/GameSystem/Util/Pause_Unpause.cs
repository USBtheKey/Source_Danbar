

using UnityEngine;

namespace GameSystem.Utility
{
    public class Pause_Unpause : MonoBehaviour
    {
        private void OnEnable()
        {
            GameManager.Instance.PauseGame();
        }

        private void OnDisable()
        {
            GameManager.Instance.ResumeGame();
        }
    }

}
