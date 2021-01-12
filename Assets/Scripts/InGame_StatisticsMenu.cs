using GameSystem.GameSceneManagement;
using UnityEngine;
using TMPro;

namespace GameSystem
{
    public class InGame_StatisticsMenu : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI titleText;

        [SerializeField]
        private TextMeshProUGUI enemyDestroyedText;

        [SerializeField]
        private TextMeshProUGUI bossDestroyedText;

        [SerializeField] 
        private TextMeshProUGUI bulletShotText;

        [SerializeField]
        private TextMeshProUGUI bulletHitTargetText;

        [SerializeField]
        private TextMeshProUGUI bulletsAccuracyText;

        [SerializeField]
        private TextMeshProUGUI missileFiredText;

        [SerializeField]
        private TextMeshProUGUI missileHitTargetText;

        [SerializeField]
        private TextMeshProUGUI missileAccuracyText;

        [SerializeField]
        private TextMeshProUGUI projectileDestroyedByUltimate;

        [SerializeField]
        private TextMeshProUGUI numberOfCloseCallsText;

        [SerializeField]
        private TextMeshProUGUI scoreText;

        [SerializeField]
        private MenuButton continueButton;

        [SerializeField]
        private MenuButton mainMenuButton;

        private void Awake()
        {
            mainMenuButton.Button.onClick.AddListener(GameSceneLoader.Instance.LoadMainMenu);
        }

        private void OnEnable()
        {
            if (!LevelManager.Instance) return;
               
            var gameplayState = GameManager.PlayState;

            if(gameplayState == GameplayState.GameOver)
            {
                titleText.text = "Game Over";
                continueButton.Button.onClick.AddListener(GameSceneLoader.Instance.ReloadCurrentScene);
                continueButton.Label.text = "Retry";
            }
            else if(gameplayState == GameplayState.GameWon)
            {
                continueButton.Button.onClick.AddListener(GameSceneLoader.Instance.LoadNextLevel);
                titleText.text = "Stage Cleared";
                continueButton.Label.text = "Continue";
            }
            else
            {
                Debug.LogError(nameof(InGame_StatisticsMenu) + " Not a valid GameplayState.", this);
            }

            enemyDestroyedText.text = LevelManager.Instance.PlayerLevelData.EnemyMinionsDestroyed.ToString();
            bossDestroyedText.text = LevelManager.Instance.PlayerLevelData.BossesDestroyed.ToString();
            bulletShotText.text = LevelManager.Instance.PlayerLevelData.BulletsFired.ToString();
            bulletHitTargetText.text = LevelManager.Instance.PlayerLevelData.BulletHitTarget.ToString();
            missileFiredText.text = LevelManager.Instance.PlayerLevelData.MissilesFired.ToString();
            missileHitTargetText.text = LevelManager.Instance.PlayerLevelData.MissileHitTarget.ToString();
            projectileDestroyedByUltimate.text = LevelManager.Instance.PlayerLevelData.EnemyProjectileDestroyedByUltimate.ToString();
            numberOfCloseCallsText.text = "MIA";
            scoreText.text = LevelManager.Instance.PlayerLevelData.Score.ToString();

            var acc = LevelManager.Instance.PlayerLevelData.ShotsAccuracy;
            bulletsAccuracyText.text = acc == float.NaN? "--,--%": $"{acc:P}";

            acc = LevelManager.Instance.PlayerLevelData.MissileAccuracy;
            missileAccuracyText.text = acc == float.NaN ? "--,--&" : $"{acc:P}";
        }

        private void OnDisable()
        {
            if (!LevelManager.Instance) return;

            continueButton.Button.onClick.RemoveAllListeners();
        }
    }

}
