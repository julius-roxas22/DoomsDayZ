using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;

namespace IndieGameManagersSystem
{
    public enum _ECameraNormalParameters
    {
        Default,
        Shake
    }

    public enum _ECameraCharacterSelectParameters
    {
        Default,
        Lewis,
        Sophie,
        Pete
    }

    public class CameraManager : Singleton<CameraManager>
    {
        private Camera cam;
        private Animator cameraAnimator;

        private void Awake()
        {
            cam = FindAnyObjectByType<Camera>();
            cameraAnimator = cam.GetComponent<Animator>();
        }

        public void _TriggerShakeCamera(float duration)
        {
            StartCoroutine(shakeCamera(duration));
        }

        public Camera _Cam
        {
            get
            {
                return cam;
            }
        }

        private IEnumerator shakeCamera(float duration)
        {
            cameraAnimator.SetTrigger(_ECameraNormalParameters.Shake.ToString());
            yield return new WaitForSeconds(duration);
            cameraAnimator.SetTrigger(_ECameraNormalParameters.Default.ToString());
        }

        public void _SelectedCharacters(_ECameraCharacterSelectParameters selectedCharacters)
        {
            cameraAnimator.SetBool(selectedCharacters.ToString(), true);
        }
    }
}

