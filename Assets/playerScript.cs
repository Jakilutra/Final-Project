using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    // Declare Player Physics/Movement Variables
    Rigidbody2D body;

    float horizontal;
    float vertical;

    float runSpeed = 5.0f;

    // Declare Player Colour Variables

    SpriteRenderer render;
    Color activeColor;

    Dictionary<Color, Color> colorChange = new Dictionary<Color, Color>
    {
        { Color.white, Color.green },
        { Color.green, Color.red },
        { Color.red, Color.blue },
        { Color.blue, Color.white }
    };

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        activeColor = Color.white;
        render = GetComponent<SpriteRenderer>();
        FindObjectOfType<gameManager>().GameOver();

    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (colorChange.TryGetValue(activeColor, out Color newColor))
            {
                activeColor = newColor;
                render.color = newColor;
            }
        }
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}