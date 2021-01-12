using GameSystem.Weapons.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Actors.Bosses.Level2
{
    public class Level_2_Boss : EnemyBoss
    {
        private bool isLowHealth = false;



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

            isLowHealth = true;
            if (health.Life <= 2 && health.HitpointsRatio < 0.5f) stateMachine.SetBool("IsLowHealth", true);
        }

    }

}
