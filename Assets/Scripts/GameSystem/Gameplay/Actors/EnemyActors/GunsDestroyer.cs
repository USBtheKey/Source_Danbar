using GameSystem.Weapons;
using UnityEngine;

namespace GameSystem.Actors
{
    public class GunsDestroyer : BaseEnemyActor, IWeaponized
    {
        [SerializeField] Weapon[] gunsGroup1;
        [SerializeField] Weapon[] gunsGroup2;

        protected override void AddSubscribers()
        {
            base.AddSubscribers();

            Movement.OnFiringPositionReached += Fire;
        }

        public void Fire()
        {
            foreach (Weapon gun in gunsGroup1) gun.Fire();
            foreach (Weapon gun in gunsGroup2) gun.Fire();
        }

        public void HoldFire()
        {
            StopAllCoroutines();
        }
    }
}

