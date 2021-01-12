using GameSystem.Weapons;
using GameSystem.Weapons.Projectiles;
using System.Collections;
using UnityEngine;

namespace GameSystem
{
    public class DetachableGuns : MonoBehaviour, ISceneObject
    {
        [SerializeField] private Transform maxTopRight;
        [SerializeField] private Transform minBottomLeft;
        [SerializeField] private GameObject destroyedVFX;

        private Collider2D coll;
        private WeaponTracker weapTrack;
        private GunPod gun;
        private Rigidbody2D rigid;
        private Vector3 destination;
        [SerializeField] private float moveSpeedTime = 1f;
        public HealthComponent Health;
        public bool Arrived => (destination - transform.position).magnitude < 50f;

        private void Awake()
        {
            coll = GetComponent<Collider2D>();
            gun = GetComponentInChildren<GunPod>(true);
            rigid = GetComponent<Rigidbody2D>();
            weapTrack = GetComponent<WeaponTracker>();
            Health = GetComponent<HealthComponent>();
        }

        private void OnEnable()
        {
            GameSceneManagement.GameSceneLoader.OnStartLoadingNextScene += DisableObj;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!Health.IsAlive) return;

            if (collision.CompareTag(Utility.Tags.Player_Projectile))
            {
                Health.TakeDamage(collision.GetComponent<Projectile>().Damage);
                if (!Health.IsAlive)
                {
                    GameObjectPooler.Spawn(destroyedVFX.name, transform.position).SetActive(true);
                    DisableObj();
                }
            }
        }

        private void OnDisable()
        {
            GameSceneManagement.GameSceneLoader.OnStartLoadingNextScene -= DisableObj;
        }

        public void Attack()
        {
            weapTrack.enabled = true;
            coll.enabled = true;

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

        private void Fire()
        {
            gun.Fire();
        }

        private void MoveToDestination()
        {
            StartCoroutine(MoveRoutine());

            IEnumerator MoveRoutine()
            {
                var currentPosition = transform.position;

                for(float i = 0; i <= moveSpeedTime; i += Time.deltaTime)
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

        public void DisableObj()
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
            gun.HoldFire();
        }
    }
}

