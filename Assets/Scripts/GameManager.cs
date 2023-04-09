using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 public class GameManager : MonoBehaviour

    // Declaring variables.
{
    bool gamehasEnded = false;

    public GameObject gameOver;
    private GameObject[] findOthers = new GameObject[0]; // [array to add the rest of the game objects to deactivate]

    void Start()
    {
        // findOthers[0] = GameObject.Find(""); // [adding to findOthers array]
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
            // Deactivate("Others"); // [deactivating the rest of the game objects]

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
        GameObject[] gameObjects = tagName != "Others" ? GameObject.FindGameObjectsWithTag(tagName) : findOthers;
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