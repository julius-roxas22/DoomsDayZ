using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;
using IndieGameWeaponSystem;
using IndieGameCharacterAttributeData;

//HACK: add ability to add a new character model
//STRENGTH
//health increase (5 health / 1 point)--
//health regeneration (0.2f health regen / 1 point)--
//primary attribute special ability restore health

//AGILITY
//damage increase (2 damage / 1 point)--
//stamina increase (4 stamina / 1 point)--
//primary attribute special ability critical damage

//INTELLIGENCE
//movement speed increase (0.01 mvmntspd / 1 point)--
//stamina regeneration (0.8 regen / 1 point)--
//primary attribute special ability flicker in a short distance

namespace IndieGameHealthSystem
{
    public enum _EAttributeType
    {
        STR,
        AGI,
        INT
    }

    //later on we will add more and more update here
    public class HealthSystem : MonoBehaviour
    {
        // public HealthUI _HealthUI;

        [Header("Character Defense")]
        public DefenseData _DefenseData;

        [Header("Attack Details")]
        public float _StaminaToDecrease;
        public float _CriticalDamage;

        [Range(0f, 1f)]
        public float _CriticalChance;
        public float _CurrentCriticalChance;
        public bool _HasCritChance;

        public string _BodyPartCameraToFollow;//when player spawn. this body will attached into camera to follow on it.
        public GameObject _WeaponProjectileStuck;
        public List<RuntimeAnimatorController> _HitReactionList = new List<RuntimeAnimatorController>();


        #region Defense Data
        //public float _STR;
        //public float _AGI;
        //public float _INT;
        ////each character model/player has a maximum of 15 points to assign to all attributes by default
        ////stamina => just for handling weapon or bear hands; everytime the player attacks it will decrease stamina and then a few seconds it will regenerating stamina

        //[Space(17)]
        //public float _MaximumHealth = 100f;//pre defined values
        //public float _CurrentHealth;
        //public float _MaximumStamina = 100f;//pre defined values
        //public float _CurrentStamina;
        //public float _AdditionalMovementSpeed;

        #endregion
        private void Awake()
        {
            _DefenseData._MaximumHealth += (_DefenseData._STR * 5f);
            _DefenseData._MaximumStamina += (_DefenseData._AGI * 4f);
            _DefenseData._AdditionalMovementSpeed = (_DefenseData._INT * 0.01f);

            _DefenseData._CurrentHealth = _DefenseData._MaximumHealth;
            _DefenseData._CurrentStamina = _DefenseData._MaximumStamina;
        }

        public void _RegenerateStamina(Character character)
        {
            if (!character._SubController.enabled)
            {
                return;
            }

            HealthSystem healthSystem = character._HealthSystem;
            float prevStamina = healthSystem._DefenseData._CurrentStamina;

            if (character._SubController.enabled)
            {
                healthSystem._DefenseData._CurrentStamina = Mathf.Min(
                    healthSystem._DefenseData._CurrentStamina + ((healthSystem._DefenseData._INT * 0.8f) * Time.deltaTime),
                    healthSystem._DefenseData._MaximumStamina
                    );
            }

            if (!Mathf.Approximately(prevStamina, healthSystem._DefenseData._CurrentStamina))
            {
                GameManager.GetInstance._GetGameUI()._UpdatePlayerStaminaBar();
            }
        }

        public void _RegenerateHealth(Character character)
        {
            if (!character._SubController.enabled)
            {
                return;
            }

            HealthSystem healthSystem = character._HealthSystem;
            float prevHealth = healthSystem._DefenseData._CurrentHealth;

            healthSystem._DefenseData._CurrentHealth = Mathf.Min(
                healthSystem._DefenseData._CurrentHealth + ((healthSystem._DefenseData._STR * 0.2f) * Time.deltaTime),
                healthSystem._DefenseData._MaximumHealth
            );

            if (!Mathf.Approximately(prevHealth, healthSystem._DefenseData._CurrentHealth))
            {
                GameManager.GetInstance._GetGameUI()._UpdatePlayerHealthBar(character);
            }
        }

        public bool _CheckStaminaSufficient(Character character)
        {
            HealthSystem healthSystem = character._HealthSystem;

            if (character._IsEquiped)
            {
                float weaponHandling = WeaponManager.GetInstance._GetWeaponList(character)[0].GetComponent<WeaponMelee>()._WeaponMeleeData._WeaponHandling;

                if (healthSystem._DefenseData._CurrentStamina > weaponHandling)
                {
                    healthSystem._StaminaToDecrease = weaponHandling;
                    return true;
                }
            }
            else
            {
                if (healthSystem._DefenseData._CurrentStamina > 2.5f)
                {
                    healthSystem._StaminaToDecrease = 2.5f;
                    return true;
                }
            }
            return false;
        }
    }
}
