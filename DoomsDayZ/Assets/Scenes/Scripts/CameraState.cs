using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameManagersSystem;

namespace IndieGameAnimationStateData
{
    public class CameraState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _ECameraNormalParameters[] arr = System.Enum.GetValues(typeof(_ECameraNormalParameters)) as _ECameraNormalParameters[];

            foreach (_ECameraNormalParameters cam in arr)
            {
                animator.ResetTrigger(cam.ToString());
            }
        }
    }
}

