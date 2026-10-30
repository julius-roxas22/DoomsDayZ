using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameAttackSystem;
using IndieGameManagersSystem;
using IndieGameInventorySystem;
using IndieGameHealthSystem;

namespace IndieGameAnimationStateData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/BaseDamage")]
    public class BaseDamage : BaseStateData
    {
        [Header("Basic Setup")]
        public _EProjectileType _ProjectileType; // just to initiate specific poolobject prefab
        public float _StartTime;
        public float _EndTime;
        public bool _MustCollide;
        public WeaponMelee _WeaponMelee;
        public List<string> _AttackParts = new List<string>();//secondary attacker parts when weapon melee is null

        [Space(17)]
        [Header("Damage Impact")]
        public float _XForce;
        public float _YForce;
        public float _ZForce;
        [HideInInspector] public HashSet<Character> _AttackersAttack = new HashSet<Character>();

        [Space(17)]
        [Header("Debugging tools")]
        [SerializeField] private bool enableDebugMode;

        [Space(17)]
        [Header("Combo Setup")]
        [SerializeField] private float comboStart;
        [SerializeField] private float comboEnd;

        Dictionary<Character, AttackInfo> _DicCurrentAttack = new Dictionary<Character, AttackInfo>();

        //private AudioSource source;

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            character._AttackTriggered = false;

            HealthSystem healthSystem = character._HealthSystem;

            healthSystem._DefenseData._CurrentStamina = Mathf.Clamp(
                healthSystem._DefenseData._CurrentStamina - healthSystem._StaminaToDecrease,
                1f, healthSystem._DefenseData._MaximumStamina);//stamina will decrease   
            animator.SetBool(_ETransitionParameters.Attack.ToString(), false);

            if (character._IsEquiped) // set a value if player have weapon and equiped
            {
                List<GameObject> weaponList = WeaponManager.GetInstance._GetWeaponList(character);

                if (weaponList.Count > 0 && null != weaponList)
                {
                    if (null == _WeaponMelee)
                    {
                        _WeaponMelee = weaponList[0].GetComponent<WeaponMelee>();
                    }
                }
            }

            if (!_DicCurrentAttack.TryGetValue(character, out AttackInfo info))//check if there is already current attack of a single character
            {
                GameObject obj = PoolProjectileManager.GetInstance._GetProjectile(character, _ProjectileType);
                info = obj.GetComponent<AttackInfo>();
                info._ResetAttack(this, character);
                _DicCurrentAttack[character] = info;
            }

            if (!AttackManager.GetInstance._CurrentAttacks.Contains(info))
            {
                AttackManager.GetInstance._CurrentAttacks.Add(info);
            }
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (!_DicCurrentAttack.ContainsKey(character))
            {
                return;
            }

            registerAttack(character, stateInfo);
            deRegisterAttack(stateInfo, character);
            checkCombo(character, animator, stateInfo);
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            animator.SetBool(_ETransitionParameters.Attack.ToString(), false);
            clearAttacks(character);
            _AttackersAttack.Remove(character);
            _WeaponMelee = null;
        }

        private void registerAttack(Character character, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= _StartTime && stateInfo.normalizedTime < _EndTime)
            {
                AttackInfo info = _DicCurrentAttack[character];

                if (enableDebugMode)
                {
                    Debug.Log("Register Attack: " + stateInfo.normalizedTime);
                }

                if (!info._IsRegisteredAttack)
                {
                    info._RegisterAttack(this, character);
                }
            }
        }

        private void deRegisterAttack(AnimatorStateInfo stateInfo, Character character)
        {
            if (stateInfo.normalizedTime >= _EndTime && _DicCurrentAttack.TryGetValue(character, out AttackInfo info))
            {
                if (enableDebugMode)
                {
                    Debug.Log("De-Register Attack: " + stateInfo.normalizedTime);
                }

                if (info._IsRegisteredAttack)
                {
                    info._IsRegisteredAttack = false;
                    PoolProjectileManager.GetInstance._ReturnPool(character, info.GetComponent<PoolObject>());
                    AttackManager.GetInstance._CurrentAttacks.Remove(info);
                    _DicCurrentAttack.Remove(character);
                }
            }
        }

        private void clearAttacks(Character character)
        {
            if (_DicCurrentAttack.TryGetValue(character, out AttackInfo info))
            {
                if (_DicCurrentAttack.ContainsKey(character))
                {
                    PoolProjectileManager.GetInstance._ReturnPool(info, _ProjectileType);
                    AttackManager.GetInstance._CurrentAttacks.Remove(info);
                }
                _DicCurrentAttack.Remove(character);
            }
        }

        private void checkCombo(Character character, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= comboStart && stateInfo.normalizedTime < comboEnd)
            {
                if (character._AttackTriggered)
                {
                    if (character._HealthSystem._CheckStaminaSufficient(character))
                    {
                        animator.SetBool(_ETransitionParameters.Attack.ToString(), true);
                    }
                    character._AttackTriggered = false;
                }
            }
        }
    }
}
