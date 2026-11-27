using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameManagersSystem;
using IndieGameInventorySystem;

namespace IndieGameGameControllerSystem
{
    [System.Obsolete("Deprecated Code", true)]
    public class GasStationEnvironmentManagerController : Singleton<GasStationEnvironmentManagerController>
    {
        public List<WeaponMelee> _MeleeWeapons = new List<WeaponMelee>();

        private void Awake()
        {
            WeaponMelee[] meleeWeapons = FindObjectsOfType<WeaponMelee>();

            foreach (WeaponMelee melee in meleeWeapons)
            {
                if (!_MeleeWeapons.Contains(melee))
                {
                    _MeleeWeapons.Add(melee);
                }
            }
        }
    }
}
