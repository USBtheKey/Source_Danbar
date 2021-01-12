using UnityEngine;

namespace GameSystem.Weapons.Projectiles
{
    public class PlayerMissile : Missile
    {
        protected override void TargetSubscription()
        {
            OnCollisionHitTarget += LevelManager.Instance.PlayerLevelData.IncrementMissileHitTarget;
        }

        //Called by base class Fixed Update
        protected override void Move()
        {
            if (hasCollided) return;

            MoveForward();
            if (TargetT) if (TargetT.gameObject.activeSelf) Turn();
        }

        protected override void OnDisable()
        {
            if (!LevelManager.Instance) return;

            base.OnDisable();

            OnCollisionHitTarget -= LevelManager.Instance.PlayerLevelData.IncrementMissileHitTarget;
        }
    }
}