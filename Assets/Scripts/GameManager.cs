using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 public class GameManager : MonoBehaviour
{
    bool gamehasEnded = false;

    public GameObject Enemy;
    public GameObject Floor;
    public GameObject GameOverPanel;
    public GameObject Player;

    public void GameOver()
    {
        if (!gamehasEnded)
        {
            gamehasEnded = true;

            Enemy.SetActive(false);
            Floor.SetActive(false);
            GameOverPanel.SetActive(true);
            Player.SetActive(false);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
          Restart();
        }
    }
}