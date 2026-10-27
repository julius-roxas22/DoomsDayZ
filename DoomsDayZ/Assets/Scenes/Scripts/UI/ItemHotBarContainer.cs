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
            //heldItems.AddRange(items);
            foreach(GameObject o in items)
            {
                if (!heldItems.Contains(o))
                {
                    heldItems.Add(o);
                }
            }

            ItemSlotContainer slotContainer = _GetItemIsNotHeld();

            if(null != slotContainer)
            {
                GameObject obj = heldItems[0];
                WeaponMeleeData weaponMeleeData = obj.GetComponent<WeaponMelee>()._WeaponMeleeData;
                slotContainer._WeaponMeleeData = weaponMeleeData;
                slotContainer._ItemImage.sprite = weaponMeleeData._WeaponSpriteImage;
                slotContainer._HasHeldItem = true;
                heldItems.Clear();
            }
            else
            {
                Debug.Log("No available Item slot");
            }

            //for (int i = 0; i < _ItemSlotContainerList.Count; i++)
            //{
            //    if (!_ItemSlotContainerList[i]._HasHeldItem)
            //    {
            //        GameObject obj = heldItems[0];
            //        WeaponMeleeData weaponMeleeData = obj.GetComponent<WeaponMelee>()._WeaponMeleeData;
            //        _ItemSlotContainerList[i]._WeaponMeleeData = weaponMeleeData;
            //        _ItemSlotContainerList[i]._ItemImage.sprite = weaponMeleeData._WeaponSpriteImage;
            //        _ItemSlotContainerList[i]._HasHeldItem = true;
            //        heldItems.Clear();
            //        break;
            //    }
            //    else
            //    {
            //        Debug.Log("No available Item slot");
            //    }
            //}
        }

        public ItemSlotContainer _GetItemIsNotHeld()
        {
            for(int i = 0; i < _ItemSlotContainerList.Count; i++)
            {
                if (!_ItemSlotContainerList[i]._HasHeldItem)
                {
                    return _ItemSlotContainerList[i];
                }
            }
            return null;
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
