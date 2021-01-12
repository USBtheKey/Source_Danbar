
using UnityEngine;

namespace GameSystem
{
    public class ResumeGameButton : MonoBehaviour
    {
        public void Resume()
        {
            if (!GameManager.Instance)
            {
                Debug.LogError("Missing Game Manager!", this);
                return;
            }

            GameManager.Instance.ResumeGame();

        }
    }
}
