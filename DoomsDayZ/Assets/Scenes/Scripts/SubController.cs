using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameManagersSystem;

namespace IndieGameCharacterControllerSystem
{
    //associated with controller
    //this is the motor of the character to move
    public class SubController : MonoBehaviour
    {
        private Controller controller;

        private void Awake()
        {
            controller = GetComponent<Controller>();
        }

        void Update()
        {
            controller._MoveForward = VirtualInputManager.GetInstance._MoveForward;
            controller._MoveBackward = VirtualInputManager.GetInstance._MoveBackward;
            controller._MoveLeft = VirtualInputManager.GetInstance._MoveLeft;
            controller._MoveRight = VirtualInputManager.GetInstance._MoveRight;
            controller._MoveForwardLeft = VirtualInputManager.GetInstance._MoveForwardLeft;
            controller._MoveForwardRight = VirtualInputManager.GetInstance._MoveForwardRight;
            controller._MoveBackwardLeft = VirtualInputManager.GetInstance._MoveBackwardLeft;
            controller._MoveBackwardRight = VirtualInputManager.GetInstance._MoveBackwardRight;
            controller._Jump = VirtualInputManager.GetInstance._Jump;
            controller._Attack = VirtualInputManager.GetInstance._Attack;
            controller._IsGrabbingWeapon = VirtualInputManager.GetInstance._IsGrabbingWeapon;
            controller._IsDroppingWeapon = VirtualInputManager.GetInstance._IsDroppingWeapon;
            controller._IsPressedKeyR = VirtualInputManager.GetInstance._IsPressedKeyR;
            controller._IsPressedKey1 = VirtualInputManager.GetInstance._IsPressedKey1;
            controller._IsPressedKey2 = VirtualInputManager.GetInstance._IsPressedKey2;
            controller._IsPressedKey3 = VirtualInputManager.GetInstance._IsPressedKey3;
            controller._IsPressedKey4 = VirtualInputManager.GetInstance._IsPressedKey4;
            controller._DisarmWeapon = VirtualInputManager.GetInstance._DisarmWeapon;
        }
    }
}


