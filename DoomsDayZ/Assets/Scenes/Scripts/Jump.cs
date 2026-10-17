using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;

namespace IndieGameAnimationStateData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/Jump")]
    public class Jump : BaseStateData
    {

        [Space(17)]
        [Header("Basic Setup")]
        [Range(0, 1)]
        [SerializeField] private float jumpDelay;
        [SerializeField] private float jumpForce;
        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            character._Controller._IsJumped = false;
            if (0f == jumpDelay)
            {
                character._RigidBody.AddForce(Vector3.up * jumpForce);
                character._Controller._IsJumped = true;
            }
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (stateInfo.normalizedTime >= jumpDelay && !character._Controller._IsJumped)
            {
                character._RigidBody.AddForce(Vector3.up * jumpForce);
                character._Controller._IsJumped = true;
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            character._Controller._IsJumped = false;
        }
    }
}
