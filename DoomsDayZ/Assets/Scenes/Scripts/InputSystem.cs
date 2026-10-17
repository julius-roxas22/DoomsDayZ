using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameManagersSystem;

namespace IndieGameKeyInputSystem
{
    public class InputSystem : MonoBehaviour
    {
        private void Update()
        {
            VirtualInputManager.GetInstance._MoveForward = Input.GetKey(KeyCode.W);
            VirtualInputManager.GetInstance._MoveBackward = Input.GetKey(KeyCode.S);
            VirtualInputManager.GetInstance._MoveLeft = Input.GetKey(KeyCode.A);
            VirtualInputManager.GetInstance._MoveRight = Input.GetKey(KeyCode.D);
            VirtualInputManager.GetInstance._MoveForwardLeft = Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A);
            VirtualInputManager.GetInstance._MoveForwardRight = Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D);
            VirtualInputManager.GetInstance._MoveBackwardLeft = Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A);
            VirtualInputManager.GetInstance._MoveBackwardRight = Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D);
            VirtualInputManager.GetInstance._Jump = Input.GetKey(KeyCode.Space);
            VirtualInputManager.GetInstance._Attack = Input.GetKey(KeyCode.Return);
            VirtualInputManager.GetInstance._IsGrabbingWeapon = Input.GetKeyDown(KeyCode.G);
            VirtualInputManager.GetInstance._IsDroppingWeapon = Input.GetKeyDown(KeyCode.F);
            VirtualInputManager.GetInstance._IsPressedKeyR = Input.GetKey(KeyCode.R);

            //UI Controller Key
            VirtualInputManager.GetInstance._IsPressedESC = Input.GetKeyDown(KeyCode.Escape);
        }
    }
}

