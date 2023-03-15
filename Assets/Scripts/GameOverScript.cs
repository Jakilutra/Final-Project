using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    private void OnCollisionEnter (Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Obstacle") // If collides with a object having OBSTACLE tag.
        {
            FindObjectOfType<GameManager>().GameOver(); // Call GameOver function in GameManager
            FindObjectOfType<PlayerScript>().enabled = false; // Disable Player Script when Game Over.
            FindObjectOfType<EnemyScript>().enabled = false; // Disable Player Script when Game Over.
        }
    }
}
