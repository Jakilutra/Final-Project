using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Declare variables (movement, damage counter and collectible spawn).

    private GameObject Player;
    private SpriteRenderer render;
    public Color enemyColor;
    public float calcColor, runSpeed;
    private Dictionary<float, float> enemyID = new Dictionary<float, float>();
    private float distance;
    private PlayerScript playerScript;
    private int deathCounter;
    private float deathPoint;
    private Vector3 spawnPosition;
    [SerializeField] private GameObject teleportAbility, health;

    // Assign variables (movement and damage counter).

    void Start()
    {
        Player = GameObject.Find("Player");
        playerScript = FindObjectOfType<PlayerScript>();
        render = gameObject.GetComponent<SpriteRenderer>();
        enemyColor = render.color;
        calcColor = 3 * Mathf.Floor(enemyColor.r) + 2 * Mathf.Floor(enemyColor.g) + 1 * Mathf.Floor(enemyColor.b);
        enemyID = new Dictionary<float, float>
        {
            { 3, 3f },
            { 2, 2f },
            { 1, 4f },
        };
        enemyID.TryGetValue(calcColor, out runSpeed);
        deathCounter = 0;
        deathPoint = Mathf.Pow(transform.localScale.x, 3f) * (runSpeed - 1);
    }

    // Enemy movement.

    void Update()
    {
        if (playerScript.activeColor != playerScript.colorWhite)
        {
            distance = Vector2.Distance(transform.position, Player.transform.position);
            Vector2 direction = Player.transform.position - transform.position;
            if (deathCounter > (20 *(runSpeed-1)))
            {
                transform.localScale = new Vector3(2, 2, 1);
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
        EnemyDamage(collision.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyDamage(other.gameObject);
    }

    // Method to handle enemy damage.

    void EnemyDamage (GameObject obj)
    {
        if (obj.CompareTag("Bullet") && obj.layer == LayerMask.NameToLayer("Player Bullet"))
        {
            deathCounter++;
            SpriteRenderer renderb = obj.GetComponent<SpriteRenderer>();
            Color playerBulletColor = renderb.color;
            if (playerBulletColor.r == 1)
            {
                deathCounter++;
            }
            if (playerBulletColor.b == 1)
            {
                deathCounter += 2;
            }
            spawnPosition = transform.position;

            if (obj != null && gameObject != null)
            {
                if (gameObject.activeSelf)
                {
                    StartCoroutine(playerScript.Flicker(gameObject));
                }
            }

            if (deathCounter >= deathPoint)
            {
                if (obj != null && gameObject != null)
                {
                    Destroy(gameObject);
                }

                // Spawn Teleport Ability from Big Enemy.

                if (teleportAbility != null)
                {
                    Dictionary<float, string> colorID = new Dictionary<float, string>
                    {
                        { 3, "Red" },
                        { 2, "Green" },
                        { 1, "Blue" },
                    };
                    colorID.TryGetValue(calcColor, out string colorName);
                    GameObject abilityClone = Instantiate(teleportAbility, spawnPosition, Quaternion.identity);
                    abilityClone.GetComponent<SpriteRenderer>().color = enemyColor;
                    abilityClone.name = colorName + " Teleport Ability(Clone)";
                    return;
                }

                // Spawn Health from Big Enemy Clone.

                if (gameObject.name == "Enemy(Clone)" || gameObject.name.Substring(0,11) == "Red Triplet")
                {
                    Instantiate(health, spawnPosition, Quaternion.identity);
                }
            }
        }
    }
}