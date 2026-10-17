using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;

namespace IndieGameAnimationStateData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/GroundDetector")]
    public class GroundDetector : BaseStateData
    {
        [SerializeField] private float radius;
        [SerializeField] private LayerMask goundMask;
        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (Physics.CheckSphere(character._VicinityDetector.transform.position, radius, goundMask))//temporary ground detector
            {
                animator.SetBool(_ETransitionParameters.IsGrounded.ToString(), true);
            }
            else
            {
                animator.SetBool(_ETransitionParameters.IsGrounded.ToString(), false);
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }
    }
}
