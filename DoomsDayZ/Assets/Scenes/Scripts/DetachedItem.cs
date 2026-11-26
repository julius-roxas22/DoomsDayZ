using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;
using IndieGameInventorySystem;
using IndieGameUISystem;

namespace IndieGameAnimationStateData
{
    //is simply detached the weapon on the characater on its position assigned
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/DetachedWeapon")]
    public class DetachedItem : BaseStateData
    {
        [Header("Basic Setup for Detach weapon")]
        [SerializeField] private float detachedTiming;
        [SerializeField] private float detachedRange;

        [Space(10)]
        [Header("Setup for throwable weapon")]
        [SerializeField] private bool throwable;
        private bool isDoneDetached;

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            isDoneDetached = false;
            //isThrowingSoundEffect = false;
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (stateInfo.normalizedTime >= detachedTiming && !isDoneDetached)
            {
                if (character._SubController.enabled)
                {
                    detachWeapon(character); // for normal player
                }
                else
                {
                    throwHeavyObject(character); // for npc boss thrower
                }
                isDoneDetached = true;
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            isDoneDetached = false;
        }

        private void throwHeavyObject(Character character)
        {
            //movable object
            //take damage the target
            //static object movement
            //destroy the object when collided something
            GameObject weapon = character._NPCBase._Weapon;

            if (null == weapon)
            {
                return;
            }

            WeaponHeavy procjectile = (WeaponHeavy)weapon.GetComponent<WeaponBase>();

            if (null == procjectile)
            {
                return;
            }

            weapon.transform.parent = null;
            procjectile._RigidBody = weapon.AddComponent<Rigidbody>();
            procjectile._RigidBody.AddForce(Vector3.up * 450f);
            procjectile._RigidBody.isKinematic = false;
            procjectile._Attacker = character;
            procjectile._IsThrowing = true;
            procjectile._MoveDirection = character._NPCBase._DetectedTarget.transform.position;// direction of the heavy object when its deploy
            procjectile._TriggeredEnable(); // timing to enable trigger
                                            // GameManager.GetInstance._GetPlayerVault()._RemoveItems();
                                            // WeaponManager.GetInstance._RemoveWeapon(character, procjectile.gameObject); // just to clear in the list
            character._NPCBase._Weapon = null;
        }

        private void detachWeapon(Character character)
        {
            PlayerVault playerVault = GameManager.GetInstance._GetPlayerVault();

            if (null == character._WeaponOnHand)
            {
                return;
            }

            GameObject weaponObject = playerVault._GetItemObject(character._WeaponOnHand);
            WeaponMelee weaponMelee = weaponObject.GetComponent<WeaponMelee>();
            weaponObject.transform.parent = null;
            character._CharacterRagdoll._SetupRagdollParts(); // reset the character ragdoll parts to detect the collision of every attack
            character._IsEquiped = false; // set the character to un equip
            playerVault._RemoveItems(weaponObject);
            Destroy(weaponObject.GetComponent<TriggerDetector>());
            weaponMelee._RigidBody = weaponObject.AddComponent<Rigidbody>();
            character._WeaponOnHand = null;

            if (!throwable) // just to drop infront of the character
            {
                weaponObject.transform.position = character.transform.position + ((character.transform.forward + Vector3.up * 0.35f) * detachedRange);
                weaponMelee._BoxCollider.isTrigger = false;
            }
            else // throw infront of the character
            {
                weaponMelee._IsThrowing = true;
                weaponMelee._Attacker = character; //set who is the thrower of this weapon
                weaponMelee.transform.position = character.transform.position + Vector3.up * 1.5f + (character.transform.forward * detachedRange); // start position
                weaponMelee.transform.rotation = Quaternion.LookRotation(character.transform.right);// just to face the right direction
                weaponMelee._MoveDirection = character.transform.forward;// just to move infront of the character
                weaponMelee.startPointPosition = weaponMelee.transform.position;
                weaponMelee._RigidBody.isKinematic = true;
                weaponMelee._BoxCollider.isTrigger = true;
                character._HealthSystem._DefenseData._CurrentStamina -= 20f; // deducted stamina when weapon throws it
            }
        }
    }
}
