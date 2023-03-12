using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    // Declare Player Physics/Movement Variables

    Rigidbody2D body;

    float horizontal;
    float vertical;

    float runSpeed = 5.0f;

    // Declare Player Colour Variables

    Color activeColor;
    SpriteRenderer render;
    Dictionary<Color, Color> colorChange = new Dictionary<Color, Color>();
    Color colorGreen = new Color(0.25f, 1f, 0.25f);
    Color colorRed = new Color(1f, 0.25f, 0.25f);
    Color colorBlue = new Color(0.25f, 0.25f, 1f);

    // Finishes Setting of Player Colour Variables

    void ColorChangeSetUp()
    {
        activeColor = Color.white;
        render = GetComponent<SpriteRenderer>();
        colorChange = new Dictionary<Color, Color>
        {
            { Color.white, colorGreen },
            { colorGreen, colorRed },
            { colorRed, colorBlue },
            { colorBlue, Color.white }
        };
    }

    // StartUp

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ColorChangeSetUp();

    }

    // Tick

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Color Change Event

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (colorChange.TryGetValue(activeColor, out Color newColor))
            {
                activeColor = newColor;
                render.color = newColor;
            }
        }

        // Game Over Event

        if (Input.GetKeyDown(KeyCode.G))
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}