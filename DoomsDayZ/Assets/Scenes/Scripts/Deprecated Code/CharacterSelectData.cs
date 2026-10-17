using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using IndieGameManagersSystem;
using IndieGameCharacterControllerSystem;

namespace IndieGameGameControllerSystem
{
    [System.Obsolete("This Code is Deprecated!", true)]
    [CreateAssetMenu(fileName = "New Data", menuName = "IndieGamePractice/CharacterSelectData/New Data")]
    public class CharacterSelectData : ScriptableObject
    {

        public int _CharacterIndexOf;
        public _ECameraCharacterSelectParameters _SelectedCharacterType;
        [HideInInspector] public string _CharacterSpawned;

        public Character _GetCharacterIndexOf(int indexOf)
        {
            List<Character> characters = CharacterManager.GetInstance._Characters;
            if (null != characters && characters.Count > 1)
            {
                return characters[indexOf];
            }

            return null;
        }
    }
}
