using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameAnimationStateData;
using IndieGameInventorySystem;

namespace IndieGameAttackSystem
{
    public class AttackInfo : MonoBehaviour
    {
        public BaseDamage _BaseDamageType;
        public Character _Attacker;
        public bool _MustCollide;
        public bool _IsRegisteredAttack;
        public List<string> _AttackParts = new List<string>();
        //public GameObject _ObjectAttack;
        public int _Damage;

        public void _ResetAttack(BaseDamage pressAttack, Character attacker)
        {
            _BaseDamageType = pressAttack;
            _Attacker = attacker;
            _IsRegisteredAttack = false;
            _AttackParts = null;
        }

        public void _RegisterAttack(BaseDamage baseDamage, Character character)
        {
            _IsRegisteredAttack = true;
            _BaseDamageType = baseDamage;
            _MustCollide = baseDamage._MustCollide;
            if (null != baseDamage._WeaponMelee)
            {
                WeaponMeleeData weaponData = baseDamage._WeaponMelee._WeaponMeleeData;
                _AttackParts = weaponData._AttackParts;
                _Damage = weaponData._Damage + (int)(character._HealthSystem._DefenseData._AGI * 2f);
            }
            else
            {
                _AttackParts = baseDamage._AttackParts;
                _Damage = Random.Range(12, 18) + (int)(character._HealthSystem._DefenseData._AGI * 2f) + (int)character._HealthSystem._CriticalDamage;
            }
        }
    }
}

