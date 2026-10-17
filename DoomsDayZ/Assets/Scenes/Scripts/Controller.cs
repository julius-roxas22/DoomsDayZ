using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameCharacterControllerSystem
{

    public enum _ETransitionParameters
    {
        Move,
        Jump,
        NextState,
        IsGrounded,
        Attack,
        IsGrabbingWeapon,
        IsDroppingWeapon,
        TransitionIndex,
        TurnLeft,
        TurnRight,
        ThreeSixtyAttack,
        Throw,
        Argonizing,
        Sprint,
    }

    public class Controller : MonoBehaviour
    {
        [Header("Button Key Controller")]
        public bool _MoveForward;
        public bool _MoveBackward;
        public bool _MoveLeft;
        public bool _MoveRight;
        public bool _MoveForwardLeft;
        public bool _MoveForwardRight;
        public bool _MoveBackwardLeft;
        public bool _MoveBackwardRight;
        public bool _Jump;
        public bool _Attack;
        public bool _IsGrabbingWeapon;
        public bool _IsDroppingWeapon;
        public bool _IsPressedKeyR;

        [Space(17)]
        [Header("Animation Progress")]
        public bool _IsJumped; // this is only for jump animation not a key press
    }
}


