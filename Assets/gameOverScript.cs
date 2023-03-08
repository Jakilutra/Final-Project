using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOverScript : MonoBehaviour
{
    private void OnCollisionEnter (Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Obstacle") // If collides with a object having OBSTACLE tag.
        {
            FindObjectOfType<gameManager>().GameOver(); // Call GameOver function in gameManager
            FindObjectOfType<playerScript>().enabled() = false();   // Disable PlayerMovement script when gameover.
        }
    }
}
