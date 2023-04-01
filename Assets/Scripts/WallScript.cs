using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{

    private SpriteRenderer render;
    private BoxCollider2D wallCollider;
    private GameObject player;
    private PolygonCollider2D playerCollider;
    private PlayerScript playerScript;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        wallCollider = GetComponent<BoxCollider2D>();
        player = GameObject.FindWithTag("Player");
        playerCollider = player.GetComponent<PolygonCollider2D>();

    }

    void Update()
    {
        if (playerCollider.IsTouching(wallCollider) && player.layer == LayerMask.NameToLayer("IgnorePlayer"))
        {
            if (gameObject.layer == LayerMask.NameToLayer("White Wall"))
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.layer == LayerMask.NameToLayer("White Wall") && collision.gameObject.layer == LayerMask.NameToLayer("IgnorePlayer"))
        {
            render.enabled = false;
            playerCollider.isTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.layer == LayerMask.NameToLayer("White Wall") && other.gameObject.layer == LayerMask.NameToLayer("IgnorePlayer"))
        {
            render.enabled = true;
            playerCollider.isTrigger = false;
        }
    }
}
