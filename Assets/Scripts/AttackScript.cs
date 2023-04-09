using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public Rigidbody2D bullet;
    private PlayerScript playerScript;
    private EnemyScript enemyScript;
    private float waitTime = 0;
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
            Fire("Player", playerScript.runSpeed * 5, Vector2.zero, Quaternion.identity, 0.3f);
        }
        if (gameObject.CompareTag("Enemy") && bullet != null)
        {
            float distance = Vector2.Distance(transform.position, playerScript.transform.position);
            float scale = transform.localScale.x;
            Vector2 direction = playerScript.transform.position - this.transform.position;

            if (distance < scale*5)
            {
                waitTime += Time.deltaTime;
                waitCount = Mathf.RoundToInt(waitTime);
                Vector3 position = direction.normalized;
                Quaternion rotation = Quaternion.LookRotation(transform.forward, direction);

                Fire("Enemy", enemyScript.runSpeed * 5, position, rotation, 0.5f);
            }
        }
    }

    void Fire(string side, float bulletSpeed, Vector3 positionModifier, Quaternion rotationModifier, float expiry)
    {
        if (playerScript.activeColor != playerScript.colorWhite && (side != "Enemy" || waitCount == waitPoint))
        {

            // Launch bullet.

            Rigidbody2D bulletClone = Instantiate(bullet, transform.position + positionModifier, rotationModifier * Quaternion.Euler(0, 0, 90) * transform.rotation);
            bulletClone.velocity = rotationModifier * Quaternion.Euler(0,0,90) * transform.right * bulletSpeed;

            // Destroy the bullet after a fixed amount of time.

            Destroy(bulletClone.gameObject, expiry);

            // Reset enemy wait.

            waitTime = Random.Range(0, 3);
            waitCount = 0;
        }
        else if (side == "Player")
        {
            StartCoroutine(playerScript.Flicker(playerScript.gameObject));
        }
    }
}