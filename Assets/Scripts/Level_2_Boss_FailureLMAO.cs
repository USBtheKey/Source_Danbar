using GameSystem.Weapons;
using GameSystem.Weapons.Projectiles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Actors.Bosses
{
    public class Level_2_Boss_FailureLMAO : EnemyBoss
    {
        private enum State { FastAttackEnter, SpinAttack, CornerAttack };
        [SerializeField] private BossPart rightWing;
        [SerializeField] private BossPart leftWing;
        [SerializeField] private BossPart headArmor;

        private Animator animator;

        private int bodyPartsCount = 3;
        [SerializeField] private Weapon[] centerWeapons;
        [SerializeField] private Weapon[] wingsWeapon;
        private bool isLowHealth = false;

        protected override void Awake()
        {
            base.Awake();

            animator = GetComponent<Animator>();

        }

        protected override void OnEnable()
        {
            if (!LevelManager.Instance) return;

            base.OnEnable();
            rightWing.OnPartDestroyed += () => RemoveBodyPart(ref rightWing);
            leftWing.OnPartDestroyed += () => RemoveBodyPart(ref leftWing);
            headArmor.OnPartDestroyed += () => RemoveBodyPart(ref headArmor);

            Array.ForEach(wingsWeapon, weap => weap.Fire());
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

        private void RemoveBodyPart(ref BossPart part)
        {
            part = null;

            if (--bodyPartsCount <= 1)
            {
                animator.SetTrigger("LowHealth");
            }
        }

        public void Fire()
        {
            Array.ForEach(centerWeapons, weap => weap.Fire());
        }

        private void CheckLowHealth()
        {
            if (isLowHealth) return;


            if (health.Life <= 2)
            {
                isLowHealth = true;
                stateMachine.SetBool("LowHealth", true);
            }
        }
    }
}
