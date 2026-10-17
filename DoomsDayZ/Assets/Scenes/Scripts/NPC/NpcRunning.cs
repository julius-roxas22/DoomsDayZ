using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameAnimationStateData;
using IndieGameCharacterControllerSystem;
using IndieGameNpcSystem;

namespace IndieGameNpcData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/NPCController/NpcRunningController")]
    public class NpcRunning : BaseStateData
    {

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            character._NPCBase.navAgent.speed = 2.4f;// movement speed increase when chasing the target
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            NpcBoomer boomer = (NpcBoomer)character._NPCBase;
            boomer.currentPositionSeen = character._NPCBase._DetectedTarget.transform.position; //keep tracking the current position of the target

            #region new ai setup
            if (character._NPCBase.currentStateActive == _ENpcState.RUNNING) // keep chasing the player if still in a vision of the boomer
            {
                character._NPCBase._MoveToTarget();
            }

            // after player die. boomer keep patrolling; however it will force to argoninize the boomer who will locked to target the player
            if (character._NPCBase.currentStateActive == _ENpcState.PATROLLING)
            {
                if (!boomer.isPathCalculated)
                {
                    boomer.lastPositionSeen = boomer.currentPositionSeen; // boomer got a last position seen if player disapear
                    boomer.isPathCalculated = true;
                }

                if (boomer.isPathCalculated)
                {
                    boomer._MoveToNewTarget();//move into the last position and argonize
                }
            }

            if (character._NPCBase.currentStateActive == _ENpcState.ARGONIZING)
            {
                animator.SetBool(_ETransitionParameters.Argonizing.ToString(), true);
            }
            #endregion
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            animator.SetBool(_ETransitionParameters.Move.ToString(), false);
            animator.SetBool(_ETransitionParameters.Sprint.ToString(), false);
            character._NPCBase.navAgent.ResetPath();
        }
    }
}


