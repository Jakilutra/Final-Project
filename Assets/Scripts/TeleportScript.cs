using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    // Declare variables.

    public Camera mainCamera;
    private GameObject player;
    private PlayerScript playerScript;
    private AttackScript attackScript;
    [SerializeField] private GameObject bigGreenEnemy;
    private bool hasSpawned = false;

    // Assign variables.

    void Start ()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();
        attackScript = player.GetComponent<AttackScript>();
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
                bool greenCheck = playerScript.activeColor == playerScript.colorGreen && teleporter.layer == LayerMask.NameToLayer("Green Teleporter") && playerScript.hasAbility["GreenTeleport"];
                bool redCheck = playerScript.activeColor == playerScript.colorRed && teleporter.layer == LayerMask.NameToLayer("Red Teleporter") && playerScript.hasAbility["RedTeleport"];
                bool blueCheck = playerScript.activeColor == playerScript.colorBlue && teleporter.layer == LayerMask.NameToLayer("Blue Teleporter") && playerScript.hasAbility["BlueTeleport"];
                if (whiteCheck || greenCheck || redCheck || blueCheck)
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
                // Check to see if the bullet or the player is being teleported.

                if (Input.GetKey(KeyCode.Space))
                {
                    attackScript.Fire("Player", playerScript.runSpeed * 5, playerScript.activeColor, teleporterLocation - transform.position, Quaternion.identity, 0.3f, 1);
                    return;
                }
                else
                {
                    transform.position = teleporterLocation;
                }
                spriteRenderer1.enabled = false;
                spriteRenderer2.enabled = true;
                if (teleporterLocation == new Vector3 (-16,0,0) && !hasSpawned)
                {
                    GameObject enemyClone = Instantiate(bigGreenEnemy, new Vector3(-16,10,0), Quaternion.identity);
                    enemyClone.transform.localScale *= 3;
                    hasSpawned = true;
                }
            }
            else
            {
                StartCoroutine(playerScript.Flicker(player));
            }
        }
    }
}
