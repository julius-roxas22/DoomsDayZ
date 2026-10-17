using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;

[CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/{TEMP}Create Ability/UpperLayer/Move")]
public class MoveScrch1 : BaseStateDataScrch1
{
    private int MoveX; // animation parameters
    private int MoveZ; // animation parameters

    public override void _OnEnterAnimation(CharacterScrch1 character, Animator animator, AnimatorStateInfo stateInfo)
    {
        MoveX = Animator.StringToHash("MoveX");
        MoveZ = Animator.StringToHash("MoveZ");
    }

    public override void _OnUpdateAnimation(CharacterScrch1 character, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetFloat(MoveZ, character._MoveDirection.z);
        animator.SetFloat(MoveX, character._MoveDirection.x);
    }

    public override void _OnExitAnimation(CharacterScrch1 character, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
