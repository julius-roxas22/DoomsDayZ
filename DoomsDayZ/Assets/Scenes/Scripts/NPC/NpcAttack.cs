using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameAnimationStateData;
using IndieGameCharacterControllerSystem;

namespace IndieGameNpcData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/NPCController/NpcAttack")]
    public class NpcAttack : BaseStateData
    {
        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            faceTarget(character);//rotate instantly when attack the target detected
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        private void faceTarget(Character character)
        {
            Vector3 look = character._NPCBase._DetectedTarget.transform.position - character.transform.position;
            look.y = 0f;
            if (look != Vector3.zero)
            {
                Quaternion lookRotate = Quaternion.LookRotation(look);
                character.transform.rotation = /*Quaternion.RotateTowards(character.transform.rotation, lookRotate, 360f * Time.deltaTime);*/ lookRotate;
            }
        }
    }
}


