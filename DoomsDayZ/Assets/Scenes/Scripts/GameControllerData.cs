using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;

namespace IndieGameGameControllerSystem
{
    [CreateAssetMenu(fileName = "Game Controller Data", menuName = "IndieGamePractice/Game/GameControllerData")]
    public class GameControllerData : ScriptableObject
    {
        [Header("Character Details Spawner")]
        public int _CharacterIndex;
        public string _CharacterName;
        public _EAllyType _CharacterAllyType;
        public _ECameraCharacterSelectParameters _CharacterCameraNameType;
    }
}
