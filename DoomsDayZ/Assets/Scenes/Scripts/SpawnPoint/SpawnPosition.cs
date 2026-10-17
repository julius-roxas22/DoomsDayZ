using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;
using Cinemachine;

namespace IndieGameGameControllerSystem
{
    //TODO: need the specific position to spawn;
    public class SpawnPosition : MonoBehaviour
    {
        private GameObject characterSpawnObj;

        private void Awake()
        {
            if (null == characterSpawnObj)
            {
                GameController gameController = GameManager.GetInstance._GetGameController();
                GameControllerData gameControllerData = gameController._GameControllerData;

                characterSpawnObj = Instantiate(Resources.Load<GameObject>(gameControllerData._CharacterName), transform.position, Quaternion.identity);

                CinemachineVirtualCamera[] cameras = FindObjectsOfType<CinemachineVirtualCamera>(); //assign the camera to follow into character spawner

                Character character = characterSpawnObj.GetComponent<Character>();
                foreach (CinemachineVirtualCamera camera in cameras)
                {
                    Transform bodyPart = character._BodyPartTransform(character._HealthSystem._BodyPartCameraToFollow);
                    camera.LookAt = bodyPart;
                    camera.Follow = bodyPart;
                }

                gameController._Character = character;
            }
        }
    }
}