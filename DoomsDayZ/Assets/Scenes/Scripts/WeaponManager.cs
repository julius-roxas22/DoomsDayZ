using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameUISystem;

namespace IndieGameManagersSystem
{
    public class WeaponManager : Singleton<WeaponManager>
    {
        private Dictionary<Character, List<GameObject>> dictionaryWeaponList = new Dictionary<Character, List<GameObject>>();

        //getting error at the beginning of the game?
        private void setUp(Character character)
        {
            if (!dictionaryWeaponList.ContainsKey(character))
            {
                dictionaryWeaponList[character] = new List<GameObject>();
            }

            //foreach (Character c in CharacterManager.GetInstance._Characters)
            //{
            //    dictionaryWeaponList[c] = new List<GameObject>();
            //}
        }

        public void _AddWeapon(Character character, GameObject weapon)
        {
            setUp(character);

            List<GameObject> weaponList = dictionaryWeaponList[character];

            if (!weaponList.Contains(weapon))
            {
                weaponList.Add(weapon);
            }

            PlayerVault vault = GameManager.GetInstance._GetPlayerVault();
            vault._Owner = character;
            if (!vault._Items.Contains(weapon))
            {
                vault._Items.Add(weapon);
            }
            GameManager.GetInstance._GetItemHotbarContainer()._SetItemHeld(weapon);
        }

        public void _RemoveWeapon(Character c, GameObject weapon)
        {
            if (!dictionaryWeaponList.ContainsKey(c))
            {
                return;
            }

            List<GameObject> weaponList = dictionaryWeaponList[c];

            if (weaponList.Contains(weapon))
            {
                weaponList.Remove(weapon);
            }
        }

        public List<GameObject> _GetWeaponList(Character c)
        {
            if (dictionaryWeaponList.ContainsKey(c))
            {
                return dictionaryWeaponList[c];
            }
            return null;
        }
    }
}


