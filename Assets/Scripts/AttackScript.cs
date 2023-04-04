using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    private float bulletSpeed = 100;
    public Rigidbody2D bullet;
    private PlayerScript playerScript;

    void Start()
    {
        playerScript = FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerScript.activeColor != playerScript.colorWhite)
            {
                Rigidbody2D bulletClone = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, -90) * transform.rotation);
                bulletClone.velocity = Quaternion.Euler(0, 0, 90) * transform.right * bulletSpeed;

                // Destroy the bullet after a fixed amount of time
                Destroy(bulletClone.gameObject, 0.5f);
            }
            else
            {
                StartCoroutine(playerScript.TogglePlayerVisibility(playerScript.gameObject));
            }
        }
    }
}
