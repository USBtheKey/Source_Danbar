#undef DO_NOT_INCLUDE

#if DO_NOT_INCLUDE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem.Utility;


namespace GameSystem.Drops
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Drops : MonoBehaviour
    {

        protected Rigidbody2D rigid = null;
        [SerializeField] protected float force = 1000f;
        [SerializeField] private float despawnTimer = 5f;

        private Coroutine despawnRoutine;

        // Start is called before the first frame update
        private void OnEnable()
        {
            despawnRoutine = StartCoroutine(EnumDespawn());

            rigid.AddForce(Util.RandomDirectionV2() * force, ForceMode2D.Force);
        }

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        protected void SetDespawnTimer(float timeToDespawn) => despawnTimer = timeToDespawn;

        protected void SetDropsForce(float force) => this.force = force;

        private void OnDisable()
        {
            if (despawnRoutine != null) StopCoroutine(despawnRoutine);
        }

        private IEnumerator EnumDespawn()
        {
            yield return new WaitForSeconds(despawnTimer);
            this.gameObject.SetActive(false);
        }
    }
}
#endif