using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    // Declare variables.

    public Camera mainCamera;
    private GameObject player;
    private PlayerScript playerScript;
    [SerializeField] private GameObject bigGreenEnemy;

    // Assign variables.

    void Start ()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();
    }

    void Update ()
    {

        // Upon pressing down T, finds the nearest visible teleport point, and teleports to it. After, it disables the forward teleport circle and reveals the backward one.

        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject[] teleporters = GameObject.FindGameObjectsWithTag("Teleporter");
            SpriteRenderer spriteRenderer1 = null;
            SpriteRenderer spriteRenderer2 = null;
            Vector3 teleporterLocation = new Vector3(0,0,0);

            foreach (GameObject teleporter in teleporters)
            {
                bool whiteCheck = player.layer == LayerMask.NameToLayer("IgnorePlayer") && teleporter.layer == LayerMask.NameToLayer("White Teleporter");
                bool greenCheck = player.layer == LayerMask.NameToLayer("Player") && teleporter.layer == LayerMask.NameToLayer("Green Teleporter") && playerScript.hasAbility["GreenTeleport"];
                if (whiteCheck || greenCheck)
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
            }
            if (spriteRenderer1 != null && spriteRenderer2 != null && !teleporterLocation.Equals(default))
            {
                transform.position = teleporterLocation;
                spriteRenderer1.enabled = false;
                spriteRenderer2.enabled = true;
                if (teleporterLocation == new Vector3 (-16,0,0))
                {
                    GameObject enemyClone = Instantiate(bigGreenEnemy, new Vector3(-16,10,0), Quaternion.identity);
                    enemyClone.transform.localScale *= 3;
                }
            }
            else
            {
                StartCoroutine(playerScript.Flicker(player));
            }
        }
    }
}
