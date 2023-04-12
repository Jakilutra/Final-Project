using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public Rigidbody2D bullet;
    private PlayerScript playerScript;
    private EnemyScript enemyScript;
    public float waitTime = 0;
    private int waitCount = 0, waitPoint = 3;

    void Start()
    {
        playerScript = FindObjectOfType<PlayerScript>();
        enemyScript = FindObjectOfType<EnemyScript>();
        waitTime = Random.Range(0f, 2f);
    }

    // Update is called once per frame.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameObject.CompareTag("Player"))
        {
            Fire("Player", playerScript.runSpeed * 5, Vector2.zero, Quaternion.identity, 0.3f, 1);
        }
        if (gameObject.CompareTag("Enemy") && bullet != null)
        {
            // Assigns distance, scale and direction variables.

            float distance = Vector2.Distance(transform.position, playerScript.transform.position);
            float scale = transform.localScale.x;
            Vector2 direction = (playerScript.transform.position) - this.transform.position;

            if (distance < scale*6)
            {

                // Increments the enemy wait time.

                waitTime += Time.deltaTime;
                waitCount = Mathf.RoundToInt(waitTime);

                // Constrains the angle to pi/4 radians (i.e. to the 8 vertices of an octagon) and positions/rotates the bullets accordingly.

                float angle = Mathf.Atan2(direction.y, direction.x);
                float constrainedAngle = Mathf.RoundToInt(angle / (Mathf.PI / 4f)) * (Mathf.PI / 4f);
                Vector3 position = new Vector3(Mathf.Cos(constrainedAngle), Mathf.Sin(constrainedAngle), 0);
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, position);

                Fire("Enemy", enemyScript.runSpeed * 5, position, rotation, 0.5f, scale);
            }
        }
    }

    void Fire(string side, float bulletSpeed, Vector3 positionModifier, Quaternion rotationModifier, float expiry, float scale)
    {
        if (playerScript.activeColor != playerScript.colorWhite && (side != "Enemy" || waitCount >= waitPoint))
        {

            // Launch bullet.

            Rigidbody2D bulletClone = Instantiate(bullet, transform.position + positionModifier, rotationModifier * Quaternion.Euler(0, 0, 90) * transform.rotation);
            bulletClone.velocity = rotationModifier * Quaternion.Euler(0,0,90) * transform.right * bulletSpeed;

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