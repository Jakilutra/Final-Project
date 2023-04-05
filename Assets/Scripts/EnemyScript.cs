using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Declare Enemy Physics/Movement Variables

    [SerializeField] private GameObject Player;
    private Color enemyColor;
    private float calcColor;
    private float runSpeed;
    private Dictionary<float, float> enemyID = new Dictionary<float, float>();
    private float distance;
    private PlayerScript playerScript;
    private int deathCounter;
    private float deathPoint;
    private Vector3 spawnPosition;
    [SerializeField] private GameObject greenTeleport;

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
        deathCounter = 0;
        deathPoint = Mathf.Pow(transform.localScale.x, 3f);
    }

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerScript.activeColor != playerScript.colorWhite)
        {
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().GameOver();
        }
        EnemyDamage(collision.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && playerScript.activeColor != playerScript.colorWhite)
        {
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().GameOver();
        }
        EnemyDamage(other.gameObject);
    }

    void EnemyDamage (GameObject obj)
    {
        if (obj.CompareTag("Bullet"))
        {
            deathCounter++;
            spawnPosition = transform.position;
            if (obj != null)
            {
                StartCoroutine(playerScript.TogglePlayerVisibility(gameObject));
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