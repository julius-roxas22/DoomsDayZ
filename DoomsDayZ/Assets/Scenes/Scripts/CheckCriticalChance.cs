using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameHealthSystem;

namespace IndieGameAnimationStateData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/CheckCriticalChance")]
    public class CheckCriticalChance : BaseStateData
    {
        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            HealthSystem health = character._HealthSystem;

            health._CurrentCriticalChance = Random.Range(0f, 1f);

            if (health._CurrentCriticalChance <= health._CriticalChance)
            {
                if (!health._HasCritChance)
                {
                    health._HasCritChance = true;
                }
            }
            else
            {
                health._HasCritChance = false;
            }
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }
    }
}
