using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;
using IndieGameUISystem;

namespace IndieGameAnimationStateData
{
    //this script is a state to grab the weapon with timing
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/{")]
    public class SetWeapon : BaseStateData
    {

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            List<ItemSlotContainer> slots = GameManager.GetInstance._GetItemHotbarContainer()._ItemSlotContainerList;

            ItemSlotContainer container1 = slots[0];
            ItemSlotContainer container2 = slots[1];
            ItemSlotContainer container3 = slots[2];
            ItemSlotContainer container4 = slots[3];

            if (container1._HasHeldItem && character._Controller._IsPressedKey1)
            {

            }
            else if (container2._HasHeldItem && character._Controller._IsPressedKey2)
            {

            }
            else if (container3._HasHeldItem && character._Controller._IsPressedKey3)
            {

            }
            else if (container4._HasHeldItem && character._Controller._IsPressedKey4)
            {

            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }
    }
}
