using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    private float blockWait;
    public bool blockOn;
    private GameObject player; 
    private PlayerScript playerScript;
    private EnemyScript enemyScript;
    private float rotationSpeed = 1800f;

    void Start()
    {
        blockWait = 0f;
        blockOn = false;
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();
        if (gameObject != player)
        {
            enemyScript = GetComponent<EnemyScript>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && gameObject.CompareTag("Player") && playerScript.activeColor != playerScript.colorWhite && playerScript.activeColor != playerScript.colorGreen)
        {
            if (blockWait <= 0f)
            {
                blockWait = 4f;
                blockOn = true;
            }
            else
            {
                StartCoroutine(playerScript.Flicker(player));
            }
        }
        if (playerScript.activeColor != playerScript.colorWhite && gameObject.CompareTag("Enemy") && enemyScript.runSpeed != 2f &&  blockWait <= 0f)
        {
            blockWait = 4f;
            blockOn = true;
        }
        if (blockWait > 0f)
        {
            blockWait -= Time.deltaTime;
            if (blockWait < 2f)
            {
                blockOn = false;
            }
        }
        if (blockOn)
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }
}
