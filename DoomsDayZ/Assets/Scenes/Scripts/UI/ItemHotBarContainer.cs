using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameInventorySystem;
using IndieGameManagersSystem;
using IndieGameCharacterControllerSystem;

namespace IndieGameUISystem
{
    //public enum _EActivatedSlotItemKey
    //{
    //    None,
    //    InputKey1,
    //    InputKey2,
    //    InputKey3,
    //    InputKey4,
    //}

    public class ItemHotBarContainer : MonoBehaviour
    {
        public List<ItemSlotContainer> _ItemSlotContainerList = new List<ItemSlotContainer>();
        public ItemSlotContainer _ActivatedItemSlotContainer;
        public int _SlotIndex = -1;
        public GameObject firstSlot;
        public GameObject lastSlot;

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

        public bool _CheckItemHeld() //check the slot container if does have item held
        {
            ItemSlotContainer container = _ItemSlotContainerList[_SlotIndex];
            _ActivatedItemSlotContainer = container;
            if (container._HasHeldItem)
            {
                return true;
            }
            return false;
        }

        public void _SwitchItem(GameObject newSlot)
        {

            if (null == firstSlot && null == lastSlot)
            {
                firstSlot = newSlot;
                lastSlot = newSlot;
            }
            else
            {
                lastSlot = firstSlot;
                firstSlot = newSlot;
            }
        }

        public void _SetItemHeld(GameObject obj) //when pick up the item it will add in weapon manager
        {
            ItemSlotContainer container = getSlotContainerHasntHeldItem;
            WeaponMelee melee = obj.GetComponent<WeaponMelee>();
            WeaponMeleeData weaponMeleeData = melee._WeaponMeleeData;
            container._ItemImage.sprite = weaponMeleeData._WeaponSpriteImage;
            container._WeaponMeleeData = weaponMeleeData;
            container._ItemObjectPrefab = obj;
            container._HasHeldItem = true;
        }

        private ItemSlotContainer getSlotContainerHasntHeldItem // get the slot container is not already held item
        {
            get
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
        }

        public ItemSlotContainer _GetItemActive // get the slot container active equiped
        {
            get
            {
                foreach (ItemSlotContainer c in _ItemSlotContainerList)
                {
                    if (c._ItemSetActive)
                    {
                        return c;
                    }
                }
                return null;
            }
        }

        //public void _UpdateItemHeld(WeaponMeleeData weaponMeleeData)
        //{
        //    for (int i = 0; i < _ItemSlotContainerList.Count; i++)
        //    {
        //        ItemSlotContainer container = _ItemSlotContainerList[i];
        //        if (weaponMeleeData == container._WeaponMeleeData && container._HasHeldItem)
        //        {
        //            _ItemSlotContainerList[i]._WeaponMeleeData = null;
        //            _ItemSlotContainerList[i]._ItemImage.sprite = null;
        //            _ItemSlotContainerList[i]._HasHeldItem = false;
        //        }
        //    }
        //}
    }
}
