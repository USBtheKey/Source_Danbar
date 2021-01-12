
using System.Collections;
using UnityEngine;


namespace GameSystem.Weapons.Projectiles
{
    public class EnemyMissile : Missile
    {
        protected override void TargetSubscription()
        {
            OnDestroyedByUltimate += LevelManager.Instance.PlayerLevelData.IncrementEnemyProjectileDestroyedByUltimate;
        }

        protected override void Move()
        {
            if (hasCollided) return;

            if (TargetT)
            {
                MoveForward();

                if (TargetT.gameObject.activeSelf) Turn();
            }
        }

        protected override void OnDisable()
        {
            if (!LevelManager.Instance) return;

            OnCollisionHitTarget -= LevelManager.Instance.PlayerLevelData.IncrementMissileHitTarget;
        }
    }
}