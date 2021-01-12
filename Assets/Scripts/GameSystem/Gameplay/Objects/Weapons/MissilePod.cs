using GameSystem.Weapons.Projectiles;
using UnityEngine;
using System;
using System.Collections;

namespace GameSystem.Weapons
{
    public class MissilePod : Weapon
    {
        public float missileTurnSpeed = 720f;

        protected override IEnumerator FireRoutine(Transform target)
        {
            int waveGroupCounter = 0;

            while (true)
            {
                for (int j = 0; j < numWavesPerGroup; j++)
                {
                    GameObject obj = GameObjectPooler.Spawn(projectile.name, transform.position, Quaternion.Euler(transform.rotation.eulerAngles));
                    var missile = obj.GetComponent<Missile>();
                    missile.TargetT = null; // Clears wtv it was before
                    missile.TargetT = target; // put new data

                    var proj = obj.GetComponent<TurnableProjectile>();
                    proj.Speed = InitialWaveSpeed;
                    proj.Target = targetTag;
                    proj.TurnSpeed = missileTurnSpeed;

                    //if (overrideCollisionVFX) proj.CollisionVFX = overrideCollisionVFX;
                    //if (overrideDestructionVFX) proj.DestructionVFX = overrideDestructionVFX;
                    obj.SetActive(true);

                    OnFire?.Invoke();
                    
                    if (j < numWavesPerGroup - 1) yield return waitForDelayBetweenWave;
                }

                if (numWaveGroups > 0)
                {
                    if (++waveGroupCounter >= numWaveGroups)
                    {
                        fireCoroutine = null;
                        yield break;
                    }
                }

                yield return waitForWaveGroupDelay;
            }
        }

        public override event Action OnFire;
    }
}

