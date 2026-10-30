using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;
using IndieGameInventorySystem;

namespace IndieGameAnimationStateData
{
    //is simply detached the weapon on the characater on its position assigned
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/DetachedWeapon")]
    public class DetachedWeapon : BaseStateData
    {
        [Header("Basic Setup for Detach weapon")]
        [SerializeField] private float detachedTiming;
        [SerializeField] private float detachedRange;

        [Space(10)]
        [Header("Setup for throwable weapon")]
        [SerializeField] private bool throwable;

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            //isThrowingSoundEffect = false;
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (stateInfo.normalizedTime >= detachedTiming)
            {
                if (character._SubController.enabled)
                {
                    detachWeapon(character); // for normal player
                }
                else
                {
                    detachHeavyWeapon(character); // for npc boss thrower
                }
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        private void detachHeavyWeapon(Character character)
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
            WeaponManager.GetInstance._RemoveWeapon(character, procjectile.gameObject); // just to clear in the list
            character._NPCBase._Weapon = null;
        }

        private void detachWeapon(Character character)
        {
            List<GameObject> weaponList = WeaponManager.GetInstance._GetWeaponList(character); // get the weapon list of the specific character

            if (weaponList.Count == 0)
            {
                return;
            }

            GameObject weapon = weaponList[0];
            WeaponMelee projectile = weapon.GetComponent<WeaponMelee>();
            weapon.transform.parent = null;
            character._CharacterRagdoll.setupRagdollParts(); // reset the character ragdoll parts to detect the collision of every attack
            character._IsEquiped = false; // set the character to un equip
            WeaponManager.GetInstance._RemoveWeapon(character, weapon); // remove from the weapon list of specific character
            Destroy(weapon.GetComponent<TriggerDetector>());
            projectile._RigidBody = weapon.AddComponent<Rigidbody>();

            if (!throwable) // just to drop infront of the character
            {
                weapon.transform.position = character.transform.position + ((character.transform.forward + Vector3.up * 0.35f) * detachedRange);
                projectile._BoxCollider.isTrigger = false;
            }
            else // throw infront of the character
            {
                projectile._IsThrowing = true;
                projectile._Attacker = character; //set who is the thrower of this weapon
                projectile.transform.position = character.transform.position + Vector3.up * 1.5f + (character.transform.forward * detachedRange); // start position
                projectile.transform.rotation = Quaternion.LookRotation(character.transform.right);// just to face the right direction
                projectile._MoveDirection = character.transform.forward;// just to move infront of the character
                projectile.startPointPosition = projectile.transform.position;
                projectile._RigidBody.isKinematic = true;
                projectile._BoxCollider.isTrigger = true;
                character._HealthSystem._DefenseData._CurrentStamina -= 20f; // deducted stamina when weapon throws it
            }
        }
    }
}
