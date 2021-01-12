using UnityEngine;
using System.Collections;

namespace GameSystem.Weapons.Projectiles
{
    [RequireComponent(typeof(GunPod))]
    public class FragmentedProjectile : Projectile
    {
        public float FuseTime = 1f;
        private GunPod gun;
        private Coroutine explosionCoroutine;

        protected override void Awake()
        {
            base.Awake();

            gun = GetComponent<GunPod>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (explosionCoroutine == null)
            {
                explosionCoroutine = StartCoroutine(Explode());
            }
        }

        protected override void Move()
        {
            if (hasCollided) return;

            MoveForward();
        }

        protected virtual IEnumerator Explode()
        {
            yield return new WaitForSeconds(FuseTime);
            hasCollided = true;
            sRend.enabled = false;
            gun.Fire();
            yield return new WaitForSeconds(1f);
            DisableObj();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            StopAllCoroutines();
            explosionCoroutine = null;
        }
    }
}

