using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 public class GameManager : MonoBehaviour
{
    bool gamehasEnded = false;

    public GameObject[] enemies;
    public GameObject[] walls;
    public GameObject Floor;
    public GameObject GameOverPanel;
    public GameObject Player;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        walls = GameObject.FindGameObjectsWithTag("Wall");
    }

    public void GameOver()
    {
        if (!gamehasEnded)
        {
            gamehasEnded = true;

            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(false);
            }
            foreach (GameObject wall in walls)
            {
                wall.SetActive(false);
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