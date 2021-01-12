using System.Collections;
using GameSystem.Weapons;
using UnityEngine;

namespace GameSystem.Actors
{
    [RequireComponent(typeof(CheckpointMove))]

    public class HeavyCruiser: BaseEnemyActor, IWeaponized
    {
        private bool switcher = true;
        [SerializeField] private Weapon[] guns;
        [SerializeField] private MissilePod[] pods;
        private static WaitForSeconds waitForSwapWeapons;

        protected override void Awake()
        {
            base.Awake();

            waitForSwapWeapons = new WaitForSeconds(2f);
        }

        protected override void AddSubscribers()
        {
            Health.OnDeath += Death;

            Movement.OnWaypointChange += LookComp.LookTowards;
            Movement.OnFinalDestinationReached += Fire;
        }

        protected override void RemoveSubscribers()
        {
            Health.OnDeath -= Death;

            Movement.OnWaypointChange -= LookComp.LookTowards;
            Movement.OnFinalDestinationReached -= Fire;
        }

        public void Fire()
        {
            StartCoroutine(FireRoutine());

            IEnumerator FireRoutine()
            {
                if (switcher)
                {
                    foreach(Weapon gun in guns) gun.Fire();
                    foreach (MissilePod pod in pods) pod.HoldFire();
                }
                else
                {
                    foreach (Weapon gun in guns) gun.HoldFire();
                    foreach (MissilePod pod in pods) pod.Fire();
                    
                }

                yield return waitForSwapWeapons;
            }
        }

        public void HoldFire()
        {
            StopAllCoroutines();
        }
    }
}

