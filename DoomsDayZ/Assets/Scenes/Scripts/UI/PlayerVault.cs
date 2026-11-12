using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameInventorySystem;

namespace IndieGameUISystem
{
    public class PlayerVault : MonoBehaviour
    {
        public Character _Owner;
        public List<GameObject> _Items = new List<GameObject>();

        public void _RemoveItems(GameObject item)
        {
            for (int i = 0; i < _Items.Count; i++)
            {
                if (_Items[i] == item)
                {
                    _Items.RemoveAt(i);
                }
            }
        }

        //just temporary searching by weapon items only
        public GameObject _GetItemByData(WeaponMeleeData weaponMeleeData)
        {
            foreach (GameObject i in _Items)
            {
                WeaponMeleeData data = i.GetComponent<WeaponMelee>()._WeaponMeleeData;

                if (data == weaponMeleeData)
                {
                    return i;
                }
            }

            return null;
        }

        public void _SetWeaponPosition(bool isEquip, Character character, GameObject weaponObject, string bodyPartName)
        {
            Transform parentTransform = character._BodyPartTransform(bodyPartName);
            WeaponMelee weaponMelee = weaponObject.GetComponent<WeaponMelee>();
            parentTransform.localPosition = weaponMelee._WeaponMeleeData._Position;
            parentTransform.localRotation = Quaternion.Euler(weaponMelee._WeaponMeleeData._Rotations);
            weaponObject.transform.parent = parentTransform;
            weaponObject.transform.localPosition = Vector3.zero;
            weaponObject.transform.localRotation = Quaternion.identity;
            weaponObject.SetActive(true);
            weaponObject.GetComponent<BoxCollider>().enabled = true;

            character._IsEquiped = isEquip;
            character._CharacterRagdoll._SetupRagdollParts();
            Destroy(weaponObject.GetComponent<Rigidbody>());
        }
        public void _SetWeaponPosition(Character character, GameObject weaponObject, string bodyPartName)
        {
            Transform parentTransform = character._BodyPartTransform(bodyPartName);
            WeaponMelee weaponMelee = weaponObject.GetComponent<WeaponMelee>();
            parentTransform.localPosition = weaponMelee._WeaponMeleeData._Position;
            parentTransform.localRotation = Quaternion.Euler(weaponMelee._WeaponMeleeData._Rotations);
            weaponObject.transform.parent = parentTransform;
            weaponObject.transform.localPosition = Vector3.zero;
            weaponObject.transform.localRotation = Quaternion.identity;
            weaponObject.SetActive(true);
            weaponObject.GetComponent<BoxCollider>().enabled = true;

            character._CharacterRagdoll._SetupRagdollParts();
            Destroy(weaponObject.GetComponent<Rigidbody>());
        }

        public void _SetAsChildPosition(GameObject obj)
        {
            obj.transform.parent = transform;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.GetComponent<BoxCollider>().enabled = false;
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (null != rb)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }
            obj.SetActive(false);
        }
    }
}
