
using UnityEngine;

namespace GameSystem
{
    public class InGameMenuManager : MonoBehaviour
    {
        public static InGameMenuManager Instance { get; private set; }
        public InGame_PauseMenu PauseMenu { get; private set; }
        public InGame_StatisticsMenu StatsMenu { get; private set; }
        public InGame_VolumeMenu VolumeMenu { get; private set; }

        private void Awake()
        {
            CreateSingleton();

            PauseMenu = GetComponentInChildren<InGame_PauseMenu>(true);
            StatsMenu = GetComponentInChildren<InGame_StatisticsMenu>(true);
            VolumeMenu = GetComponentInChildren<InGame_VolumeMenu>(true);

            void CreateSingleton()
            {
                if (!Instance) Instance = this;
                else Destroy(gameObject);
            }
        }

        public void OpenStatsMenu()
        {
            if (!LevelManager.Instance) return;

            StatsMenu.gameObject.SetActive(true);
        }

        public void OpenVolumeMenu()
        {
            if (!LevelManager.Instance) return;

            VolumeMenu.gameObject.SetActive(true);
        }

        public void TogglePauseMenu()
        {
            if (!LevelManager.Instance) return;

            if (PauseMenu.gameObject.activeSelf)
            {
                PauseMenu.gameObject.SetActive(false);
                GameManager.Instance.ResumeGame();
            }
            else
            {
                PauseMenu.gameObject.SetActive(true);
                GameManager.Instance.PauseGame();
            }
        }
    }
}

