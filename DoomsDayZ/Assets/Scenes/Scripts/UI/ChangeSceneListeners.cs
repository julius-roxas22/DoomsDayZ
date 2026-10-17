using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGameGameControllerSystem
{
    public class ChangeSceneListeners : MonoBehaviour
    {
        [SerializeField] private string sceneName;

        public void _ChangeScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }

        public void _QuitGame()
        {
            Application.Quit();
            Debug.Log("Quit Game is Pressed..");
        }
    }
}

