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
        public ItemSlotContainer _ActivatedItemSlotContainer;
        public int _SlotIndex = -2;

        [SerializeField] private ItemSlotContainer firstItemSlot;
        [SerializeField] private ItemSlotContainer lastItemSlot;

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
                if (null == firstItemSlot && null == lastItemSlot)
                {
                    firstItemSlot = container;
                    lastItemSlot = container;
                    container._ItemActiveHightLight.gameObject.SetActive(true);
                }
                else
                {
                    lastItemSlot = firstItemSlot;
                    firstItemSlot = container;
                    lastItemSlot._ItemActiveHightLight.gameObject.SetActive(false);
                    firstItemSlot._ItemActiveHightLight.gameObject.SetActive(true);
                }
                return true;
            }
            return false;
        }

        //switching item without animation happens
        public void _SwitchItem(Character character)
        {
            if (_SlotIndex == -2) return;

            //set those position and transform
            //last item slot goes to player vault
            //first item slot goes to player
            GameObject first = firstItemSlot._ItemObjectPrefab;
            GameObject last = lastItemSlot._ItemObjectPrefab;

            PlayerVault playerVault = GameManager.GetInstance._GetPlayerVault();
            playerVault._SetAsChildPosition(last);
            playerVault._SetWeaponPosition(character, first, "WeaponSlot");
            _SlotIndex = -2;//back to default index of hot bar slot
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

        public void _SetSlotActiveDefault()
        {
            _ActivatedItemSlotContainer._ItemActiveHightLight.gameObject.SetActive(false);
            _ActivatedItemSlotContainer = null;
        }

        public void _UpdateSlotContainer()
        {
            ItemSlotContainer container = _ActivatedItemSlotContainer;
            container._WeaponMeleeData = null;
            container._ItemImage.sprite = null;
            container._HasHeldItem = false;
            container._ItemObjectPrefab = null;
            container._ItemActiveHightLight.gameObject.SetActive(false);
        }
    }
}
