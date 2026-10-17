using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/{TEMP}Create Ability/UpperLayer/BaseLayerJump")]
public class BaseLayerJumpScrch1 : BaseStateDataScrch1
{
    public override void _OnEnterAnimation(CharacterScrch1 character, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(_EAnimationTransition.Jump.ToString(), false);
        character._CharacterRigidBody.AddForce(Vector3.up * 150f);
    }

    public override void _OnUpdateAnimation(CharacterScrch1 character, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void _OnExitAnimation(CharacterScrch1 character, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
