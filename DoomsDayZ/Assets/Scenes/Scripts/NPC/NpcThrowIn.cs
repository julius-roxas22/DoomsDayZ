using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameAnimationStateData;
using IndieGameCharacterControllerSystem;

namespace IndieGameNpcData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/NPCController/NpcThrowInController")]
    public class NpcThrowIn : BaseStateData
    {
        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            // instantly look the target when throw the object
            character.transform.rotation = Quaternion.LookRotation(character._NPCBase._DetectedTarget.transform.position - character.transform.position);
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }
    }
}


