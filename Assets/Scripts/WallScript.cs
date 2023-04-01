using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{

    private SpriteRenderer render;
    private new BoxCollider2D collider;
    private GameObject player;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        player = GameObject.FindWithTag("Player");
    }

    void Update ()
    {
        if (player.GetComponent<PolygonCollider2D>().IsTouching(collider) && player.layer == LayerMask.NameToLayer("IgnorePlayer"))
        {
            render.enabled = false;
            player.GetComponent<PolygonCollider2D>().isTrigger = true;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("IgnorePlayer"))
        {
            render.enabled = false;
            collision.gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("IgnorePlayer"))
        {
            render.enabled = true;
            other.gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
        }
    }
}
