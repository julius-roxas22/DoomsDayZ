using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;
using IndieGameUISystem;

namespace IndieGameAnimationStateData
{
    //this state is check wether pick or drop the weapon
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/PickOrDropWeaponState")]
    public class PickOrDropWeaponState : BaseStateData
    {
        [Space(10)]
        [Header("Setup for Drop Weapon")]
        [SerializeField] private bool onDropWeapon;

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

            if (onDropWeapon)
            {
                //if we already have weapon equiped
                //we can drop it on the same location of the character
                if (!character._IsEquiped) // if there is no weapon equiped or no weapon yet we cant drop something
                {
                    return;
                }

                if (character._Controller._IsDroppingWeapon)
                {
                    animator.SetBool(_ETransitionParameters.IsDroppingWeapon.ToString(), true);
                }
            }
            else
            {
                //if (character._IsEquiped) // just to check if we already equiped weapon and if not we can pick up
                //{
                //    return;
                //}
                List<Collider> weaponList = character._VicinityDetector._VicinityDetects;

                if (weaponList.Count == 0)//check if there is weapon on the ground; same location of the character
                {
                    return;
                }
                
                if (character._Controller._IsGrabbingWeapon)
                {
                    //
                    animator.SetBool(_ETransitionParameters.IsGrabbingWeapon.ToString(), true);
                }
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            animator.SetBool(_ETransitionParameters.IsGrabbingWeapon.ToString(), false);
            animator.SetBool(_ETransitionParameters.IsDroppingWeapon.ToString(), false);
        }
    }
}
