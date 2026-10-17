using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameManagersSystem;

public class ResetCharacterSelectBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _ECameraCharacterSelectParameters[] arr = System.Enum.GetValues(typeof(_ECameraCharacterSelectParameters)) as _ECameraCharacterSelectParameters[];

        foreach (_ECameraCharacterSelectParameters val in arr)
        {
            animator.SetBool(val.ToString(), false);
        }
    }
}
