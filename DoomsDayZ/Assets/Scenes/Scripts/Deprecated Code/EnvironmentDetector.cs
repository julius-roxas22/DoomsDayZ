using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;

namespace IndieGameAnimationStateData
{
    [System.Obsolete("Deprecated Code", true)]
    //in general detector of object in environments; just to interact, pick something etc...
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/EnvironmentDetector")]
    public class EnvironmentDetector : BaseStateData
    {
        //in the future this is usefull when we detect some other survivor or something in environment
        [SerializeField] private float characterRadiusDetector;
        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            environmentDetected(character);
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        private void environmentDetected(Character character)
        {
            Collider[] colliders = Physics.OverlapSphere(character.transform.position, characterRadiusDetector);

            List<GameObject> currentObjectDetected = new List<GameObject>();

            foreach (Collider col in colliders)
            {
                if (character._CharacterRagdoll._RagdollParts.Contains(col))
                {
                    continue;
                }

                if (col.gameObject == character.gameObject)
                {
                    continue;
                }

                if (!currentObjectDetected.Contains(col.gameObject))
                {
                    currentObjectDetected.Add(col.gameObject);
                }
            }

            //foreach (GameObject c in currentObjectDetected)
            //{
            //    if (!character._EnvironmentDetectedList.Contains(c))
            //    {
            //        character._EnvironmentDetectedList.Add(c);
            //    }
            //}

            //character._EnvironmentDetectedList.RemoveAll(c => !currentObjectDetected.Contains(c));
        }
    }
}
