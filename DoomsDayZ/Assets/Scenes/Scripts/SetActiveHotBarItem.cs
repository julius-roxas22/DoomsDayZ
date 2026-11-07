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

                if (!hotBar._CheckItemHeld())//check the specific item slot if held or not
                {
                    return;
                }

                if (hotBar._SetActiveSlotContainers[hotBar._ActivatedItemSlotContainer])
                {
                    return;
                }

                if (isAnimateTranstion)
                {
                    return;
                }

                isAnimateTranstion = true;
                hotBar._SetActiveSlotContainers[hotBar._ActivatedItemSlotContainer] = true;
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
                hotBar._SetActiveSlotContainers[hotBar._ActivatedItemSlotContainer] = false;
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
