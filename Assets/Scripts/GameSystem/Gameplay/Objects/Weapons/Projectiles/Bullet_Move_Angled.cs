

using UnityEngine;

namespace GameSystem.Weapons.Projectiles
{
    public class Bullet_Move_Angled: Projectile
    {
        [SerializeField] private float horizontalSpeed = 0;
        protected override void Move()
        {
            rigid.MovePosition(rigid.position + ((((Vector2)transform.up) * Speed) + ((Vector2)transform.right) * horizontalSpeed) * Time.fixedDeltaTime);
        }
    }
}