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
        private Dictionary<ItemSlotContainer, bool> dictionaryActiveItem = new Dictionary<ItemSlotContainer, bool>();
        private bool isAnimateTranstion;
        private ItemHotBarContainer hotBar;

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if(null == hotBar)
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

            if (hotBar._SlotIndex != -1)
            {
                if (!isAnimateTranstion)
                {
                    isAnimateTranstion = true;
                    animator.SetBool(_ETransitionParameters.IsEquiped.ToString(), isAnimateTranstion);
                }
            }
            else
            {
                if (isAnimateTranstion)
                {
                    isAnimateTranstion = false;
                    animator.SetBool(_ETransitionParameters.IsEquiped.ToString(), isAnimateTranstion);
                }

            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        private ItemSlotContainer getItemSlotContainer(int gameObjectIndex, Animator animator)
        {
            List<ItemSlotContainer> slots = GameManager.GetInstance._GetItemHotbarContainer()._ItemSlotContainerList;
            ItemSlotContainer container = slots[gameObjectIndex];
            if (container._HasHeldItem && null != container._WeaponMeleeData)
            {
                return container;
            }

            if (!isAnimateTranstion)
            {
                animator.SetBool(_ETransitionParameters.IsEquiped.ToString(), true);
                isAnimateTranstion = true;
            }
            return null;
        }
    }
}
