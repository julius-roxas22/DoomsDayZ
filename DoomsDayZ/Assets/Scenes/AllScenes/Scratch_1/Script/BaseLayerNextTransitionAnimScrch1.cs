using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/{TEMP}Create Ability/UpperLayer/BaseLayerNextTransitionAnim")]
public class BaseLayerNextTransitionAnimScrch1 : BaseStateDataScrch1
{
    [SerializeField] private float animationTiming;

    public override void _OnEnterAnimation(CharacterScrch1 character, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void _OnUpdateAnimation(CharacterScrch1 character, Animator animator, AnimatorStateInfo stateInfo)
    {
        if (stateInfo.normalizedTime >= animationTiming)
        {
            animator.SetBool(_EAnimationTransition.NextTransitionAnim.ToString(), true);
        }
    }

    public override void _OnExitAnimation(CharacterScrch1 character, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(_EAnimationTransition.NextTransitionAnim.ToString(), false);
    }
}
