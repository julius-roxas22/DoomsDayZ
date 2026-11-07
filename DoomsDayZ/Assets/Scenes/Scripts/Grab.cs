using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;
using IndieGameUISystem;

namespace IndieGameAnimationStateData
{
    //this script is a state to grab the weapon with timing
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/Grab")]
    public class Grab : BaseStateData
    {
        [SerializeField] private float animationTiming;
        [SerializeField] private bool disarmWeapon;
        [SerializeField] private bool grabItem;
        [SerializeField] private string bodyPartName;
        private bool isAnimatated;

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
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
                setPosition(obj);
                //  PlayerVault playerVault = GameManager.GetInstance._GetPlayerVault();
                //   obj.transform.parent = playerVault.transform;
                //obj.GetComponent<BoxCollider>().enabled = false;
                //Rigidbody rb = obj.GetComponent<Rigidbody>();
                //rb.useGravity = false;
                //rb.isKinematic = true;
                //obj.transform.localPosition = Vector3.zero;
                //obj.transform.localRotation = Quaternion.identity;
                //obj.SetActive(false);
            }
        }

        private void disArmWeapon(Character character)
        {
            Transform bodyPartTransform = character._BodyPartTransform(bodyPartName); // get the parent transform
            GameObject obj1 = bodyPartTransform.GetChild(0).gameObject; //get the weapon from parent
            setPosition(obj1);
            character._IsEquiped = false;
        }

        private void setPosition(GameObject obj)
        {
            PlayerVault playerVault = GameManager.GetInstance._GetPlayerVault();
            obj.transform.parent = playerVault.transform;
            obj.GetComponent<BoxCollider>().enabled = false;
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (null != rb)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.SetActive(false);
        }
    }
}
