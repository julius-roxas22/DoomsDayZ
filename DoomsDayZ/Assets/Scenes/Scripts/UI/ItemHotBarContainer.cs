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
                    return true;
                }
            }
            return false;
        }

        public void _SwitchItem(ItemSlotContainer slot, Character character, PlayerVault playerVault)
        {
            if (_SlotIndex == -2) return;

            lastItemSlot = firstItemSlot;
            firstItemSlot = slot;

            //set those position and transform
            //last item slot goes to player vault
            //first item slot goes to player

            GameObject first = firstItemSlot._ItemObjectPrefab;
            GameObject last = lastItemSlot._ItemObjectPrefab;

            playerVault._SetAsChildPosition(last);
            Transform parentTransform = character._BodyPartTransform("WeaponSlot");
            WeaponMelee weaponMelee = first.GetComponent<WeaponMelee>();
            parentTransform.localPosition = weaponMelee._WeaponMeleeData._Position;
            parentTransform.localRotation = Quaternion.Euler(weaponMelee._WeaponMeleeData._Rotations);
            first.transform.parent = parentTransform;
            first.transform.localPosition = Vector3.zero;
            first.transform.localRotation = Quaternion.identity;
            first.SetActive(true);
            first.GetComponent<BoxCollider>().enabled = true;
            character._CharacterRagdoll.setupRagdollParts();
            Destroy(first.GetComponent<Rigidbody>());
            _SlotIndex = -2;
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
