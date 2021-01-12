using System;
using System.Collections;
using GameSystem.Weapons.Projectiles;
using UnityEngine;

namespace GameSystem.Weapons
{
    public class GunPod : Weapon
    {
        public float FuseTime = 1f;

        protected override IEnumerator FireRoutine()
        {
            var groupCounter = 0;
            var currentWaveSpeed = InitialWaveSpeed;
            var deltaWavesSpeed = (float)Math.Round((lastWaveSpeed - InitialWaveSpeed) / (numWavesPerGroup - 1));

            var angleBetweenProj = centralAngle == 360 ? numProjectilesPerWave : numProjectilesPerWave - 1;
            var spreadAngle = centralAngle / (angleBetweenProj < 1 ? 1 : angleBetweenProj);
            var upValueForCentering = -(centralAngle * 0.5f);


            yield return new WaitForSeconds(initialDelay);

            while (true)
            {
#if UNITY_EDITOR //To Facilitate Testing
                angleBetweenProj = centralAngle == 360 ? numProjectilesPerWave : numProjectilesPerWave - 1;
                spreadAngle = centralAngle / (angleBetweenProj < 1 ? 1 : angleBetweenProj);
                upValueForCentering = -(centralAngle * 0.5f);
#endif

                for (int j = 0; j < numWavesPerGroup; j++) // Wave Group
                {
                    for (int currentAngle = 0; currentAngle <= angleBetweenProj; currentAngle++) // Wave
                    {
                        if (centralAngle == 360 && currentAngle >= angleBetweenProj) break;

                        yield return SpawnProjectileRoutine(spreadAngle, currentAngle, upValueForCentering, currentWaveSpeed);

                        if (audioS) audioS.Play();
                        OnFire?.Invoke();

                        if (centralAngle == 0) break;
                    }

                    if (!(lastWaveSpeed <= 0))
                    {
                        var nextWaveSpeed = currentWaveSpeed + deltaWavesSpeed;

                        if ((lastWaveSpeed - InitialWaveSpeed) > 0) 
                            currentWaveSpeed = (nextWaveSpeed > lastWaveSpeed) ? InitialWaveSpeed : nextWaveSpeed;
                        else 
                            currentWaveSpeed = (nextWaveSpeed < lastWaveSpeed) ? InitialWaveSpeed : nextWaveSpeed;
                    }

                    if (j < numWavesPerGroup - 1) yield return waitForDelayBetweenWave;
                }

                if (numWaveGroups > 0)
                {
                    if (++groupCounter >= numWaveGroups)
                    {
                        fireCoroutine = null;
                        yield break;
                    }
                }
                yield return waitForWaveGroupDelay;
            }
        }

        protected override IEnumerator SpawnProjectileRoutine(float spreadAngle, float currentAngle, float upValueForCentering, float currentWaveGroupSpeed)
        {
            var obj = GameObjectPooler.Spawn(projectile.name,
                transform.position + transform.up * upOffset,
                Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, spreadAngle * currentAngle + (upValueForCentering - offset))));
            //obj.SetActive(false);
            var proj = obj.GetComponent<Projectile>();
            proj.Speed = currentWaveGroupSpeed;
            proj.Target = targetTag;
            if (obj.GetComponent<FragmentedProjectile>()) obj.GetComponent<FragmentedProjectile>().FuseTime = FuseTime;
            //if (overrideCollisionVFX) proj.CollisionVFX = overrideCollisionVFX;
            if (overrideDestructionVFX) proj.DestructionVFX = overrideDestructionVFX;
            obj.SetActive(true);
            yield break;
        }

        public override event Action OnFire;
    }
}
