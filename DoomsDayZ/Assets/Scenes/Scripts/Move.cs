using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameHealthSystem;

namespace IndieGameAnimationStateData
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/Ability/Move")]
    public class Move : BaseStateData
    {
        [SerializeField] private bool disableController;
        [SerializeField] private AnimationCurve speedGraph;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;
        //     [SerializeField] private LayerMask blockingLayers;

        private float moveZ;
        private float moveX;
        private AudioSource audioSource;

        [SerializeField] private float blockingDistance = 0.75f;

        public override void OnEnterAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            moveX = 0f;
            moveZ = 0;
            character._MoveDirection = Vector3.zero;
        }

        public override void OnUpdateAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            HealthSystem healthSystem = character._HealthSystem;

            if (healthSystem._DefenseData._CurrentStamina < healthSystem._DefenseData._MaximumStamina)
            {
                healthSystem._RegenerateStamina(character);// regenerating while player stopped
            }

            if (healthSystem._DefenseData._CurrentHealth < healthSystem._DefenseData._MaximumHealth)
            {
                healthSystem._RegenerateHealth(character);//health regeneration every frame
            }

            if ((character._Controller._MoveForward && character._Controller._MoveBackward) || (character._Controller._MoveLeft && character._Controller._MoveRight))
            {
                animator.SetBool(_ETransitionParameters.Move.ToString(), false);
                return;
            }

            if (!character._Controller._MoveForward && !character._Controller._MoveBackward && !character._Controller._MoveLeft && !character._Controller._MoveRight)
            {
                animator.SetBool(_ETransitionParameters.Move.ToString(), false);
            }

            if (character._Controller._MoveForward)
            {
                moveZ = 1f;
                moveX = 0;
            }

            if (character._Controller._MoveBackward)
            {
                moveZ = -1f;
                moveX = 0;
            }

            if (character._Controller._MoveLeft)
            {
                moveZ = 0;
                moveX = -1f;
            }

            if (character._Controller._MoveRight)
            {
                moveZ = 0;
                moveX = 1f;
            }

            if (character._Controller._MoveForwardLeft)
            {
                moveZ = 1f;
                moveX = -1f;
            }

            if (character._Controller._MoveForwardRight)
            {
                moveZ = 1f;
                moveX = 1f;
            }

            if (character._Controller._MoveBackwardLeft)
            {
                moveZ = -1f;
                moveX = -1f;
            }

            if (character._Controller._MoveBackwardRight)
            {
                moveZ = -1f;
                moveX = 1f;
            }

            character._MoveDirection = new Vector3(moveX, 0f, moveZ);

            if (!disableController)
            {
                useController(character, stateInfo);
            }
            else
            {
                useConstantController(character, stateInfo);
            }
        }

        private void useController(Character character, AnimatorStateInfo stateInfo)
        {
            float speed = movementSpeed + character._HealthSystem._DefenseData._AdditionalMovementSpeed;//base values and pre-initialized values for movement speed

            if (character._Controller._MoveForward || character._Controller._MoveBackward || character._Controller._MoveLeft || character._Controller._MoveRight
                || character._Controller._MoveForwardLeft || character._Controller._MoveForwardRight || character._Controller._MoveBackwardLeft || character._Controller._MoveBackwardRight)
            {
                if (!isBlocking(character))
                {
                    character._CharacterMove(character._MoveDirection, speed, speedGraph.Evaluate(stateInfo.normalizedTime));
                }

                Quaternion lookDdir = Quaternion.LookRotation(character._MoveDirection);
                character.transform.rotation = Quaternion.Slerp(character.transform.rotation, lookDdir, rotationSpeed * Time.deltaTime);
            }
        }

        private void useConstantController(Character character, AnimatorStateInfo stateInfo)
        {
            if (!isBlocking(character))
            {
                character._CharacterMove(character.transform.forward, movementSpeed, speedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }

        public override void OnExitAnimation(Character character, AnimatorStateInfo stateInfo, Animator animator)
        {
            moveX = 0f;
            moveZ = 0;
            character._MoveDirection = Vector3.zero;

            if (null != audioSource)
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
        }

        private bool isBlocking(Character character)
        {
            if (Physics.Raycast(character.transform.position, character.transform.forward, out RaycastHit hit, blockingDistance))
            {
                Debug.DrawRay(/*character.transform.position*/character._BoxCollider.bounds.center, character.transform.forward * blockingDistance, Color.green);

                if (!isBodyParts(hit.collider) && !IndieGameInventorySystem.WeaponBase._IsWeaponProjectile(hit.collider))//just to check if it is weapon
                {
                    return true;
                }
            }
            return false;
        }

        private bool isBodyParts(Collider col)
        {
            Character triggeredCharacter = col.transform.root.GetComponent<Character>();

            if (null == triggeredCharacter)
            {
                return false;
            }

            if (col.gameObject == triggeredCharacter.gameObject)
            {
                return false;
            }

            if (triggeredCharacter._CharacterRagdoll._RagdollParts.Contains(col))
            {
                return true;
            }

            return false;
        }
    }
}