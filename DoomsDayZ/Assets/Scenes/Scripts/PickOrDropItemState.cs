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
    public class PickOrDropItemState : BaseStateData
    {
        private GameUI gameUI;
        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if(null == gameUI)
            {
                gameUI = GameManager.GetInstance._GetGameUI();
            }
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

            if (character._IsEquiped)
            {
                if (character._Controller._IsDroppingWeapon)
                {
                    animator.SetBool(_ETransitionParameters.IsDroppingWeapon.ToString(), true);
                }
            }
            else
            {
                pickUpdateState(character, animator);
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            animator.SetBool(_ETransitionParameters.IsGrabbingItem.ToString(), false);
            animator.SetBool(_ETransitionParameters.IsDroppingWeapon.ToString(), false);
        }

        private void pickUpdateState(Character character, Animator animator)
        {
            List<Collider> weaponList = character._VicinityDetector._VicinityDetects;

            if (weaponList.Count == 0)
            {
                return;
            }

            if (!character._Controller._IsGrabbingWeapon)
            {
                return;
            }

            if (GameManager.GetInstance._GetItemHotbarContainer()._IsItemSlotIsFull)
            {
                //add some text ui in to the game
                Debug.Log("Item slot is full");
                return;
            }

            animator.SetBool(_ETransitionParameters.IsGrabbingItem.ToString(), true);
        }
    }
}
