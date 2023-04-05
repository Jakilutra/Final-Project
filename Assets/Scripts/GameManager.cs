using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 public class GameManager : MonoBehaviour
{
    bool gamehasEnded = false;

    public GameObject[] collectibles;
    public GameObject[] enemies;
    public GameObject[] messages;
    public GameObject[] walls;
    public GameObject Floor;
    public GameObject GameOverPanel;
    public GameObject Player;

    void Start()
    {
    }

    public void GameOver()
    {
        if (!gamehasEnded)
        {
            gamehasEnded = true;
            collectibles = GameObject.FindGameObjectsWithTag("Collectible");
            foreach (GameObject collectible in collectibles)
            {
                if (collectible != null)
                {
                    collectible.SetActive(false);
                }
            }
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.SetActive(false);
                }
            }
            messages = GameObject.FindGameObjectsWithTag("Message");
            foreach (GameObject message in messages)
            {
                if (message != null)
                {
                   message.SetActive(false);
                }
            }
            walls = GameObject.FindGameObjectsWithTag("Wall");
            foreach (GameObject wall in walls)
            {
                if (wall != null)
                {
                    wall.SetActive(false);
                }
            }
            Floor.SetActive(false);
            GameOverPanel.SetActive(true);

        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
          Restart();
        }
    }
}