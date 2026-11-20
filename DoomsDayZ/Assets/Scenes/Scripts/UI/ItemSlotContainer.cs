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
        public bool _HasHeldItem;
        public Image _ItemImage;
        public Image _ItemActiveHightLight;
        public WeaponMeleeData _WeaponMeleeData;
        public GameObject _ItemObjectPrefab;

        private void Awake()
        {
            _ItemImage = transform.GetChild(0).GetComponent<Image>();
            _ItemActiveHightLight = transform.GetChild(1).GetComponent<Image>();
            _ItemActiveHightLight.gameObject.SetActive(false);//default set to active; it will active only when its equiped
        }
    }
}
