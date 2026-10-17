using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachineScrch1 : StateMachineBehaviour
{
    [SerializeField] private List<BaseStateDataScrch1> _Data = new List<BaseStateDataScrch1>();
    private CharacterScrch1 characterScrch1;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (BaseStateDataScrch1 data in _Data)
        {
            data._OnEnterAnimation(getCharacterScrch1(animator), animator, stateInfo);
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (BaseStateDataScrch1 data in _Data)
        {
            data._OnUpdateAnimation(getCharacterScrch1(animator), animator, stateInfo);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (BaseStateDataScrch1 data in _Data)
        {
            data._OnExitAnimation(getCharacterScrch1(animator), animator, stateInfo);
        }
    }

    CharacterScrch1 getCharacterScrch1(Animator animator)
    {
        if (null == characterScrch1)
        {
            characterScrch1 = animator.GetComponentInParent<CharacterScrch1>();
        }
        return characterScrch1;
    }
}
