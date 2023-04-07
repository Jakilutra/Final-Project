using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 public class GameManager : MonoBehaviour

    // Declaring variables.
{
    bool gamehasEnded = false;

    public GameObject gameOver;
    private GameObject[] findRest = new GameObject[0];

    void Start()
    {
        // findRest[0] = GameObject.Find(""); // [adding to findRest array]
    }

    // Restart button

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
          Restart();
        }
    }

    // Deactivates game objects apart from the game over one.

    public void GameOver()
    {
        if (!gamehasEnded)
        {
            gamehasEnded = true;
            gameOver.SetActive(true);
            Deactivate("Collectible");
            Deactivate("Enemy");
            Deactivate("Floor");
            Deactivate("Message");
            Deactivate("Teleporter");
            Deactivate("Wall");
            // Deactivate("Others"); // [deactivating single game objects]

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