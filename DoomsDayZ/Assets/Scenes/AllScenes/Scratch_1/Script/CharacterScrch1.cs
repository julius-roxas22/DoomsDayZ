using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;

public enum _EAnimationTransition
{
    MoveForward,
    MoveBackward,
    MoveRight,
    MoveLeft,
    MoveForwardRight,
    MoveForwardLeft,
    MoveBackwardRight,
    MoveBackwardLeft,
    Jump,
    NextTransitionAnim,
}

public class CharacterScrch1 : MonoBehaviour
{
    public Vector3 _MoveDirection = new Vector3();
    public Controller _Controller;
    public Rigidbody _CharacterRigidBody;

    private void Awake()
    {
        _Controller = GetComponent<Controller>();
        _CharacterRigidBody = GetComponent<Rigidbody>();
    }

    public void _Move(float speed)
    {
        //transform.position += (_MoveDirection.normalized * speed * Time.deltaTime);
        transform.Translate(_MoveDirection.normalized * speed * Time.deltaTime);
    }
}
