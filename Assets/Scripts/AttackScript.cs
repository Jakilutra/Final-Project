using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public Rigidbody2D bullet;
    private PlayerScript playerScript;
    private EnemyScript enemyScript;
    private BlockScript blockScript;
    public float waitTime = 0;
    private int waitCount = 0, waitPoint = 3;

    void Start()
    {
        playerScript = FindObjectOfType<PlayerScript>();
        enemyScript = FindObjectOfType<EnemyScript>();
        blockScript = FindObjectOfType<BlockScript>();
        waitTime = Random.Range(0f, 2f);
    }

    // Update is called once per frame.
    void Update()
    {
        if (gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire("Player", playerScript.runSpeed * 5, playerScript.activeColor, Vector2.zero, Quaternion.identity, 0.3f, 1);
            }
            else if (Input.GetKeyDown(KeyCode.E) && playerScript.activeColor == playerScript.colorBlue)
            {
                for (int i = 0; i < 5; i++)
                {
                    Fire("Player", playerScript.runSpeed * 5, playerScript.activeColor, transform.rotation * Vector2.up, Quaternion.identity, 0.5f, 1);
                    Fire("Player", playerScript.runSpeed * 5, playerScript.activeColor, transform.rotation * Vector2.right, Quaternion.AngleAxis(-90, Vector3.forward), 0.5f, 1);
                    Fire("Player", playerScript.runSpeed * 5, playerScript.activeColor, transform.rotation * Vector2.down, Quaternion.AngleAxis(180, Vector3.forward), 0.5f, 1);
                    Fire("Player", playerScript.runSpeed * 5, playerScript.activeColor, transform.rotation * Vector2.left, Quaternion.AngleAxis(90, Vector3.forward), 0.5f, 1);
                }
            }
        }
        if (gameObject.CompareTag("Enemy") && bullet != null)
        {
            SpriteRenderer rendereb = gameObject.GetComponent<SpriteRenderer>();

            Color bulletColor = rendereb.color;
            bulletColor.a = 0.5f;

            // Assigns distance, scale and direction variables.

            float distance = Vector2.Distance(transform.position, playerScript.transform.position);
            float scale = transform.localScale.x;
            Vector2 direction = (playerScript.transform.position) - this.transform.position;

            if (distance < scale * 6)
            {

                // Increments the enemy wait time.

                waitTime += Time.deltaTime;
                waitCount = Mathf.RoundToInt(waitTime);

                // Constrains the angle to pi/4 radians (i.e. to the 8 vertices of an octagon) and positions/rotates the bullets accordingly.

                float angle = Mathf.Atan2(direction.y, direction.x);
                float constrainedAngle = Mathf.RoundToInt(angle / (Mathf.PI / 4f)) * (Mathf.PI / 4f);
                Vector3 position = new Vector3(Mathf.Cos(constrainedAngle), Mathf.Sin(constrainedAngle), 0);
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, position);

                if (scale < 5)
                {
                    Fire("Enemy", enemyScript.runSpeed * 5, bulletColor, position, rotation, 0.5f, scale);
                }
                else
                {
                    int speedModifier;
                    speedModifier = blockScript.blockOn ? 5 : 10;
                    for (int i = 0; i < 5; i++)
                    {
                        Fire("Enemy", enemyScript.runSpeed * speedModifier, bulletColor, position, rotation, 0.5f, 1);
                        waitCount = waitPoint;
                    }
                    waitTime = Random.Range(0f, 2f);
                    waitCount = 0;
                }
            }
        }
    }

    public void Fire(string side, float bulletSpeed, Color bulletColor, Vector3 positionModifier, Quaternion rotationModifier, float expiry, float scale)
    {
        if (playerScript.activeColor != playerScript.colorWhite && (side != "Enemy" || waitCount >= waitPoint))
        {

            // Launch bullet.

            Rigidbody2D bulletClone = Instantiate(bullet, transform.position + positionModifier, rotationModifier * Quaternion.Euler(0, 0, 90) * transform.rotation);
            bulletClone.velocity = rotationModifier * Quaternion.Euler(0,0,90) * transform.right * bulletSpeed;

            bulletClone.GetComponent<SpriteRenderer>().color = bulletColor;

            // Destroy the bullet after a fixed amount of time.

            Destroy(bulletClone.gameObject, expiry*Mathf.Sqrt(scale));

            // Reset enemy wait.

            waitTime = Random.Range(0f, 2f);
            waitCount = 0;
            
        }
        else if (side == "Player")
        {
            StartCoroutine(playerScript.Flicker(playerScript.gameObject));
        }
    }
}