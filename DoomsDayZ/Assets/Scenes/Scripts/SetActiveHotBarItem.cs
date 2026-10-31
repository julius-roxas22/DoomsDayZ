using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;
using IndieGameUISystem;

namespace IndieGameAnimationStateData
{
    //this script is a state to grab the weapon with timing
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/SetActiveHotBarItem")]
    public class SetActiveHotBarItem : BaseStateData
    {
        private Dictionary<ItemSlotContainer, bool> dictionaryActiveWeapon = new Dictionary<ItemSlotContainer, bool>();
        private bool setAnimator;

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            setDefaultDictionary();
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            List<ItemSlotContainer> slots = GameManager.GetInstance._GetItemHotbarContainer()._ItemSlotContainerList;

            ItemSlotContainer container1 = slots[0];
            ItemSlotContainer container2 = slots[1];
            ItemSlotContainer container3 = slots[2];
            ItemSlotContainer container4 = slots[3];

            if (container1._HasHeldItem)
            {
                if (character._Controller._IsPressedKey1)
                {
                    setValueDictionary(container1, animator);
                }
            }
            else if (container2._HasHeldItem)
            {
                if (character._Controller._IsPressedKey2)
                {
                    setValueDictionary(container2, animator);
                }
            }
            else if (container3._HasHeldItem)
            {
                if (character._Controller._IsPressedKey3)
                {
                    setValueDictionary(container3, animator);
                }
            }
            else if (container4._HasHeldItem)
            {
                if (character._Controller._IsPressedKey4)
                {
                    setValueDictionary(container4, animator);
                }
            }
            else if (character._Controller._IsPressKeyX)
            {
                setDefaultDictionary();
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        private void setDefaultDictionary()
        {
            foreach (ItemSlotContainer slot in GameManager.GetInstance._GetItemHotbarContainer()._ItemSlotContainerList)
            {
                slot._ItemSetActive = false;
                dictionaryActiveWeapon[slot] = false;
            }

            if (setAnimator)
            {
                //animator transition here
                setAnimator = false;
            }
        }

        private void setValueDictionary(ItemSlotContainer slot, Animator animator)
        {
            foreach (KeyValuePair<ItemSlotContainer, bool> kvp in dictionaryActiveWeapon)
            {
                if (kvp.Key == slot)
                {
                    dictionaryActiveWeapon[kvp.Key] = true;
                    slot._ItemSetActive = true;
                }
                else
                {
                    dictionaryActiveWeapon[kvp.Key] = false;
                    slot._ItemSetActive = false;
                }
            }


            if (!setAnimator)
            {
                //animation transition here
                setAnimator = true;
            }
        }
    }
}
