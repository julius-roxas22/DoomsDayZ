using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameHealthSystem;
using IndieGameManagersSystem;

namespace IndieGameAnimationStateData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/HealthRestoration")]
    public class HealthRestoration : BaseStateData
    {
        [SerializeField] private float staminaConsumed;
        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            HealthSystem healthSystem = character._HealthSystem;
            healthSystem._DefenseData._CurrentStamina -= staminaConsumed;
            float current = healthSystem._DefenseData._CurrentHealth + (healthSystem._DefenseData._MaximumHealth * 0.24f); // 24% health increase
            healthSystem._DefenseData._CurrentHealth = Mathf.Clamp(current, 1f, healthSystem._DefenseData._MaximumHealth);

            GameManager.GetInstance._GetGameUI()._UpdatePlayerHealthBar(character);
        }
    }
}
