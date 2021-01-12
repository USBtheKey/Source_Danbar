using GameSystem.Weapons;
using GameSystem.Weapons.Projectiles;
using System;
using System.Collections;
using UnityEngine;

namespace GameSystem.Actors.Bosses
{
    public class Level_3_Boss: EnemyBoss
    {
        private enum State { FastAttackEnter, SpinAttack, CornerAttack };

        [SerializeField] private Transform maxTopRight;
        [SerializeField] private Transform minBottomLeft;
        [SerializeField] private GameObject secondGun;
        [SerializeField] private float moveSpeedTime = 1f;
        private Vector3 destination;

        [SerializeField] private GunPod gun;
        [SerializeField] private DetachableGuns[] detachableGuns;
        private int counter = 4;
        private bool isLowHealth = false;
        public bool Arrived => (destination - transform.position).magnitude < 100f;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void OnEnable()
        {
            if (!LevelManager.Instance) return;

            base.OnEnable();

            Attack();

            foreach(DetachableGuns dg in detachableGuns)
            {
                dg.Health.OnDeath += CheckRemainingGuns;
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (!health.IsAlive) return;

            if (collision.CompareTag(Utility.Tags.Player_Projectile))
            {
                health.TakeDamage(collision.GetComponent<Projectile>().Damage);
                CheckLowHealth();
            }
        }

        public void Attack()
        {
            StartCoroutine(AttackRoutine());

            IEnumerator AttackRoutine()
            {
                PickMoveDestination();

                while (true)
                {
                    if (!Arrived) MoveToDestination();
                    else // Arrived at destination
                    {
                        Fire();
                        PickMoveDestination();
                    }
                    yield return null;
                }
            }
        }

        [ContextMenu("DetachGuns")]
        public void DetachGuns()
        {
            Array.ForEach(detachableGuns, g => g.Attack());
        }

        private void Fire()
        {
            if (gun.gameObject.activeSelf) gun.Fire();
            else if (secondGun.activeSelf) secondGun.GetComponent<GunPod>().Fire();
        }

        private void MoveToDestination()
        {
            StartCoroutine(MoveRoutine());

            IEnumerator MoveRoutine()
            {
                var currentPosition = transform.position;

                for (float i = 0; i <= moveSpeedTime; i += Time.deltaTime)
                {
                    transform.position = Vector2.Lerp(currentPosition, destination, i / moveSpeedTime);
                    yield return null;
                }
            }
        }

        private void PickMoveDestination()
        {
            float x;
            float y;

            do
            {
                x = UnityEngine.Random.Range(minBottomLeft.position.x, maxTopRight.position.x);
            }
            while (x == destination.x);

            do
            {
                y = UnityEngine.Random.Range(minBottomLeft.position.y, maxTopRight.position.y);
            } while (y == destination.y);

            destination = new Vector2(x, y);
        }

        private void CheckLowHealth()
        {
            if (isLowHealth) return;
            
            if (health.Life <= 3)
            {
                isLowHealth = true;
                DetachGuns();
            }
        }

        private void CheckRemainingGuns()
        {
            counter--;
            if (counter <= 0)
            {
                gun.gameObject.SetActive(false);
                secondGun.SetActive(true);
            }
        }
    }
}
