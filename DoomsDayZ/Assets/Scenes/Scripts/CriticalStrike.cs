using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameHealthSystem;

namespace IndieGameAnimationStateData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/UppercutCriticalStrike")]
    public class CriticalStrike : BaseStateData
    {
        [SerializeField] private float abilityConsumed;

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            HealthSystem healthSystem = character._HealthSystem;

            healthSystem._StaminaToDecrease = 0f;
            healthSystem._DefenseData._CurrentStamina = Mathf.Clamp(healthSystem._DefenseData._CurrentStamina - abilityConsumed, 1f, healthSystem._DefenseData._MaximumStamina);
            HealthSystem health = character._HealthSystem;

            if (health._HasCritChance)
            {
                health._CriticalDamage = 100f;
            }
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            HealthSystem healthSystem = character._HealthSystem;
            healthSystem._HasCritChance = false;
            healthSystem._CriticalDamage = 0f;
            character._AttackTriggered = false;
        }
    }
}
