using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;
using IndieGameUISystem;

namespace IndieGameAnimationStateData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/SetActiveHotBarItem")]
    public class SetActiveHotBarItem : BaseStateData
    {
        [SerializeField] private bool isAnimateTranstion = false;
        private ItemHotBarContainer hotBar;

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            isAnimateTranstion = false;
            if (null == hotBar)
            {
                hotBar = GameManager.GetInstance._GetItemHotbarContainer();
            }
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (character._Controller._IsPressedKey1)
            {
                hotBar._SlotIndex = 0;
            }
            else if (character._Controller._IsPressedKey2)
            {
                hotBar._SlotIndex = 1;
            }
            else if (character._Controller._IsPressedKey3)
            {
                hotBar._SlotIndex = 2;
            }
            else if (character._Controller._IsPressedKey4)
            {
                hotBar._SlotIndex = 3;
            }
            else if (character._Controller._IsPressKeyX)
            {
                hotBar._SlotIndex = -1;
            }

            if (hotBar._SlotIndex >= 0 && hotBar._SlotIndex <= 3)
            {

                //TODO: check if item slot does have held
                //TODO: if it has; then want to switch other weapon slot that has held. it will not animate again just to switch weapon

                if (!hotBar._CheckItemHeld()) //check the hotbar slot if does have held
                {
                    return;
                }

                if (isAnimateTranstion)
                {
                    return;
                }

                isAnimateTranstion = true;
                animator.SetBool(_ETransitionParameters.Equip.ToString(), isAnimateTranstion);//automatically turn off when switch animator
            }
            else if (hotBar._SlotIndex == -1)
            {
                if (!character._IsEquiped)
                {
                    return;
                }

                if (isAnimateTranstion)
                {
                    return;
                }

                isAnimateTranstion = true;
                animator.SetBool(_ETransitionParameters.UnEquip.ToString(), isAnimateTranstion);//automatically turn off when switch animator
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            isAnimateTranstion = false;
            hotBar._SlotIndex = -2;
        }
    }
}
