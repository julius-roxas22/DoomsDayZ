using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;

namespace IndieGameAnimationStateData
{
    public abstract class BaseStateData : ScriptableObject
    {
        public abstract void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator);

        public abstract void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator);

        public abstract void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator);
    }
}

