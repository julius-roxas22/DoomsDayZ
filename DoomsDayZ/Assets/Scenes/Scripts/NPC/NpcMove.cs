using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameAnimationStateData;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;

namespace IndieGameNpcData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/NPCController/NpcMoveController")]
    public class NpcMove : BaseStateData
    {
        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.PATROLLING)
            {
                if (!character._NPCBase.detectedAlly)
                {
                    character._NPCBase._SetRandomPath();
                }
                else
                {
                    character._NPCBase.currentStateActive = IndieGameNpcSystem._ENpcState.IDLE;
                }
            }
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            #region new ai setup
            if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.PATROLLING)
            {
                if (character._NPCBase._ReachedPath || character._NPCBase.detectedAlly)
                {
                    animator.SetBool(_ETransitionParameters.Move.ToString(), false);
                }
            }

            if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.IDLE)
            {
                animator.SetBool(_ETransitionParameters.Move.ToString(), false);
            }

            if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.ATTACK)
            {
                animator.SetBool(_ETransitionParameters.Attack.ToString(), true);
            }

            //if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.ARGONIZING)
            //{
            //    animator.SetBool(_ETransitionParameters.Argonizing.ToString(), true);
            //}

            if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.THREE_SIXTY_ATTACK)
            {
                animator.SetBool(_ETransitionParameters.ThreeSixtyAttack.ToString(), true);
            }

            if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.THROW)
            {
                animator.SetBool(_ETransitionParameters.Throw.ToString(), true);
            }

            if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.MOVING)
            {
                if (!character._NPCBase.detectedAlly)
                {
                    character._NPCBase._MoveToTarget();
                }
                else
                {
                    character._NPCBase.currentStateActive = IndieGameNpcSystem._ENpcState.IDLE;
                }
            }

            if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.RUNNING)
            {
                animator.SetBool(_ETransitionParameters.Move.ToString(), true);
                animator.SetBool(_ETransitionParameters.Sprint.ToString(), true);
            }
            #endregion
        }

        //issues when have multiple npc`s
        //sound effect will keep playing even they are all die or stopped moving
        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            //Debug.Log("Npc Move has been exit");
            animator.SetBool(_ETransitionParameters.Attack.ToString(), false);
            animator.SetBool(_ETransitionParameters.ThreeSixtyAttack.ToString(), false);
            animator.SetBool(_ETransitionParameters.Throw.ToString(), false);
            animator.SetBool(_ETransitionParameters.Move.ToString(), false);
            animator.SetBool(_ETransitionParameters.Argonizing.ToString(), false);
            animator.SetBool(_ETransitionParameters.Sprint.ToString(), false);
            character._NPCBase.navAgent.ResetPath();
        }
    }
}


