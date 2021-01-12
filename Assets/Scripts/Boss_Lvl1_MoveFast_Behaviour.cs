using GameSystem.Weapons;

using UnityEngine;

namespace GameSystem.Actors.Bosses.Level1.Behavior
{
    public class Boss_Lvl1_MoveFast_Behaviour : StateMachineBehaviour
    {
        private Level_1_Boss boss;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!boss) boss = animator.GetComponent<Level_1_Boss>();

            foreach (Weapon gun in boss.GetRotatingGuns)
            {
                gun.GetComponent<Rotator>().Rotate();
                gun.Fire();
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (Weapon gun in boss.GetRotatingGuns)
            {
                gun.GetComponent<Rotator>().Stop();
                gun.HoldFire();
            }
        }
    }
}

