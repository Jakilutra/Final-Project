using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{

    // Declaring variables.

    private SpriteRenderer render;
    private GameObject player;
    private PlayerScript playerScript;
    private PolygonCollider2D playerCollider;

    // Assigning variables.

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        playerCollider = player.GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        // Player touching wall and changing colour check.

        BoxCollider2D wallCollider = GetComponent<BoxCollider2D>();
        Rigidbody2D playerRigidbody = playerCollider.GetComponent<Rigidbody2D>();

        if (playerCollider.IsTouching(wallCollider))
        {
            bool whiteCheck = player.layer == LayerMask.NameToLayer("IgnorePlayer") && gameObject.layer == LayerMask.NameToLayer("White Wall");
            bool greenCheck = playerScript.activeColor == playerScript.colorGreen && gameObject.layer == LayerMask.NameToLayer("Green Wall");
            bool redCheck = playerScript.activeColor == playerScript.colorRed && gameObject.layer == LayerMask.NameToLayer("Red Wall");
            bool blueCheck = playerScript.activeColor == playerScript.colorBlue && gameObject.layer == LayerMask.NameToLayer("Blue Wall");

            if (whiteCheck || greenCheck || redCheck || blueCheck)
            {
                render.enabled = false;
                playerCollider.isTrigger = true;
            }
            // Turn Trigger off if player is moving diagonally and not touching a coloured wall.

            else if (playerRigidbody.velocity.x != 0f && playerRigidbody.velocity.y != 0f)
            {
                playerCollider.isTrigger = false;
            }
        }

        // Turn Trigger off if enemy  is moving diagonally and not touching a coloured wall.

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                PolygonCollider2D enemyCollider = enemy.GetComponent<PolygonCollider2D>();
                Rigidbody2D enemyRigidbody = enemyCollider.GetComponent<Rigidbody2D>();
                if (enemyRigidbody.velocity.x != 0f && enemyRigidbody.velocity.y != 0f && gameObject.layer != LayerMask.NameToLayer("Green Wall") && gameObject.layer != LayerMask.NameToLayer("Red Wall") && gameObject.layer != LayerMask.NameToLayer("Blue Wall"))
                {
                    enemyCollider.isTrigger = false;
                }
            }
        }

    }

    // Collision events.

    void OnCollisionEnter2D(Collision2D collision)
    {
        ColorCollision(collision.gameObject, true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ColorCollision(other.gameObject, false);
    }

    // Method that determines whether the object of a colour collides with a wall of another colour (same colours don't collide).

    void ColorCollision(GameObject obj, bool triggerState)
    {
        if (gameObject.layer == LayerMask.NameToLayer("White Wall") && obj.layer == LayerMask.NameToLayer("IgnorePlayer"))
        {
            render.enabled = !triggerState;
            playerCollider.isTrigger = triggerState;
        }
        Color objColor = obj.GetComponent<SpriteRenderer>().color;
        if (gameObject.layer == LayerMask.NameToLayer("Green Wall") && objColor.g == 1 && objColor.r != 1)
        {
            render.enabled = !triggerState;
            if (obj.CompareTag("Bullet"))
            {
                obj.GetComponent<BoxCollider2D>().isTrigger = triggerState;
            }
            else
            {
                obj.GetComponent<PolygonCollider2D>().isTrigger = triggerState;
            }
        }
        if (gameObject.layer == LayerMask.NameToLayer("Red Wall") && objColor.g != 1 && objColor.r == 1)
        {
            render.enabled = !triggerState;
            if (obj.CompareTag("Bullet"))
            {
                obj.GetComponent<BoxCollider2D>().isTrigger = triggerState;
            }
            else
            {
                obj.GetComponent<PolygonCollider2D>().isTrigger = triggerState;
            }
        }
        if (gameObject.layer == LayerMask.NameToLayer("Blue Wall") && objColor.b == 1 && objColor.r != 1)
        {
            render.enabled = !triggerState;
            if (obj.CompareTag("Bullet"))
            {
                obj.GetComponent<BoxCollider2D>().isTrigger = triggerState;
            }
            else
            {
                obj.GetComponent<PolygonCollider2D>().isTrigger = triggerState;
            }
        }
        if (gameObject.activeSelf)
        {
            StartCoroutine(ReturnVisibility(obj));
        }
    }

    IEnumerator ReturnVisibility(GameObject obj)
    {
        yield return new WaitForSeconds(0.6f);
        if (obj == null)
        {
            render.enabled = true;
        }
    }

}
