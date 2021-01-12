using GameSystem.Weapons;
using UnityEngine;

namespace GameSystem.Actors
{
    [RequireComponent(typeof(CheckpointMove))]
    public class GunMissileDestroyer : BaseEnemyActor, IWeaponized
    {
        [SerializeField] Weapon gun;
        [SerializeField] MissilePod[] pods;

        protected override void RemoveSubscribers()
        {
            base.RemoveSubscribers();

            Movement.OnFiringPositionReached -= Fire;
        }

        protected override void AddSubscribers()
        {
            base.AddSubscribers();

            Movement.OnFiringPositionReached += Fire;

            GameSceneManagement.GameSceneLoader.OnStartLoadingNextScene += HoldFire;
        }

        public void Fire()
        {
            gun.Fire();
            foreach (MissilePod pod in pods) pod.Fire();
        }

        public void HoldFire()
        {
            StopAllCoroutines();
        }
    }
}

