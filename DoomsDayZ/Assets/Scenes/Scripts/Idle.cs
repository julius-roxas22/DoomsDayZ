using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameHealthSystem;

namespace IndieGameAnimationStateData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/Idle")]
    public class Idle : BaseStateData
    {
        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            character._AttackTriggered = false;//to prevent attack when button attack is pressed while doing any animation action
            animator.SetBool(_ETransitionParameters.Attack.ToString(), false);
            animator.SetBool(_ETransitionParameters.Move.ToString(), false);
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            HealthSystem healthSystem = character._HealthSystem;

            if (healthSystem._DefenseData._CurrentStamina < healthSystem._DefenseData._MaximumStamina)
            {
                healthSystem._RegenerateStamina(character);// regenerating while player stopped
            }

            if (healthSystem._DefenseData._CurrentHealth < healthSystem._DefenseData._MaximumHealth)
            {
                healthSystem._RegenerateHealth(character);//health regeneration every frame
            }

            if ((character._Controller._MoveForward && character._Controller._MoveBackward) || (character._Controller._MoveLeft && character._Controller._MoveRight))
            {
                return;
            }

            if (character._AttackTriggered)
            {
                //check if character is equiped item or not
                //check the weapon handling if sufficient into stamina, if not it will not going to attack
                //for the basic punch attack; stamina decrease 1 every attack
                if (healthSystem._CheckStaminaSufficient(character))
                {
                    animator.SetBool(_ETransitionParameters.Attack.ToString(), true);
                }
                character._AttackTriggered = false;// add fail safe to avoid run in every frame
            }

            if (character._Controller._Jump)
            {
                animator.SetBool(_ETransitionParameters.Jump.ToString(), true);
            }

            if (character._Controller._MoveForward || character._Controller._MoveBackward || character._Controller._MoveLeft || character._Controller._MoveRight)
            {
                animator.SetBool(_ETransitionParameters.Move.ToString(), true);
            }

            if (character._Controller._MoveForwardLeft || character._Controller._MoveForwardRight || character._Controller._MoveBackwardLeft || character._Controller._MoveBackwardRight)
            {
                animator.SetBool(_ETransitionParameters.Move.ToString(), true);
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            animator.SetBool(_ETransitionParameters.Attack.ToString(), false);
        }
    }
}
