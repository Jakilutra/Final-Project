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
    private GameObject otherObject;
    private Color otherColor;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        wallCollider = GetComponent<BoxCollider2D>();
        player = GameObject.FindWithTag("Player");
        playerCollider = player.GetComponent<PolygonCollider2D>();
        playerScript = player.GetComponent<PlayerScript>();

    }

    void Update()
    {
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.layer == LayerMask.NameToLayer("White Wall") && collision.gameObject.layer == LayerMask.NameToLayer("IgnorePlayer"))
        {
            render.enabled = false;
            playerCollider.isTrigger = true;
        }
        otherObject = collision.gameObject;
        otherColor = otherObject.GetComponent<SpriteRenderer>().color;
        if (gameObject.layer == LayerMask.NameToLayer("Green Wall") && otherColor.g == playerScript.colorGreen.g && otherColor.r != 1)
        {
            render.enabled = false;
            if (otherObject.tag == "Bullet")
            {
                otherObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            }
            else
            {
                otherObject.GetComponent<PolygonCollider2D>().isTrigger = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.layer == LayerMask.NameToLayer("White Wall") && other.gameObject.layer == LayerMask.NameToLayer("IgnorePlayer"))
        {
            render.enabled = true;
            playerCollider.isTrigger = false;
        }
        otherObject = other.gameObject;
        otherColor = otherObject.GetComponent<SpriteRenderer>().color;
        if (gameObject.layer == LayerMask.NameToLayer("Green Wall") && otherColor.g == playerScript.colorGreen.g && otherColor.r != 1)
        {
            render.enabled = true;
            if (otherObject.tag == "Bullet")
            {
                otherObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
            }
            else
            {
                otherObject.GetComponent<PolygonCollider2D>().isTrigger = false;
            }
        }
    }
}
