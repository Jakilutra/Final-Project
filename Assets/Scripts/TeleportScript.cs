using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{

    public Camera mainCamera;

    void Update ()
    {

    // Upon pressing down T, finds the nearest visible teleport point, and teleports to it. After, it disables the forward teleport circle and reveals the backward one.

        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject[] teleporters = GameObject.FindGameObjectsWithTag("Teleporter");
            foreach (GameObject teleporter in teleporters)
            {
                Vector3 targetPosition = mainCamera.WorldToViewportPoint(teleporter.transform.position);

                if (targetPosition.x >= 0 && targetPosition.x <= 1 && targetPosition.y >= 0 && targetPosition.y <= 1)
                {

                    SpriteRenderer spriteRenderer = teleporter.GetComponent<SpriteRenderer>();
               
                    if (spriteRenderer.enabled)
                    {
                        transform.position = teleporter.transform.position;
                        spriteRenderer.enabled = false;
                    }
                    else
                    {
                        spriteRenderer.enabled = true;
                    }
                }
            }
        }
    }
}
