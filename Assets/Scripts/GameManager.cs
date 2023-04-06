using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 public class GameManager : MonoBehaviour

    // Declaring Variables
{
    bool gamehasEnded = false;

    public GameObject gameOver;
    private GameObject[] findRest = new GameObject[1];

    void Start()
    {
        findRest[0] = GameObject.Find("Floor");
    }

    // Restart button

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
          Restart();
        }
    }

    // Deactivates Game Objects apart from the Game Over one.

    public void GameOver()
    {
        if (!gamehasEnded)
        {
            gamehasEnded = true;
            gameOver.SetActive(true);
            Deactivate("Collectible");
            Deactivate("Enemy");
            Deactivate("Message");
            Deactivate("Wall");
            Deactivate("Others");

        }
    }

    // Restarts the game.

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Function that loops through arrays to find game objects to deactivate.

    void Deactivate(string tagName)
    {
        GameObject[] gameObjects = tagName != "Others" ? GameObject.FindGameObjectsWithTag(tagName) : findRest;
        {
            foreach (GameObject i in gameObjects)
            {
                if (i != null)
                {
                    i.SetActive(false);
                }
            }
        }
    }
}