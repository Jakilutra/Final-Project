using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 public class GameManager : MonoBehaviour

    // Declaring variables.
{
    bool gamehasEnded = false;

    public GameObject gameOver, gameWin, finalBoss;
    public GameObject[] findOthers = new GameObject[0]; // [array to add the rest of the game objects to deactivate]

    private GameObject player, whiteTeleporter1, whiteTeleporter2, redTeleporter1, redTeleporter2, blueTeleporter1, blueTeleporter2;
    private PlayerScript playerScript;
    private float restartTime;

    void Start()
    {
        // findOthers[0] = GameObject.Find(""); // [adding to findOthers array]
        player = GameObject.Find("Player");
        whiteTeleporter1 = GameObject.Find("Teleporter 3");
        whiteTeleporter2 = GameObject.Find("Teleporter 4");
        redTeleporter1 = GameObject.Find("Teleporter 5");
        redTeleporter2 = GameObject.Find("Teleporter 6");
        blueTeleporter1 = GameObject.Find("Teleporter 7");
        blueTeleporter2 = GameObject.Find("Teleporter 8");
        playerScript = player.GetComponent<PlayerScript>();
        restartTime = Time.time;
    }

    // Restart button

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha2) && Time.time < (restartTime + 1f))
        {
            player.transform.position = new Vector2(-16f, 25f);
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
            playerScript.UpdateOverlay();
        }
        else if (Input.GetKey(KeyCode.Alpha3) && Time.time < (restartTime + 1f))
        {
            player.transform.position = new Vector2(15f, 52f);
            playerScript.colorChange = new Dictionary<Color, Color>
            {
                { playerScript.colorWhite, playerScript.colorGreen },
                { playerScript.colorGreen, playerScript.colorRed },
                { playerScript.colorRed, playerScript.colorWhite },
            };
            playerScript.hasAbility["GreenWall"] = true;
            playerScript.hasAbility["RedWall"] = true;
            playerScript.hasAbility["GreenTeleport"] = true;
            playerScript.hasAbility["RedTeleport"] = true;
            playerScript.greenTeleporter1.SetActive(true);
            playerScript.greenTeleporter2.SetActive(true);
            playerScript.greenTeleporter1.GetComponent<SpriteRenderer>().enabled = true;
            playerScript.greenTeleporter2.GetComponent<SpriteRenderer>().enabled = false;
            whiteTeleporter1.GetComponent<SpriteRenderer>().enabled = true;
            whiteTeleporter2.GetComponent<SpriteRenderer>().enabled = false;
            redTeleporter1.GetComponent<SpriteRenderer>().enabled = true;
            redTeleporter2.GetComponent<SpriteRenderer>().enabled = false;
            playerScript.UpdateOverlay();
        }
        else if (Input.GetKey(KeyCode.Alpha4) && Time.time < (restartTime + 1f))
        {
            player.transform.position = new Vector2(15.5f, 80f);
            playerScript.colorChange = new Dictionary<Color, Color>
            {
                { playerScript.colorWhite, playerScript.colorGreen },
                { playerScript.colorGreen, playerScript.colorRed },
                { playerScript.colorRed, playerScript.colorBlue },
                { playerScript.colorBlue, playerScript.colorWhite },
            };
            playerScript.hasAbility["GreenWall"] = true;
            playerScript.hasAbility["RedWall"] = true;
            playerScript.hasAbility["BlueWall"] = true;
            playerScript.hasAbility["GreenTeleport"] = true;
            playerScript.hasAbility["RedTeleport"] = true;
            playerScript.hasAbility["BlueTeleport"] = true;
            playerScript.greenTeleporter1.SetActive(true);
            playerScript.greenTeleporter2.SetActive(true);
            playerScript.greenTeleporter1.GetComponent<SpriteRenderer>().enabled = true;
            playerScript.greenTeleporter2.GetComponent<SpriteRenderer>().enabled = false;
            whiteTeleporter1.GetComponent<SpriteRenderer>().enabled = true;
            whiteTeleporter2.GetComponent<SpriteRenderer>().enabled = false;
            redTeleporter1.GetComponent<SpriteRenderer>().enabled = true;
            redTeleporter2.GetComponent<SpriteRenderer>().enabled = false;
            blueTeleporter1.GetComponent<SpriteRenderer>().enabled = false;
            blueTeleporter2.GetComponent<SpriteRenderer>().enabled = true;
            playerScript.UpdateOverlay();
            if (finalBoss != null)
            {
                finalBoss.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Restart();
        }
    }


    public void GameWin()
    {
        gamehasEnded = true;
        gameWin.SetActive(true);
        Deactivate("Collectible");
        Deactivate("Enemy");
        Deactivate("Floor");
        Deactivate("FakeWall");
        Deactivate("Message");
        Deactivate("Teleporter");
        Deactivate("Wall");
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
            Deactivate("FakeWall");
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

    public void Deactivate(string tagName)
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