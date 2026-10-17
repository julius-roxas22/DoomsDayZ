using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameAnimationStateData;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;

namespace IndieGameNpcData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/NPCController/NpcIdleController")]
    public class NpcIdle : BaseStateData
    {
        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        { 
        
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            #region new ai setup
            if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.MOVING)
            {
                animator.SetBool(_ETransitionParameters.Move.ToString(), true);
                animator.SetBool(_ETransitionParameters.Sprint.ToString(), false);
            }

            if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.PATROLLING)
            {
                if (character._NPCBase._IsBeginToPatrol)// after a few seconds of idle; begin to patrol
                {
                    animator.SetBool(_ETransitionParameters.Move.ToString(), true);
                    animator.SetBool(_ETransitionParameters.Sprint.ToString(), false);
                }
            }

            if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.RUNNING)
            {
                animator.SetBool(_ETransitionParameters.Move.ToString(), true);
                animator.SetBool(_ETransitionParameters.Sprint.ToString(), true);
            }

            if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.THREE_SIXTY_ATTACK)
            {
                animator.SetBool(_ETransitionParameters.ThreeSixtyAttack.ToString(), true); // need to set up properly for the perfect timing and tuning
            }

            if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.ATTACK)
            {
                animator.SetBool(_ETransitionParameters.Attack.ToString(), true);
            }

            if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.ARGONIZING)
            {
                animator.SetBool(_ETransitionParameters.Argonizing.ToString(), true);
            }

            if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.THROW)
            {
                animator.SetBool(_ETransitionParameters.Throw.ToString(), true);
            }

            if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.TURN_LEFT)
            {
                animator.SetBool(_ETransitionParameters.TurnLeft.ToString(), true);
            }

            if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.TURN_RIGHT)
            {
                animator.SetBool(_ETransitionParameters.TurnRight.ToString(), true);
            }
            #endregion
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            animator.SetBool(_ETransitionParameters.Attack.ToString(), false);
            animator.SetBool(_ETransitionParameters.TurnLeft.ToString(), false);
            animator.SetBool(_ETransitionParameters.TurnRight.ToString(), false);
            animator.SetBool(_ETransitionParameters.ThreeSixtyAttack.ToString(), false);
            animator.SetBool(_ETransitionParameters.Throw.ToString(), false);
            animator.SetBool(_ETransitionParameters.Argonizing.ToString(), false);
        }
    }
}


