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
        [SerializeField] private float grabTiming;
        private bool isGrabbed;

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            isGrabbed = false;
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (stateInfo.normalizedTime >= grabTiming && !isGrabbed)
            {
                List<Collider> itemsDetects = character._VicinityDetector._VicinityDetects;

                if (itemsDetects.Count > 0)//for now, its just a weapon
                {
                    int rand = Random.Range(0, itemsDetects.Count - 1);
                    GameObject obj = itemsDetects[rand].gameObject;
                    WeaponManager.GetInstance._AddWeapon(character, obj);
                    PlayerVault playerVault = GameManager.GetInstance._GetPlayerVault();
                    obj.transform.parent = playerVault.transform;
                    obj.GetComponent<BoxCollider>().enabled = false;
                    Rigidbody rb = obj.GetComponent<Rigidbody>();
                    rb.useGravity = false;
                    rb.isKinematic = true;
                    obj.transform.localPosition = Vector3.zero;
                    obj.transform.localRotation = Quaternion.identity;
                    obj.SetActive(false);
                    isGrabbed = true;
                }
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            isGrabbed = false;
        }
    }
}
