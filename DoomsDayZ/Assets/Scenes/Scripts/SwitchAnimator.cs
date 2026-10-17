using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;
using IndieGameAttackSystem;

namespace IndieGameAnimationStateData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/SwitchAnimator")]
    public class SwitchAnimator : BaseStateData
    {
        //[SerializeField] private RuntimeAnimatorController _NewSwitchedAnimatorController;
        [SerializeField] private List<RuntimeAnimatorController> _NewRunAnimControllers;
        [SerializeField] private float switchTiming;

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (AttackManager.GetInstance._CurrentAttacks.Count > 0) // just to clear the current attacks in the list when it get hit
            {
                AttackManager.GetInstance._CurrentAttacks.RemoveAll(e => AttackManager.GetInstance._CurrentAttacks.Contains(e));
            }

            if (character._SubController.enabled && character._Controller._IsJumped)
            {
                character._Controller._IsJumped = false;
            }
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (stateInfo.normalizedTime >= switchTiming)
            {
                if (_NewRunAnimControllers.Count > 1)
                {
                    switchTo(character);
                }
                else
                {
                    character._Animator.runtimeAnimatorController = _NewRunAnimControllers[0];//switch into one armed weapon
                }
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        private bool checkHeavyWeapon(string name)
        {
            if (name.Equals("Hammer") || name.Equals("Buckle Pipe") || name.Equals("Nail Flocker")) //tmporary condition
            {
                return false;
            }
            return true;
        }

        private void switchTo(Character character)
        {
            if (character._IsEquiped)
            {
                GameObject weapon = WeaponManager.GetInstance._GetWeaponList(character)[0];
                if (!checkHeavyWeapon(weapon.name))
                {
                    //change to deault melee run animator
                    character._Animator.runtimeAnimatorController = _NewRunAnimControllers[0]; // one hand animator
                }
                else
                {
                    //change to heavy run animator
                    character._Animator.runtimeAnimatorController = _NewRunAnimControllers[1]; // two hand animator
                }
            }
        }
    }
}
