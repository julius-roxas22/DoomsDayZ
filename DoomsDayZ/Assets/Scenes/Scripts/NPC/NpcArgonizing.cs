using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameAnimationStateData;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;

namespace IndieGameNpcData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/NPCController/NpcArgonizingController")]
    public class NpcArgonizing : BaseStateData
    {
        [SerializeField] private float explodeTiming;

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            #region new ai setup
            if (stateInfo.normalizedTime >= explodeTiming)
            {
                //boom
                IndieGameNpcSystem.NpcBoomer boomer = (IndieGameNpcSystem.NpcBoomer)character._NPCBase;
                if (character._HealthSystem._DefenseData._CurrentHealth > 0f)
                {
                    boomer._Exlopde();
                }
            }
            #endregion
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }
    }
}


