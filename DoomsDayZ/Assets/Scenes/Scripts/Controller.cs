using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameCharacterControllerSystem
{
    //the following variables are drive by sub controller

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

        [Header("Interaction Keys")]
        public bool _IsGrabbingWeapon;
        public bool _IsDroppingWeapon;
        public bool _IsPressedKeyR;
        public bool _IsPressKeyX;

        [Header("Hot bar Keys")]
        public bool _IsPressedKey1;
        public bool _IsPressedKey2;
        public bool _IsPressedKey3;
        public bool _IsPressedKey4;

        [Space(17)]
        [Header("Animation Progress")]
        public bool _IsJumped; // this is only for jump animation not a key press
    }
}


