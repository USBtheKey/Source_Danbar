

using UnityEngine;

namespace GameSystem.Weapons.Projectiles
{
    public abstract class TurnableProjectile : Projectile
    {
        [HideInInspector] public float TurnSpeed = 0;

        protected virtual void Turn()
        {
            transform.Rotate(transform.forward * TurnSpeed * Time.fixedDeltaTime);
        }
    }
}

