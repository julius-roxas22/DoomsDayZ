using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;

namespace IndieGameAnimationStateData
{
    public class BaseStateMachine : StateMachineBehaviour
    {
        public List<BaseStateData> _Data = new List<BaseStateData>();
        [HideInInspector] public Character controller;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

            if (null == controller)
            {
                controller = animator.transform.root.GetComponent<Character>();
                controller._CacheController(animator);
            }

            foreach (BaseStateData data in _Data)
            {
                data.OnEnterAnimation(controller, stateInfo, animator);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (BaseStateData data in _Data)
            {
                data.OnUpdateAnimation(controller, stateInfo, animator);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (BaseStateData data in _Data)
            {
                data.OnExitAnimation(controller, stateInfo, animator);
            }
        }
    }
}

