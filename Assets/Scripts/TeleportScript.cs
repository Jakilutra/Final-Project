using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{

    public Camera mainCamera;
    private GameObject player;

    void Update ()
    {

        // Upon pressing down T, finds the nearest visible teleport point, and teleports to it. After, it disables the forward teleport circle and reveals the backward one.

        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject[] teleporters = GameObject.FindGameObjectsWithTag("Teleporter");
            SpriteRenderer spriteRenderer1 = null;
            SpriteRenderer spriteRenderer2 = null;
            Vector3 teleporterLocation = new Vector3(0,0,0);
            GameObject player = GameObject.FindWithTag("Player");

            foreach (GameObject teleporter in teleporters)
            {
                Vector3 targetPosition = mainCamera.WorldToViewportPoint(teleporter.transform.position);

                if (targetPosition.x >= 0 && targetPosition.x <= 1 && targetPosition.y >= 0 && targetPosition.y <= 1)
                {

                    SpriteRenderer spriteRenderer = teleporter.GetComponent<SpriteRenderer>();

                    if (spriteRenderer.enabled)
                    {
                        teleporterLocation = teleporter.transform.position;
                        spriteRenderer1 = spriteRenderer;
                    }
                    else
                    {
                        spriteRenderer2 = spriteRenderer;
                    }
                }
            }
            if (spriteRenderer1 != null && spriteRenderer2 != null && !teleporterLocation.Equals(default))
            {
                transform.position = teleporterLocation;
                spriteRenderer1.enabled = false;
                spriteRenderer2.enabled = true;
            }
            else
            {
                StartCoroutine(TogglePlayerVisibility(player));
            }
        }
    }

    private IEnumerator TogglePlayerVisibility(GameObject player)
    {
        SpriteRenderer render = player.GetComponent<SpriteRenderer>();
        render.enabled = false;
        yield return new WaitForSeconds(0.1f);
        render.enabled = true;
    }
}
