using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    // Declare player physics/movement variables.

    Rigidbody2D body;

    private float horizontal;
    private float vertical;
    private Vector2 movementDirection;

    private float runSpeed = 5f;
    private float rotationSpeed = 3600f;

    // Declare player colour variables.

    public Color activeColor;
    private SpriteRenderer render;
    private SpriteRenderer renderb;
    public GameObject bullet;
    private Dictionary<Color, Color> colorChange = new Dictionary<Color, Color>();
    public Color colorWhite = new Color(1f, 1f, 1f);
    public Color colorGreen = new Color(0.25f, 1f, 0.25f);
    public Color colorRed = new Color(1f, 0.25f, 0.25f);
    public Color colorBlue = new Color(0.25f, 0.25f, 1f);
    private Dictionary<Color, float> speedChange = new Dictionary<Color, float>();

    // Declare ability variables.

    public Dictionary<string, bool> hasAbility = new Dictionary<string, bool>();

    // Declare collectible, message and teleporter variables.

    public GameObject greenWallAbility;
    public GameObject greenTeleportAbility;

    public GameObject matchColor;
    public GameObject typeR;
    public GameObject typeSpace;
    public GameObject typeT;
    public GameObject whiteSafe;

    public GameObject greenTeleporter1;
    public GameObject greenTeleporter2;


    // Assigning variables (physics, colour and abilities).

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ColorChangeSetUp();
        hasAbility = new Dictionary<string, bool>
        {
                    { "GreenWall", false },
                    { "GreenTeleport", false },
                    { "RedWall", false },
                    { "RedTeleport", false },
                    { "BlueWall", false },
                    { "BlueTeleport", false },
        };
    }

    // Finishes setting of player colour variables.

    void ColorChangeSetUp()
    {
        colorWhite.a = 0.75f;
        activeColor = colorWhite;
        render = GetComponent<SpriteRenderer>();
        renderb = bullet.GetComponent<SpriteRenderer>();
        gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");
        speedChange = new Dictionary<Color, float>
        {
            { colorWhite, 5f },
            { colorGreen, 6f },
            { colorRed, 7f },
            { colorBlue, 8f },
        };
    }

    // Tick

    void Update()
    {
        // Player movement update.

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;

        if (movementDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Color change event.

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (colorChange.TryGetValue(activeColor, out Color newColor) && !GetComponent<PolygonCollider2D>().isTrigger)
            {
                activeColor = newColor;
                render.color = newColor;
                renderb.color = newColor;
                speedChange.TryGetValue(newColor, out runSpeed);
                if (newColor == colorWhite)
                {
                    gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");
                }
                else
                {
                    gameObject.layer = LayerMask.NameToLayer("Player");
                }
            }
            else
            {
                StartCoroutine(Flicker(gameObject));
            }
        }
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    // Collision.

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Game over event.
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().GameOver();
        }

        // Player upgrade events (via collectibles).

        if (collision.gameObject.CompareTag("Collectible"))
        {
            if (collision.gameObject == greenWallAbility)
            {
                Destroy(greenWallAbility);
                hasAbility["GreenWall"] = true;
                colorChange = new Dictionary<Color, Color>
                {
                    { colorWhite, colorGreen },
                    { colorGreen, colorWhite },
                };
                activeColor = colorGreen;
                render.color = colorGreen;
                renderb.color = colorGreen;
                gameObject.layer = LayerMask.NameToLayer("Player");
                runSpeed = 6f;
                typeR.SetActive(true);
                typeSpace.SetActive(true);
                whiteSafe.SetActive(true);
                return;
            }
            GameObject gTAClone = GameObject.Find(greenTeleportAbility.name + "(Clone)");
            if (collision.gameObject == gTAClone)
            {
                Destroy(gTAClone);
                hasAbility["GreenTeleport"] = true;
                greenTeleporter1.SetActive(true);
                greenTeleporter2.SetActive(true);
                matchColor.SetActive(true);
                typeT.SetActive(true);
                activeColor = colorGreen;
                render.color = colorGreen;
                renderb.color = colorGreen;
                gameObject.layer = LayerMask.NameToLayer("Player");
                runSpeed = 6f;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Game over event.
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    public IEnumerator Flicker(GameObject player)
    {
        SpriteRenderer render = player.GetComponent<SpriteRenderer>();
        render.enabled = false;
        yield return new WaitForSeconds(0.1f);
        render.enabled = true;
    }
}