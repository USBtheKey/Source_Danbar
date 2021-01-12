

using UnityEngine;

namespace GameSystem.Weapons.Projectiles
{
    public class SinWaveProjectile : Projectile
    {
        [SerializeField] private float amplitude;

        protected override void Move()
        {
            Vector2 newForwardPosition = (rigid.position.y + Speed * Time.fixedDeltaTime) * transform.up;
            Vector2 newSinPosition = (Vector2)(Mathf.Sin(Time.time) * amplitude * 50 * transform.right) * Time.fixedDeltaTime;

            rigid.MovePosition(newForwardPosition + newSinPosition);
        }
    }
}