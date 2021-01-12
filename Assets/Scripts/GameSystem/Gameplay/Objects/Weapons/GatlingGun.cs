using GameSystem.Weapons.Projectiles;
using System;
using System.Collections;

using UnityEngine;

namespace GameSystem.Weapons
{
    public class GatlingGun : GunPod
    {
        public float FiringArc = 3f;

        protected override IEnumerator SpawnProjectileRoutine(float spreadAngle, float currentAngle, float upValueForCentering, float currentWaveGroupSpeed)
        {
            var halfFiringArc = FiringArc * 0.5f;

            var obj = GameObjectPooler.Spawn(
                projectile.name,
                transform.position,
                Quaternion.Euler(this.transform.rotation.eulerAngles + new Vector3(0, 0, UnityEngine.Random.Range(-halfFiringArc, halfFiringArc))));

            var proj = obj.GetComponent<Projectile>();
            proj.Speed = InitialWaveSpeed;
            proj.Target = targetTag;
            obj.SetActive(true);
            //if (overrideCollisionVFX) proj.CollisionVFX = overrideCollisionVFX;
            //if (overrideDestructionVFX) proj.DestructionVFX = overrideDestructionVFX;

            OnFire?.Invoke();

            yield break;
        }

        public override event Action OnFire;
    }
}
