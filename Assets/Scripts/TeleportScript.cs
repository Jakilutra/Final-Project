using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{

    public Camera mainCamera;

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject[] teleporters = GameObject.FindGameObjectsWithTag("Teleporter");
            foreach (GameObject teleporter in teleporters)
            {
                Vector3 targetPosition = mainCamera.WorldToViewportPoint(teleporter.transform.position);

                if (targetPosition.x >= 0 && targetPosition.x <= 1 && targetPosition.y >= 0 && targetPosition.y <= 1)
                {
                    Vector3 newPosition = teleporter.transform.position;
                    newPosition.z = transform.position.z;

                    transform.position = newPosition;

                    break;
                }
            }
        }
    }
}
