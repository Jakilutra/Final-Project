using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Declare Enemy Physics/Movement Variables

    public GameObject Player;
    private Color enemyColor;
    private float calcColor;
    private float runSpeed;
    private Dictionary<float, float> enemyID = new Dictionary<float, float>();
    private float distance;
    private PlayerScript playerScript;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerScript.activeColor != playerScript.colorWhite)
        {
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    void Start()
    {
        playerScript = FindObjectOfType<PlayerScript>();
        enemyColor = gameObject.GetComponent<SpriteRenderer>().color;
        calcColor = 3 * Mathf.Floor(enemyColor.r) + 2 * Mathf.Floor(enemyColor.g) + 1 * Mathf.Floor(enemyColor.b);
        enemyID = new Dictionary<float, float>
        {
            { 3, 3f },
            { 2, 2f },
            { 1, 4f },
        };
        enemyID.TryGetValue(calcColor, out runSpeed);
    }

    void Update()
    {
        if (playerScript.activeColor != playerScript.colorWhite)
        {
            distance = Vector2.Distance(transform.position, Player.transform.position);
            Vector2 direction = Player.transform.position - transform.position;

            transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, runSpeed * Time.deltaTime);
        }
    }
}