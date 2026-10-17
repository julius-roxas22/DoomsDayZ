using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameAnimationStateData
{
    [CreateAssetMenu(fileName = "New Text State", menuName = "IndieGamePractice/State/DamageTakenText")]
    public class DamageTakenState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //destroy damage taken text
            Destroy(animator.gameObject);
        }
    }
}
