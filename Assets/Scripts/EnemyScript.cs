using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Declare variables (movement, damage counter and collectible spawn).

    private GameObject Player;
    private Color enemyColor;
    public float calcColor, runSpeed;
    private Dictionary<float, float> enemyID = new Dictionary<float, float>();
    private float distance;
    private PlayerScript playerScript;
    private int deathCounter;
    private float deathPoint;
    private Vector3 spawnPosition;
    [SerializeField] private GameObject greenTeleport;

    // Assign variables (movement and damage counter).

    void Start()
    {
        Player = GameObject.Find("Player");
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
        deathCounter = 0;
        deathPoint = Mathf.Pow(transform.localScale.x, 3f);
    }

    // Enemy movement.

    void Update()
    {
        if (playerScript.activeColor != playerScript.colorWhite)
        {
            distance = Vector2.Distance(transform.position, Player.transform.position);
            Vector2 direction = Player.transform.position - transform.position;
            if (deathCounter > 20)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, -runSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, runSpeed * Time.deltaTime);
            }
        }
    }

    // Collision enter/exit events (enemy damage & game over).

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Game over event.

        if (collision.gameObject.CompareTag("Player") && playerScript.activeColor != playerScript.colorWhite)
        {
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().GameOver();
        }
        EnemyDamage(collision.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Game over event.

        if (other.gameObject.CompareTag("Player") && playerScript.activeColor != playerScript.colorWhite)
        {
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().GameOver();
        }
        EnemyDamage(other.gameObject);
    }

    // Method to handle enemy damage.

    void EnemyDamage (GameObject obj)
    {
        if (obj.CompareTag("Bullet") && obj.layer == LayerMask.NameToLayer("Player Bullet"))
        {
            deathCounter++;
            spawnPosition = transform.position;
            if (obj != null)
            {
                StartCoroutine(playerScript.Flicker(gameObject));
            }
            if (deathCounter == deathPoint)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
                if (obj != null)
                {
                    Destroy(gameObject);
                }
                if (greenTeleport != null)
                {
                    Instantiate(greenTeleport, spawnPosition, Quaternion.identity);
                }
            }
        }

    }
}