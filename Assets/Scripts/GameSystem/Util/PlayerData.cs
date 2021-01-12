
namespace GameSystem.Statistics
{
    [System.Serializable]
    public class PlayerData
    {
        public bool Level1Unlocked { get; private set; } = false;
        public bool Level2Unlocked { get; private set; } = false;
        public bool Level3Unlocked { get; private set; } = false;
        public bool Level4Unlocked { get; private set; } = false;

        public int Deaths { get; private set; } = 0;
        public int BulletsFired { get; private set; } = 0;
        public int MissilesFired { get; private set; } = 0;
        public int BulletHitTarget { get; private set; } = 0;
        public int MissileHitTarget { get; private set; } = 0;
        public int BossesDestroyed { get; private set; } = 0;
        public int EnemyMinionsDestroyed { get; private set; } = 0;
        public int UltimateUsed { get; private set; } = 0;
        public int EnemyProjectileDestroyedByUltimate { get; private set; } = 0;
        public float ShotsAccuracy => BulletHitTarget / (float)BulletsFired;
        public float MissileAccuracy => MissileHitTarget / (float)MissilesFired;

        public void IncrementBulletHitTarget() => BulletHitTarget += 1;
        public void IncrementMissile() => MissilesFired += 1;
        public void IncrementBullets() => BulletsFired += 1;
        public void IncrementPlayerDeath() => Deaths += 1;
        public void IncrementMinionKilled() => EnemyMinionsDestroyed += 1;
        public void IncrementBossKilled() => BossesDestroyed += 1;
        public void IncrementUltimateUsed() => UltimateUsed += 1;
        public void IncrementMissileHitTarget() => MissileHitTarget += 1;
        public void IncrementEnemyProjectileDestroyedByUltimate() => EnemyProjectileDestroyedByUltimate += 1;

        public void UnlockLevel1() => Level1Unlocked = true;
        public void UnlockLevel2() => Level2Unlocked = true;
        public void UnlockLevel3() => Level3Unlocked = true;
        public void UnlockLevel4() => Level4Unlocked = true;
        public int Score =>
                BulletHitTarget
                + MissileHitTarget
                + (BossesDestroyed * 2500)
                + (EnemyMinionsDestroyed * 100)
                + (EnemyProjectileDestroyedByUltimate * 50)
                - (Deaths * 1000);

        public void Add(PlayerData data)
        {
            Deaths += data.Deaths;
            BulletsFired += data.BulletsFired;
            MissilesFired += data.MissilesFired;
            BulletHitTarget += data.BulletHitTarget;
            MissileHitTarget += data.MissileHitTarget;
            BossesDestroyed += data.BossesDestroyed;
            EnemyMinionsDestroyed += data.EnemyMinionsDestroyed;
            UltimateUsed += data.UltimateUsed;
            EnemyProjectileDestroyedByUltimate += data.EnemyProjectileDestroyedByUltimate;
            Level1Unlocked = data.Level1Unlocked;
            Level2Unlocked = data.Level2Unlocked;
            Level3Unlocked = data.Level3Unlocked;
            Level4Unlocked = data.Level4Unlocked;
        }

        public override string ToString()
        {
            return
                $"Score {BulletHitTarget} \n" +
                $"Deaths {Deaths} \n" +
                $"Bullet Fired {BulletsFired} \n" +
                $"Bullet Hit Fired {BulletHitTarget} \n" +
                $"Missile Fired {MissilesFired} \n" +
                $"Missile Hit Fired {MissileHitTarget} \n" +
                $"Bosses Destroyed {BossesDestroyed} \n" +
                $"Enemy Minions Destroyed {EnemyMinionsDestroyed} \n" +
                $"Ultimate Used {UltimateUsed} \n" +
                $"Enemy Projectile Destroyed by Ultimate {EnemyProjectileDestroyedByUltimate} \n"  +
                $"Shots Accuracy {ShotsAccuracy} \n" +
                $"Missile Accuracy {MissileAccuracy}";
        }
    }
}

