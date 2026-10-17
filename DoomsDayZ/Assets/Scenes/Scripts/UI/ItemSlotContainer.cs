using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IndieGameWeaponSystem;
using IndieGameManagersSystem;

namespace IndieGameUISystem
{
    public class ItemSlotContainer : MonoBehaviour
    {
        public bool _HasHeldItem;
        public Image _ItemImage;
        public WeaponMeleeData _WeaponMeleeData;

        private ItemHotBarContainer hotbarContainer;

        private void Awake()
        {
            _ItemImage = transform.GetChild(0).GetComponent<Image>();
            hotbarContainer = GetComponentInParent<ItemHotBarContainer>();
            hotbarContainer._AddItemSlotContainer(this);
        }
    }
}
