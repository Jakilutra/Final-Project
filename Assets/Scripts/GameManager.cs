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

    private GameObject player, whiteTeleporter1, whiteTeleporter2;
    private PlayerScript playerScript;
    private float restartTime;

    void Start()
    {
        // findOthers[0] = GameObject.Find(""); // [adding to findOthers array]
        player = GameObject.Find("Player");
        whiteTeleporter1 = GameObject.Find("Teleporter 3");
        whiteTeleporter2 = GameObject.Find("Teleporter 4");
        playerScript = player.GetComponent<PlayerScript>();
        restartTime = Time.time;
    }

    // Restart button

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha2) && Time.time < (restartTime + 1f))
        {
            player.transform.position = new Vector2(-20f, 21f);
            playerScript.colorChange = new Dictionary<Color, Color>
            {
                { playerScript.colorWhite, playerScript.colorGreen },
                { playerScript.colorGreen, playerScript.colorWhite },
            };
            playerScript.hasAbility["GreenWall"] = true;
            playerScript.hasAbility["GreenTeleport"] = true;
            playerScript.greenTeleporter1.SetActive(true);
            playerScript.greenTeleporter2.SetActive(true);
            playerScript.greenTeleporter1.GetComponent<SpriteRenderer>().enabled = true;
            playerScript.greenTeleporter2.GetComponent<SpriteRenderer>().enabled = false;
            whiteTeleporter1.GetComponent<SpriteRenderer>().enabled = true;
            whiteTeleporter2.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.P))
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