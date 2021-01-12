using GameSystem.GameSceneManagement;
using UnityEngine;
using TMPro;

namespace GameSystem
{

    public class LevelUIManager : MonoBehaviour, ISceneDestroyed
    {
        public static LevelUIManager Instance { get; private set; }

        public UI_BossHealthBar UI_BossHealthBar { get; private set; }

        [SerializeField] private Animation levelStartAnnouncer;
        [SerializeField] private Animation bossWarningAnnouncer;
        [SerializeField] private TextMeshProUGUI winLoseAnnouncer;

        public Animation LevelStartAnnouncer { get => levelStartAnnouncer; }
        public Animation BossWarningAnnouncer { get => bossWarningAnnouncer; }
        public TextMeshProUGUI WinLoseAnnouncer { get => winLoseAnnouncer; }

        private void Awake()
        {
            CreateInstance();

            void CreateInstance()
            {
                if (!Instance) Instance = this;
                else Destroy(this.gameObject);
            }

            UI_BossHealthBar = GetComponentInChildren<UI_BossHealthBar>(true);
        }

        public void OnSceneDestroyed()
        {
            winLoseAnnouncer.enabled = false;
            UI_BossHealthBar.DisableChildren();

            levelStartAnnouncer.Rewind();
            levelStartAnnouncer.Sample();
            levelStartAnnouncer.Stop();

            bossWarningAnnouncer.Rewind();
            bossWarningAnnouncer.Sample();
            bossWarningAnnouncer.Stop();
        }
    }
}
