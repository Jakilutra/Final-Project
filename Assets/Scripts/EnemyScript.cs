using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Declare Enemy Physics/Movement Variables

    public GameObject Player;
    private float runSpeedGreen = 2f;
    private float distance;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, Player.transform.position);
        Vector2 direction = Player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, runSpeedGreen * Time.deltaTime);
    }
}