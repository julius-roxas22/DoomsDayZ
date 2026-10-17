using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameCharacterControllerSystem;
using IndieGameManagersSystem;

namespace IndieGameCameraSystem
{
    [System.Obsolete("This code is Deprecated!", true)]
    public class CameraMain : MonoBehaviour
    {
        [SerializeField] private Character character;
        [SerializeField] private GameObject obstacleDetected;
        private void Update()
        {

            //TODO: enemy and character have different material when its not visible in the game

            setUp();

            Vector3 direction = character.transform.position - transform.position;
            Ray ray = new Ray(transform.position, direction.normalized);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (character._CharacterRagdoll._RagdollParts.Contains(hit.collider))
                {
                    return;
                }

                GameObject obj = hit.collider.gameObject;

                if (character.gameObject != obj)
                {
                    Renderer rend = obj.GetComponent<Renderer>();

                    if (null == rend)
                    {
                        return;
                    }

                    Color c = rend.material.color;
                    c.a = 0.65f;
                    rend.material.color = c;
                    obstacleDetected = obj;
                }
                else
                {
                    if (null == obstacleDetected)
                    {
                        return;
                    }

                    Renderer rend = obstacleDetected.GetComponent<Renderer>();

                    if (null == rend)
                    {
                        return;
                    }

                    Color c = rend.material.color;
                    c.a = 1f;
                    rend.material.color = c;
                    obstacleDetected = null;
                }
            }
        }

        private void setUp()
        {
            if (null == character)
            {
                foreach (Character c in CharacterManager.GetInstance._Characters)
                {
                    if (c._SubController.enabled)
                    {
                        character = c;
                    }
                }
            }
        }
    }
}
