using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameAnimationStateData;

[CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/{TEMP}Create Ability/UpperLayer/Idle")]
public class IdleScrch1 : BaseStateDataScrch1
{
    public override void _OnEnterAnimation(CharacterScrch1 character, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void _OnUpdateAnimation(CharacterScrch1 character, Animator animator, AnimatorStateInfo stateInfo)
    {
        Controller controller = character._Controller;

        if (controller._MoveForward && controller._MoveBackward)
        {
            return;
        }

        if (controller._MoveRight && controller._MoveLeft)
        {
            return;
        }

        if (controller._MoveForward)
        {
            animator.SetBool(_EAnimationTransition.MoveForward.ToString(), true);
        }

        if (controller._MoveBackward)
        {
            animator.SetBool(_EAnimationTransition.MoveBackward.ToString(), true);
        }

        if (controller._MoveRight)
        {
            animator.SetBool(_EAnimationTransition.MoveRight.ToString(), true);
        }

        if (controller._MoveLeft)
        {
            animator.SetBool(_EAnimationTransition.MoveLeft.ToString(), true);
        }

        if (controller._MoveForwardRight)
        {
            animator.SetBool(_EAnimationTransition.MoveForwardRight.ToString(), true);
        }

        if (controller._MoveForwardLeft)
        {
            animator.SetBool(_EAnimationTransition.MoveForwardLeft.ToString(), true);
        }

        if (controller._MoveBackwardRight)
        {
            animator.SetBool(_EAnimationTransition.MoveBackwardRight.ToString(), true);
        }

        if (controller._MoveBackwardLeft)
        {
            animator.SetBool(_EAnimationTransition.MoveBackwardLeft.ToString(), true);
        }
    }

    public override void _OnExitAnimation(CharacterScrch1 character, Animator animator, AnimatorStateInfo stateInfo)
    {
        //_EAnimationTransition[] arr = System.Enum.GetValues(typeof(_EAnimationTransition)) as _EAnimationTransition[];

        //foreach (_EAnimationTransition t in arr)
        //{
        //    animator.SetBool(t.ToString(), false);
        //}
    }
}
