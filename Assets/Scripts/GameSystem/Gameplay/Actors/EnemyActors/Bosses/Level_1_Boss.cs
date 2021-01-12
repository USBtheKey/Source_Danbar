
using GameSystem.Weapons;
using GameSystem.Weapons.Projectiles;
using System.Collections;

using UnityEngine;

namespace GameSystem.Actors
{
    public class Level_1_Boss : EnemyBoss
    {
        [SerializeField] private Weapon[] wingsMountedGuns;
        [SerializeField] private Weapon noseMountedGun;
        [SerializeField] private Weapon[] bodyMountedRotatingGuns;

        private bool isLowHealth = false;

        public Weapon[] GetWingsWeapon => wingsMountedGuns;
        public Weapon[] GetRotatingGuns => bodyMountedRotatingGuns;
        public Weapon GetNoseWeapon => noseMountedGun;


        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (!health.IsAlive) return;

            if (collision.CompareTag(Utility.Tags.Player_Projectile))
            {
                health.TakeDamage(collision.GetComponent<Projectile>().Damage);
                CheckLowHealth();
            }
        }

        private void CheckLowHealth()
        {
            if (isLowHealth) return;

            if (health.Life <= 2 && health.HitpointsRatio < 0.5f)
            {
                isLowHealth = true;
                stateMachine.SetBool("IsLowHealth", true);
            }
        }
    }
}
