using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using IndieGameManagersSystem;
using IndieGameCharacterControllerSystem;
using IndieGameWeaponSystem;

namespace IndieGameUISystem
{
    public class ItemCanvasContainer : CanvasContainerBase
    {
        [SerializeField] private TMP_Text weaponNameText;
        [SerializeField] private TMP_Text weaponDamageText;
        [SerializeField] private TMP_Text weaponHandlingText;
        [SerializeReference] private GameObject itemDetailsrootParent;

        private WeaponMelee weaponMelee;

        protected override void Awake()
        {
            base.Awake();
            weaponMelee = GetComponentInParent<WeaponMelee>();
            itemDetailsrootParent.SetActive(false);
        }

        protected override void Update()
        {
            base.Update();

            bool isShowsUpPanel = false;

            //foreach (Character c in CharacterManager.GetInstance._Characters)
            //{
            //    if (null == c._EnvironmentDetectedList)
            //    {
            //        continue;
            //    }

            //    if (c._EnvironmentDetectedList.Contains(weaponMelee.gameObject))
            //    {
            //        if (!isShowsUpPanel)
            //        {
            //            isShowsUpPanel = true;
            //        }
            //        break;
            //    }
            //}

            if (isShowsUpPanel)
            {
                transform.position = weaponMelee.transform.position + Vector3.up * 2.1f;
                weaponNameText.SetText(weaponMelee.name);
                weaponDamageText.SetText("Damage: " + weaponMelee._WeaponMeleeData._Damage);
                weaponHandlingText.SetText("Weapon Handling: " + weaponMelee._WeaponMeleeData._WeaponHandling);
            }

            itemDetailsrootParent.SetActive(isShowsUpPanel);
        }
    }
}