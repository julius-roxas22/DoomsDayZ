using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameWeaponSystem;
using IndieGameManagersSystem;
using IndieGameCharacterControllerSystem;

namespace IndieGameUISystem
{
    public class ItemHotBarContainer : MonoBehaviour
    {
        public List<ItemSlotContainer> _ItemSlotContainerList = new List<ItemSlotContainer>();
        [Header("Temporary items holder")]
        [SerializeField] private List<GameObject> heldItems = new List<GameObject>();

        public void _AddItemSlotContainer(ItemSlotContainer slotContainer)
        {
            if (!_ItemSlotContainerList.Contains(slotContainer))
            {
                _ItemSlotContainerList.Add(slotContainer);
            }
        }

        public void _SetItemHeld()
        {
            List<GameObject> items = GameManager.GetInstance._GetPlayerVault()._Items;
            heldItems.AddRange(items);

            for (int i = 0; i < _ItemSlotContainerList.Count; i++)
            {
                if (!_ItemSlotContainerList[i]._HasHeldItem)
                {
                    GameObject obj = heldItems[0];
                    WeaponMeleeData weaponMeleeData = obj.GetComponent<WeaponMelee>()._WeaponMeleeData;
                    _ItemSlotContainerList[i]._WeaponMeleeData = weaponMeleeData;
                    _ItemSlotContainerList[i]._ItemImage.sprite = weaponMeleeData._WeaponSpriteImage;
                    _ItemSlotContainerList[i]._HasHeldItem = true;
                    heldItems.Clear();
                    break;
                }
                else
                {
                    Debug.Log("No available Item slot");
                }
            }
        }

        public void _UpdateItemHeld(WeaponMeleeData weaponMeleeData)
        {
            for (int i = 0; i < _ItemSlotContainerList.Count; i++)
            {
                if (weaponMeleeData == _ItemSlotContainerList[i]._WeaponMeleeData && _ItemSlotContainerList[i]._HasHeldItem)
                {
                    _ItemSlotContainerList[i]._WeaponMeleeData = null;
                    _ItemSlotContainerList[i]._ItemImage.sprite = null;
                    _ItemSlotContainerList[i]._HasHeldItem = false;
                }
            }
        }
    }
}
