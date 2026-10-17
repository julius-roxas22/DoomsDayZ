using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;
using IndieGameHealthSystem;

namespace IndieGameGameControllerSystem
{

    [System.Obsolete("This Code is Deprecated!", true)]
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private GameObject quitPanel;
        public Slider _PlayerHealthBar;
        public Character _CharacterSpawned;
        private HealthSystem healthSystem;
        private bool isEnablePanel;

        private void Awake()
        {
            if (null != quitPanel)
            {
                quitPanel.SetActive(false);
            }
        }

        private void Start()
        {
            _PlayerHealthBar.maxValue = _CharacterSpawned._HealthSystem._DefenseData._MaximumHealth;
            _PlayerHealthBar.value = _CharacterSpawned._HealthSystem._DefenseData._CurrentHealth;
        }

        private void Update()
        {
            if (null == healthSystem)
            {
                foreach (Character c in CharacterManager.GetInstance._Characters)
                {
                    if (c._SubController.enabled)
                    {
                        healthSystem = c.GetComponent<HealthSystem>();
                        break;
                    }
                }
            }

            //geting error due to no player aactive
            //if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainMenu")
            //{
            //    if (null != healthSystem)
            //    {
            //        if (!isEnablePanel && healthSystem._CurrentHealth < 0f)
            //        {
            //            Debug.Log("Enable Quit Panel");
            //            quitPanel.SetActive(true);
            //            isEnablePanel = true;
            //        }
            //    }
            //}
        }

        public void UpdateHealthBar()
        {
            _PlayerHealthBar.value = _CharacterSpawned._HealthSystem._DefenseData._CurrentHealth;
        }
    }
}

