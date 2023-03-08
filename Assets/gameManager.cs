using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    bool gamehasEnded = true;

    public GameObject GameOverPanel;

    public void GameOver()
    {
        if (gamehasEnded == true)
        {
            gamehasEnded = true;
            GameOverPanel.SetActive(true);
            Restart();

            void Restart()
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
