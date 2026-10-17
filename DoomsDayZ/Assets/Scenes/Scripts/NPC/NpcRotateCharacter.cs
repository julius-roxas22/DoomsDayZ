using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameAnimationStateData;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;

namespace IndieGameNpcData
{
    //this wont work properply when i have many npc`s
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/NPCController/NpcRotateCharacterController")]
    public class NpcRotateCharacter : BaseStateData
    {

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            animator.applyRootMotion = true;
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

            if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.TURN_LEFT)
            {
                character.transform.rotation = Quaternion.Euler(0f, character.transform.eulerAngles.y - 90f, 0f);
            }
            else if (character._NPCBase.currentStateActive == IndieGameNpcSystem._ENpcState.TURN_RIGHT)
            {
                character.transform.rotation = Quaternion.Euler(0f, character.transform.eulerAngles.y + 90f, 0f);
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            animator.applyRootMotion = false;
            animator.transform.localPosition = Vector3.zero;
            animator.transform.localRotation = Quaternion.identity;

        }
    }
}


