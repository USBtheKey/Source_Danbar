using UnityEngine;
using TMPro;
using GameSystem.Statistics;

namespace GameSystem
{
    public class MainMenuStatsPanel : MonoBehaviour
    {
        private PlayerData data;

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

        private void Awake()
        {
            data = SaveSystem.LoadPlayerData();
        }

        private void OnEnable()
        {
            enemyDestroyedText.text = data.EnemyMinionsDestroyed.ToString();
            bossDestroyedText.text = data.BossesDestroyed.ToString();
            bulletShotText.text =  data.BulletsFired.ToString();
            bulletHitTargetText.text =  data.BulletHitTarget.ToString();
            missileFiredText.text =  data.MissilesFired.ToString();
            missileHitTargetText.text =  data.MissileHitTarget.ToString();
            projectileDestroyedByUltimate.text =  data.EnemyProjectileDestroyedByUltimate.ToString();

            var acc =  data.ShotsAccuracy;
            bulletsAccuracyText.text = acc == float.NaN ? "--,--%" : $"{acc:P}";

            acc =  data.MissileAccuracy;
            missileAccuracyText.text = acc == float.NaN ? "--,--&" : $"{acc:P}";
        }
    }
}
