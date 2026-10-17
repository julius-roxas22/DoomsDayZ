using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameManagersSystem
{
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
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
        public bool _IsPressedESC;
    }
}
