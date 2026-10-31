using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IndieGameInventorySystem;
using IndieGameManagersSystem;

namespace IndieGameUISystem
{
    public class ItemSlotContainer : MonoBehaviour
    {
        public bool _ItemSetActive;
        public bool _HasHeldItem;
        public Image _ItemImage;
        public WeaponMeleeData _WeaponMeleeData;

        private void Awake()
        {
            _ItemImage = transform.GetChild(0).GetComponent<Image>();
        }
    }
}
