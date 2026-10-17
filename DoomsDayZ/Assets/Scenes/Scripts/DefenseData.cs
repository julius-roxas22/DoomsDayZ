using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameCharacterAttributeData
{
    [CreateAssetMenu(fileName = "New Character Defense Data", menuName = "IndieGamePractice/CharacterData/DefenseData")]
    public class DefenseData : ScriptableObject
    {
        [Space(17)]
        public float _STR;
        public float _AGI;
        public float _INT;
        //each character model/player has a maximum of 15 points to assign to all attributes by default
        //stamina => just for handling weapon or bear hands; everytime the player attacks it will decrease stamina and then a few seconds it will regenerating stamina

        [Space(17)]
        public float _MaximumHealth = 100f;//pre defined values
        public float _CurrentHealth;
        public float _MaximumStamina = 100f;//pre defined values
        public float _CurrentStamina;
        public float _AdditionalMovementSpeed;
    }
}
