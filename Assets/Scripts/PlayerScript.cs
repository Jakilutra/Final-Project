using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    // Declare Player Physics/Movement Variables

    Rigidbody2D body;

    private float horizontal;
    private float vertical;

    private float runSpeed = 5f;

    // Declare Player Colour Variables

    public Color activeColor;
    private SpriteRenderer render;
    private Dictionary<Color, Color> colorChange = new Dictionary<Color, Color>();
    public Color colorWhite = new Color(1f, 1f, 1f);
    private Color colorGreen = new Color(0.25f, 1f, 0.25f);
    private Color colorRed = new Color(1f, 0.25f, 0.25f);
    private Color colorBlue = new Color(0.25f, 0.25f, 1f);

    // Finishes Setting of Player Colour Variables

    void ColorChangeSetUp()
    {
        colorWhite.a = 0.75f;
        activeColor = colorWhite;
        render = GetComponent<SpriteRenderer>();
        colorChange = new Dictionary<Color, Color>
        {
            { colorWhite, colorGreen },
            { colorGreen, colorRed },
            { colorRed, colorBlue },
            { colorBlue, colorWhite }
        };
        gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");
    }

    // StartUp

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ColorChangeSetUp();
    }

    // Collision

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            // Game Over Event
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().GameOver();
        }
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
                if (newColor == colorWhite)
                {
                    gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");
                }
                else
                {
                    gameObject.layer = LayerMask.NameToLayer("Player");
                }
            }
        }
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}