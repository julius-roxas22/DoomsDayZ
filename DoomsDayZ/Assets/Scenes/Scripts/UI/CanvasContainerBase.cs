using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameManagersSystem;
using IndieGameCharacterControllerSystem;

namespace IndieGameUISystem
{
    public enum _ECanvasContainerType
    {
        None,
        CharacterCanvas,
        WeaponCanvas,
    }

    public abstract class CanvasContainerBase : MonoBehaviour
    {
        public _ECanvasContainerType _CanvasContainerType;

        protected virtual void Awake()
        {
            if (null != GameManager.GetInstance._GetGameUI()) // check if there is a game ui object in the scene, to prevent error when start game
            {
                GameManager.GetInstance._GetGameUI()._AddDicCanvasContainer(this);
            }
            //Time.timeScale = 0.2f;
        }

        protected virtual void Update()
        {
            Vector3 direction = CameraManager.GetInstance._Cam.transform.position - transform.position;
            direction.x = 0f;
            transform.rotation = Quaternion.LookRotation(-direction);
        }
    }
}