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
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/WeaponFixedPosition")]
    public class WeaponFixedPosition : BaseStateData
    {
        [Header("Weapon Set up Details")]
        [SerializeField] private float fixedPositionTiming;
        [SerializeField] private string bodyPartName;//being parent of the item if weapon is grabbed
        [SerializeField] private float xScale;
        [SerializeField] private float yScale;
        [SerializeField] private float zScale;
        private bool isAnimated;

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            isAnimated = false;
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
            if (stateInfo.normalizedTime >= fixedPositionTiming && !isAnimated)
            {
                if (!character._SubController.enabled)
                {
                    grabbedObject(character);//for npc
                }
                else
                {
                    setWeaponPosition(character);//for player
                }
                Debug.Log("Weapon fixed");
                isAnimated = true;
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            isAnimated = false;
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

            GameObject weaponDataObject = playerVault._GetItemByData(hotBar._ActivatedItemSlotContainer._WeaponMeleeData);
            Transform parentTransform = character._BodyPartTransform(bodyPartName);
            WeaponMelee weaponMelee = weaponDataObject.GetComponent<WeaponMelee>();
            parentTransform.localPosition = weaponMelee._WeaponMeleeData._Position;
            parentTransform.localRotation = Quaternion.Euler(weaponMelee._WeaponMeleeData._Rotations);
            weaponDataObject.transform.parent = parentTransform;
            weaponDataObject.transform.localPosition = Vector3.zero;
            weaponDataObject.transform.localRotation = Quaternion.identity;
            weaponDataObject.SetActive(true);
            weaponDataObject.GetComponent<BoxCollider>().enabled = true;

            character._IsEquiped = true;
            character._CharacterRagdoll.setupRagdollParts();
            Destroy(weaponDataObject.GetComponent<Rigidbody>());
        }
    }
}
