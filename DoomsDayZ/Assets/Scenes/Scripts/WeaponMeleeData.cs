using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this is data for every weapon that contains different type of weapons
namespace IndieGameWeaponSystem
{
    [CreateAssetMenu(fileName = "New Data", menuName = "IndieGamePractice/WeaponData/MeleeWeaponData")]
    public class WeaponMeleeData : ScriptableObject
    {
        //whewn its triggered into target;
        [Header("Damage And Impact")]
        public int _Damage;
        public List<string> _AttackParts; //being collide to detect damage
        public float _WeaponHandling;

        [Space(17)]
        [Header("Weapon Transform")]
        public Vector3 _Position;
        public Vector3 _Rotations;

        [Space(17)]
        [Header("Weapon Details")]
      //  public GameObject _WeaponObject; //weapon object prefab just to determine during runtime
        public Sprite _WeaponSpriteImage;
    }
}


