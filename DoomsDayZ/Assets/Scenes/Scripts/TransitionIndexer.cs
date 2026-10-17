using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;

namespace IndieGameAnimationStateData
{
    public enum _ETransitionConditionType
    {
        NONE,
        ATTACK,
        WEAPON_EQUIPED,
        STAMINA_SUFFICIENT,
        HAS_CRIT_CHANCE,
        IS_PRESSED_KEY_R,
        JUMP,
    }

    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/TransitionIndexer")]
    public class TransitionIndexer : BaseStateData
    {
        [SerializeField] List<_ETransitionConditionType> conditionType = new List<_ETransitionConditionType>();
        [SerializeField] private int targetIndex;
        [SerializeField] private float consumableStamina; //set consumable stamina

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            animator.SetInteger(_ETransitionParameters.TransitionIndex.ToString(), 0);
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (makeCondition(character))
            {
                animator.SetInteger(_ETransitionParameters.TransitionIndex.ToString(), targetIndex);
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            animator.SetInteger(_ETransitionParameters.TransitionIndex.ToString(), 0);
        }

        private bool makeCondition(Character character)
        {
            foreach (_ETransitionConditionType transition in conditionType)
            {
                switch (transition)
                {
                    case _ETransitionConditionType.ATTACK:
                        {
                            if (!character._AttackTriggered)
                            {
                                return false;
                            }
                            break;
                        }
                    case _ETransitionConditionType.JUMP:
                        {
                            if (!character._Controller._Jump)
                            {
                                return false;
                            }
                            break;
                        }
                    case _ETransitionConditionType.WEAPON_EQUIPED:
                        {
                            if (!character._IsEquiped)
                            {
                                return false;
                            }
                            break;
                        }
                    case _ETransitionConditionType.STAMINA_SUFFICIENT:
                        {
                            if (character._HealthSystem._DefenseData._CurrentStamina < consumableStamina)
                            {
                                return false;
                            }
                            break;
                        }
                    case _ETransitionConditionType.HAS_CRIT_CHANCE:
                        {
                            if (!character._HealthSystem._HasCritChance)
                            {
                                return false;
                            }
                            break;
                        }
                    case _ETransitionConditionType.IS_PRESSED_KEY_R:
                        {
                            if (!character._Controller._IsPressedKeyR)
                            {
                                return false;
                            }
                            break;
                        }
                }
            }
            return true;
        }
    }
}
