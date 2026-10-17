using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControllerScrch1 : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject characterObj;
    [SerializeField] private float rotateDeltaSpeed;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 playerForward = hit.point - characterObj.transform.position;
            playerForward.y = 0f;

            if (playerForward.sqrMagnitude > 1.2f && playerForward.sqrMagnitude < 30f)
            {
                //characterObj.transform.rotation = Quaternion.LookRotation(playerForward);
                Quaternion lookRotation = Quaternion.LookRotation(playerForward);
                characterObj.transform.rotation = Quaternion.RotateTowards(characterObj.transform.rotation, lookRotation, rotateDeltaSpeed * Time.deltaTime);
            }
        }
    }
}
