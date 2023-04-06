using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{

    // Declaring variables.

    private SpriteRenderer render;
    private BoxCollider2D wallCollider;
    private GameObject player;
    private PolygonCollider2D playerCollider;

    // Assigning variables.

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        playerCollider = player.GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        // Player touching wall and changing colour check.

        BoxCollider2D wallCollider;
        wallCollider = GetComponent<BoxCollider2D>();
        if (playerCollider.IsTouching(wallCollider))
        {
            bool whiteCheck = player.layer == LayerMask.NameToLayer("IgnorePlayer") && gameObject.layer == LayerMask.NameToLayer("White Wall");
            bool greenCheck = player.layer == LayerMask.NameToLayer("Player") && gameObject.layer == LayerMask.NameToLayer("Green Wall");
            if (whiteCheck || greenCheck)
            {
                render.enabled = false;
                playerCollider.isTrigger = true;
            }
            else
            {
                playerCollider.isTrigger = false;
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
            render.enabled = true;
            if (obj.CompareTag("Bullet"))
            {
                obj.GetComponent<BoxCollider2D>().isTrigger = triggerState;
            }
            else
            {
                obj.GetComponent<PolygonCollider2D>().isTrigger = triggerState;
            }
        }
    }
}
