using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameInventorySystem;
using IndieGameManagersSystem;
using IndieGameCharacterControllerSystem;

namespace IndieGameUISystem
{
    public class ItemHotBarContainer : MonoBehaviour
    {
        public List<ItemSlotContainer> _ItemSlotContainerList = new List<ItemSlotContainer>();

        public void _AddItemSlotContainer(ItemSlotContainer slotContainer)
        {
            if (!_ItemSlotContainerList.Contains(slotContainer))
            {
                _ItemSlotContainerList.Add(slotContainer);
            }
        }

        public bool _CheckItemSlotIsFull
        {
            get
            {
                for (int i = 0; i < _ItemSlotContainerList.Count; i++)
                {
                    if (!_ItemSlotContainerList[i]._HasHeldItem)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public void _SetItemHeld(GameObject obj)
        {
            ItemSlotContainer container = _GetSlotContainerHasntHeldItem();
            WeaponMelee melee = obj.GetComponent<WeaponMelee>();
            WeaponMeleeData weaponMeleeData = melee._WeaponMeleeData;
            container._ItemImage.sprite = weaponMeleeData._WeaponSpriteImage;
            container._WeaponMeleeData = weaponMeleeData;
            container._HasHeldItem = true;
        }

        private ItemSlotContainer _GetSlotContainerHasntHeldItem()
        {
            foreach (ItemSlotContainer c in _ItemSlotContainerList)
            {
                if (!c._HasHeldItem)
                {
                    return c;
                }
            }
            return null;
        }

        public void _UpdateItemHeld(WeaponMeleeData weaponMeleeData)
        {
            for (int i = 0; i < _ItemSlotContainerList.Count; i++)
            {
                ItemSlotContainer container = _ItemSlotContainerList[i];
                if (weaponMeleeData == container._WeaponMeleeData && container._HasHeldItem)
                {
                    _ItemSlotContainerList[i]._WeaponMeleeData = null;
                    _ItemSlotContainerList[i]._ItemImage.sprite = null;
                    _ItemSlotContainerList[i]._HasHeldItem = false;
                }
            }
        }
    }
}
