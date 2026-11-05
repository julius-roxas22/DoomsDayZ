using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;
using IndieGameInventorySystem;
using IndieGameUISystem;

namespace IndieGameAnimationStateData
{
    //this script is a state to grab the weapon on fixed position on the hand of character
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/GrabbedAndEquipItem")]
    public class GrabbedAndEquipItem : BaseStateData
    {
        [Header("Weapon Set up Details")]
        [SerializeField] private float fixedPositionTiming;
        [SerializeField] private string bodyPartName;//being parent of the item if weapon is grabbed
        [SerializeField] private float xScale;
        [SerializeField] private float yScale;
        [SerializeField] private float zScale;

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            //add it into the weapon manager to handle it specific character with weapon list
            if (!character._SubController.enabled)
            {
                character._NPCBase._Weapon = PoolProjectileManager.GetInstance._GetProjectile(character, _EProjectileType.Rock);
                character._NPCBase._Weapon.SetActive(false);
                return;
            }

            List<Collider> itemsDetects = character._VicinityDetector._VicinityDetects;

            if (itemsDetects.Count > 0)//for now, its just a weapon
            {
                int rand = Random.Range(0, itemsDetects.Count - 1);
                WeaponManager.GetInstance._AddWeapon(character, itemsDetects[rand].gameObject);
            }
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (stateInfo.normalizedTime >= fixedPositionTiming)
            {
                if (!character._SubController.enabled)
                {
                    grabbedObject(character);//for npc
                }
                else
                {
                    setWeaponPosition(character);//for player
                }
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        //npc
        private void grabbedObject(Character character) // temporary initiate heavy object to throw by npc
        {
            if (null != character._NPCBase._Weapon)
            {
                character._NPCBase._Weapon.SetActive(true);
                character._NPCBase._Weapon.transform.parent = character._BodyPartTransform(bodyPartName); //being child object
                character._NPCBase._Weapon.transform.localPosition = Vector3.zero;
                character._NPCBase._Weapon.transform.localRotation = Quaternion.identity;
                character._NPCBase._Weapon.transform.localScale = new Vector3(xScale, yScale, zScale);
            }
        }

        //HACK: every imported character model should have empty gameobject and place into his hand
        //just to make sure, it is place on the right position
        //player
        private void setWeaponPosition(Character character)
        {
            PlayerVault playerVault = GameManager.GetInstance._GetPlayerVault();
            ItemHotBarContainer hotBar = GameManager.GetInstance._GetItemHotbarContainer();

            GameObject weaponObject = playerVault._GetItemByData(hotBar._ActivatedItemSlotContainer._WeaponMeleeData);
            Transform parentTransform = character._BodyPartTransform(bodyPartName);
            WeaponMelee weaponMelee = weaponObject.GetComponent<WeaponMelee>();
            parentTransform.localPosition = weaponMelee._WeaponMeleeData._Position;
            parentTransform.localRotation = Quaternion.Euler(weaponMelee._WeaponMeleeData._Rotations);
            weaponObject.transform.parent = parentTransform;
            weaponObject.transform.localPosition = Vector3.zero;
            weaponObject.transform.localRotation = Quaternion.identity;
            weaponObject.SetActive(true);
            weaponObject.GetComponent<BoxCollider>().enabled = true;
            //Rigidbody rb = weaponObject.GetComponent<Rigidbody>();
            //rb.useGravity = true;
            //rb.isKinematic = false;

            character._IsEquiped = true;
            character._CharacterRagdoll.setupRagdollParts();
            Destroy(weaponObject.GetComponent<Rigidbody>());


            //GameObject weapon = WeaponManager.GetInstance._GetWeaponList(character)[0];//get the first weapon in the list
            ////  WeaponMeleeData meleeData = weapon.GetComponent<WeaponMeleeData>();

            //WeaponMelee melee = weapon.GetComponent<WeaponMelee>();
            //WeaponMeleeData meleeData = melee._WeaponMeleeData;

            //if (null == meleeData) return;

            //Vector3 defaultScale = weapon.transform.localScale;
            //Transform parentTransform = character._BodyPartTransform(bodyPartName);
            //parentTransform.localPosition = meleeData._Position;
            //parentTransform.localRotation = Quaternion.Euler(meleeData._Rotations);
            //weapon.transform.parent = parentTransform;
            //weapon.transform.localPosition = Vector3.zero;
            //weapon.transform.localRotation = Quaternion.identity;
            //weapon.transform.localScale = defaultScale;

            //melee._ResetWeaponData();
            ////character._IsEquiped = true;
            //character._CharacterRagdoll.setupRagdollParts();
            //Destroy(weapon.GetComponent<Rigidbody>());
        }
    }
}
