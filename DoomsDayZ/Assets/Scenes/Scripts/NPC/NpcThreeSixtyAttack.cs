using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameAnimationStateData;
using IndieGameCharacterControllerSystem;

namespace IndieGameNpcData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/NPCController/NpcThreeSixtyAttackController")]
    public class NpcThreeSixtyAttack : BaseStateData
    {
        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            character._NPCBase._MoveToTarget();
            character._NPCBase.currentStateActive = IndieGameNpcSystem._ENpcState.MOVING;
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }
    }
}


