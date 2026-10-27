using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;
using IndieGameWeaponSystem;

namespace IndieGameAnimationStateData
{
    //this script is a state to grab the weapon with timing
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/Grab")]
    public class Grab : BaseStateData
    {
        [SerializeField] private float grabTiming;

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (stateInfo.normalizedTime >= grabTiming)
            {
                List<Collider> itemsDetects = character._VicinityDetector._VicinityDetects;

                if (itemsDetects.Count > 0)//for now, its just a weapon
                {
                    int rand = Random.Range(0, itemsDetects.Count - 1);
                    WeaponManager.GetInstance._AddWeapon(character, itemsDetects[rand].gameObject);
                }
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }
    }
}
