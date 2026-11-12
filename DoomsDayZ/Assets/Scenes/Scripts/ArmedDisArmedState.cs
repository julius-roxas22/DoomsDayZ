using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;
using IndieGameUISystem;

namespace IndieGameAnimationStateData
{
    //this script is a state to grab the weapon with timing
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/ArmedDisArmedState")]
    public class ArmedDisArmedState : BaseStateData
    {
        [SerializeField] private float animationTiming;
        [SerializeField] private bool disarmWeapon;
        [SerializeField] private bool grabItem;
        [SerializeField] private string bodyPartName;
        private bool isAnimatated;
        private PlayerVault playerVault;

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (null == playerVault)
            {
                playerVault = GameManager.GetInstance._GetPlayerVault();
            }

            if (grabItem && disarmWeapon)
            {
                grabItem = false;
                disarmWeapon = false;
            }

            isAnimatated = false;
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (stateInfo.normalizedTime >= animationTiming && !isAnimatated)
            {
                if (grabItem)
                {
                    grab(character);
                }
                else if (disarmWeapon)
                {
                    disArmWeapon(character);
                }
                isAnimatated = true;
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            isAnimatated = false;
        }

        private void grab(Character character)
        {
            List<Collider> itemsDetects = character._VicinityDetector._VicinityDetects;

            if (itemsDetects.Count > 0)//for now, its just a weapon
            {
                int rand = Random.Range(0, itemsDetects.Count - 1);
                GameObject obj = itemsDetects[rand].gameObject;
                WeaponManager.GetInstance._AddWeapon(character, obj);
                playerVault._SetAsChildPosition(obj);
            }
        }

        private void disArmWeapon(Character character)
        {
            Transform bodyPartTransform = character._BodyPartTransform(bodyPartName); // get the parent transform
            GameObject obj1 = bodyPartTransform.GetChild(0).gameObject; //get the weapon from parent
            playerVault._SetAsChildPosition(obj1);
            character._IsEquiped = false;
            character._CharacterRagdoll._SetupRagdollParts();
        }
    }
}
