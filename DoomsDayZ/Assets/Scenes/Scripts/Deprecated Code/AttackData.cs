using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameCharacterAttributeData
{
    [System.Obsolete("Deprecated code!", true)]
    [CreateAssetMenu(fileName = "New Character Defense Data", menuName = "IndieGamePractice/CharacterData/AttackData")]
    public class AttackData : ScriptableObject
    {
        public float _BaseDamage;
        public List<GameObject> _Projectiles = new List<GameObject>(); // list of projectiles attack

        public float _StaminaToDecrease; // every normal attack decrease specific stamina
        public float _CriticalDamage; // massive damage
        public float _CriticalChance; // chance of a critical damage
        public float _CurrentCriticalChance; // the current chance of critical damage
        public bool _HasCritChance; // check wether activate the critical or not
    }
}