
using System.Collections;
using UnityEngine;


namespace GameSystem.Weapons.Projectiles
{
    public abstract class Missile : TurnableProjectile
    {
        [SerializeField] private float countdownTime = 10f;
        [HideInInspector] public Transform TargetT;
        private TrailRenderer tRend = null;

        protected override void OnEnable()
        {
            if (!LevelManager.Instance) return;

            base.OnEnable();
            Countdown(()=> DespawnRoutine());
        }

        protected override void Start()
        {
            base.Start();

            tRend.startWidth = this.transform.localScale.x + 5;
            tRend.endWidth = 0;
        }

        protected override void GetComponents()
        {
            base.GetComponents();

            tRend = GetComponentInChildren<TrailRenderer>(true);
        }

        protected override void Turn()
        {
            Vector2 direction = TargetT.position - this.transform.position;

#if UNITY_EDITOR
            Debug.DrawLine(this.transform.position, TargetT.position);
            Debug.DrawLine(this.transform.position, (Vector2)transform.position + direction, Color.red);
#endif

            float rotation = Vector3.Cross(direction.normalized, transform.up).z;

            this.transform.RotateAround(this.transform.position, transform.forward, -rotation * TurnSpeed * Time.fixedDeltaTime);
        }

        protected override IEnumerator DespawnRoutine()
        {
            gameObject.layer = LayerMask.NameToLayer("IgnoreAll");
            sRend.enabled = false;
            coll2D.enabled = false;

            yield return new WaitForSeconds(tRend.time + 5);

            TargetT = null;

            yield return base.DespawnRoutine(); // Back To pooler

            yield break;
        }

        protected override IEnumerator TriggerDespawnRoutine()
        {
            gameObject.layer = LayerMask.NameToLayer("IgnoreAll");

            coll2D.enabled = false;

            yield return waitForTriggerDespawn;

            sRend.enabled = false;

            yield return new WaitForSeconds(tRend.time + 5);

            TargetT = null;

            gameObject.transform.parent = BulletPooler.GetInstance().transform;
            gameObject.SetActive(false);
            yield break;
        }

        protected void Countdown(System.Action Reached)
        {
            StartCoroutine(CountdownRoutine());

            IEnumerator CountdownRoutine()
            {
                var currTime = countdownTime;
                while (currTime > 0) 
                {
                    currTime -= Time.deltaTime;
                    yield return null;
                }
                Reached?.Invoke();
                TargetT = null;
            }
        }
    }
}
