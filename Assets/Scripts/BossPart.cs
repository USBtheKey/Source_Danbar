using GameSystem.Weapons;
using GameSystem.Weapons.Projectiles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Actors.Bosses
{
    [RequireComponent(typeof(Animation))]
    public class BossPart : MonoBehaviour
    {
        private HealthComponent health;
        private Animation anim;
        private Collider2D coll2D;
        [SerializeField] private Weapon[] children;


        private void Awake()
        {
            health = GetComponent<HealthComponent>();
            anim = GetComponent<Animation>();
            coll2D = GetComponent<Collider2D>();
        }

        private void OnEnable()
        {
            gameObject.layer = LayerMask.NameToLayer("Enemy");

            health.OnDeath += Destroy;

            coll2D.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!health.IsAlive) return;

            if (collision.CompareTag(Utility.Tags.Player_Projectile))
            {
                health.TakeDamage(collision.GetComponent<Projectile>().Damage);
               // Debug.Log($"{ gameObject.name } | {health.Hitpoints}", this);
            }
        }

        private void OnDisable()
        {
            health.OnDeath -= Destroy;
        }

        private void Destroy()
        {
            gameObject.layer = LayerMask.NameToLayer("IgnoreAll");
            if (children != null) if (children.Length > 0) foreach (Weapon wp in children) wp.HoldFire();
            anim.Play();

            StartCoroutine(SetDisableRoutine());

            IEnumerator SetDisableRoutine()
            {
                while (anim.isPlaying) yield return null;
                OnPartDestroyed?.Invoke();
                gameObject.SetActive(false);
                yield break;
            }
        }

        public event Action OnPartDestroyed;
    }
}
