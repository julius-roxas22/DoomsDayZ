using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameManagersSystem;

namespace IndieGameGameControllerSystem
{
    public class MainUIController : MonoBehaviour
    {
        [SerializeField] private GameObject backToMenuPanel;
        [SerializeField] private bool isShowedPanel;

        private void Awake()
        {
            backToMenuPanel.SetActive(false);
        }

        void Update()
        {
            if (VirtualInputManager.GetInstance._IsPressedESC)
            {
                if (!isShowedPanel)
                {
                    isShowedPanel = true;
                }
                else
                {
                    isShowedPanel = false;
                }
                backToMenuPanel.SetActive(isShowedPanel);
            }
        }
    }
}
