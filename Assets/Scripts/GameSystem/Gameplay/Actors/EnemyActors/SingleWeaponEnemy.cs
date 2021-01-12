using GameSystem.Weapons;

namespace GameSystem.Actors
{
    public class SingleWeaponEnemy : BaseEnemyActor, IWeaponized
    {
        protected Weapon gun;

        protected override void AddSubscribers()
        {
            base.AddSubscribers();

            Movement.OnFiringPositionReached += Fire;
        }

        protected override void GetComponents()
        {
            base.GetComponents();

            gun = GetComponentInChildren<Weapon>(true);
        }

        public void Fire()
        {
            if(gun) gun.Fire();
        }

        public void HoldFire()
        {
            if(gun) gun.HoldFire();
        }
    }
}

