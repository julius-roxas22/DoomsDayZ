using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;

[CreateAssetMenu(fileName = "New Ability", menuName = "IndieGamePractice/{TEMP}Create Ability/UpperLayer/BaseLayerMove")]
public class BaseLayerMoveScrch1 : BaseStateDataScrch1
{
    [SerializeField] private float movemengSpeed;

    [SerializeField] private float acceleration = 40f;
    [SerializeField] private float deceleration = 50f;

    [SerializeField] private float velocityZ = 0.01f;
    [SerializeField] private float velocityX;

    private int MoveX; // animation parameters
    private int MoveZ; // animation parameters

    public override void _OnEnterAnimation(CharacterScrch1 character, Animator animator, AnimatorStateInfo stateInfo)
    {
        character._MoveDirection = Vector3.zero;
        MoveX = Animator.StringToHash("MoveX");
        MoveZ = Animator.StringToHash("MoveZ");
    }

    public override void _OnUpdateAnimation(CharacterScrch1 character, Animator animator, AnimatorStateInfo stateInfo)
    {
        Controller controller = character._Controller;

        float targetZ = 0f;
        float targetX = 0f;

        //move z position
        if (controller._MoveForward && !controller._MoveBackward)
        {
            targetZ = 1f;
        }
        else if (controller._MoveBackward && !controller._MoveForward)
        {
            targetZ = -1f;
        }

        //move x position
        if (controller._MoveRight && !controller._MoveLeft)
        {
            targetX = 1f;
        }
        else if (controller._MoveLeft && !controller._MoveRight)
        {
            targetX = -1f;
        }

        if (controller._Jump)
        {
            animator.SetBool(_EAnimationTransition.Jump.ToString(), true);
        }

        #region enable/disable upper layer animation
        //float targetWeight = 0f;

        //if (Input.GetKey(KeyCode.X))
        //{
        //    targetWeight = 1;
        //}
        //else if (Input.GetKey(KeyCode.Z))
        //{
        //    targetWeight = 0;
        //}

        //float weight = animator.GetLayerWeight(1);
        //weight = Mathf.MoveTowards(weight, targetWeight, 5f * Time.deltaTime);
        //animator.SetLayerWeight(1, weight); 
        #endregion

        velocityZ = Mathf.MoveTowards(velocityZ, targetZ, (targetZ == 0 ? deceleration : acceleration) * Time.deltaTime);
        velocityX = Mathf.MoveTowards(velocityX, targetX, (targetX == 0 ? deceleration : acceleration) * Time.deltaTime);

        character._MoveDirection = new Vector3(velocityX, 0f, velocityZ);

        if (character._MoveDirection != Vector3.zero)
        {
            float speed = character._MoveDirection.magnitude * movemengSpeed;
            if (character._MoveDirection.z < 0f)
            {
                speed = character._MoveDirection.magnitude * (movemengSpeed * 0.7f); //movemend speed backward reduce
            }
            character._Move(speed);
        }

        animator.SetFloat(MoveZ, velocityZ);
        animator.SetFloat(MoveX, velocityX);
    }

    public override void _OnExitAnimation(CharacterScrch1 character, Animator animator, AnimatorStateInfo stateInfo)
    {
        character._MoveDirection = Vector3.zero;
    }
}
