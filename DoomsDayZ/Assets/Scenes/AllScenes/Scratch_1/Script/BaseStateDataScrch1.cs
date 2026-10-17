using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateDataScrch1 : ScriptableObject
{
    public abstract void _OnEnterAnimation(CharacterScrch1 character, Animator animator, AnimatorStateInfo stateInfo);
    public abstract void _OnUpdateAnimation(CharacterScrch1 character, Animator animator, AnimatorStateInfo stateInfo);
    public abstract void _OnExitAnimation(CharacterScrch1 character, Animator animator, AnimatorStateInfo stateInfo);
}
