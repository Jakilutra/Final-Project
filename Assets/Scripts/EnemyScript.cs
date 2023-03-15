using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Declare Enemy Physics/Movement Variables

    public GameObject player;
    private float runSpeedGreen = 1.0f;
    private float distance;

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, runSpeedGreen * Time.deltaTime);
    }
}