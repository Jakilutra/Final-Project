﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    // Declare Game Manager Script.

    private GameManager gameManager;
    
    // Declare player physics/movement variables.

    Rigidbody2D body;

    private float horizontal, vertical;
    private Vector2 movementDirection;

    public float runSpeed = 5f, rotationSpeed = 3600f;

    // Declare player colour variables.

    public Color activeColor;
    private SpriteRenderer render, renderb;
    public GameObject bullet;
    public Dictionary<Color, Color> colorChange = new Dictionary<Color, Color>();
    public Color colorWhite = new Color(1f, 1f, 1f);
    public Color colorGreen = new Color(0.25f, 1f, 0.25f);
    public Color colorRed = new Color(1f, 0.25f, 0.25f);
    public Color colorBlue = new Color(0.25f, 0.25f, 1f);
    private Dictionary<Color, float> speedChange = new Dictionary<Color, float>();

    // Declare ability variables.

    public Dictionary<string, bool> hasAbility = new Dictionary<string, bool>();

    // Declare collectible, message and teleporter variables.

    public GameObject greenWallAbility, redWallAbility, blueWallAbility;
    public GameObject matchColor, typeR, typeSpace, typeT, whiteSafe;
    public GameObject greenTeleporter1, greenTeleporter2;

    // Declare player damage variables.

    private int deathCounter;
    private bool pauseDamage;
    private BlockScript blockScript;

    // Overlay variables

    private GameObject textObj, background;
    private Text textComponent;
    public int points;
    private int damagedCount;
    public Font arialFont;
    private RectTransform rectTransform;
    private Vector2 originalDelta;

    void Start()
    {
        // Assigning variables (game manager, physics, colour, abilities, damage and points).
        gameManager = FindObjectOfType<GameManager>();
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
        deathCounter = 0;
        pauseDamage = false;
        points = 0;
        damagedCount = 0;

        // Assigning overlay variables

        blockScript = GetComponent<BlockScript>();
        textObj = GameObject.Find("Text");
        textComponent = textObj.GetComponent<Text>();
        background = GameObject.Find("Background");
        rectTransform = background.GetComponent<RectTransform>();
        originalDelta = rectTransform.sizeDelta;

        UpdateOverlay();
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

        if (movementDirection != Vector2.zero & !blockScript.blockOn)
        {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeColor();
        }
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    // Collision.

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerDamage(collision.gameObject);

        // Player upgrade/health restore events (via collectibles).

        if (collision.gameObject.CompareTag("Collectible"))
        {
            if (collision.gameObject == greenWallAbility)
            {
                Destroy(greenWallAbility);
                hasAbility["GreenWall"] = true;
                colorChange = new Dictionary<Color, Color>
                {
                    { colorWhite, colorGreen },
                    { colorGreen, colorWhite }
                };
                activeColor = colorGreen;
                render.color = colorGreen;
                renderb.color = colorGreen;
                gameObject.layer = LayerMask.NameToLayer("Player");
                runSpeed = 6f;
                typeR.SetActive(true);
                typeSpace.SetActive(true);
                whiteSafe.SetActive(true);
                deathCounter = 0;
                UpdateOverlay();
                return;
            }
            string gTAClone = "Green Teleport Ability(Clone)";
            if (collision.gameObject.name == gTAClone)
            {
                Destroy(collision.gameObject);
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
                deathCounter = 0;
                UpdateOverlay();
                return;
            }
            if (collision.gameObject == redWallAbility)
            {
                Destroy(redWallAbility);
                hasAbility["RedWall"] = true;
                colorChange = new Dictionary<Color, Color>
                {
                    { colorWhite, colorGreen },
                    { colorGreen, colorRed },
                    { colorRed, colorWhite }
                };
                activeColor = colorRed;
                render.color = colorRed;
                renderb.color = colorRed;
                gameObject.layer = LayerMask.NameToLayer("Player");
                runSpeed = 7f;
                deathCounter = 0;
                UpdateOverlay();
                return;
            }
            string rTAClone = "Red Teleport Ability(Clone)";
            if (collision.gameObject.name == rTAClone)
            {
                Destroy(collision.gameObject);
                hasAbility["RedTeleport"] = true;
                activeColor = colorRed;
                render.color = colorRed;
                renderb.color = colorRed;
                gameObject.layer = LayerMask.NameToLayer("Player");
                runSpeed = 7f;
                deathCounter = 0;
                UpdateOverlay();
                return;
            }
            if (collision.gameObject == blueWallAbility)
            {
                Destroy(blueWallAbility);
                hasAbility["BlueWall"] = true;
                colorChange = new Dictionary<Color, Color>
                {
                    { colorWhite, colorGreen },
                    { colorGreen, colorRed },
                    { colorRed, colorBlue },
                    { colorBlue, colorWhite }
                };
                activeColor = colorBlue;
                render.color = colorBlue;
                renderb.color = colorBlue;
                gameObject.layer = LayerMask.NameToLayer("Player");
                runSpeed = 8f;
                deathCounter = 0;
                gameManager.Deactivate("FakeWall");
                GameObject typeP3 = GameObject.Find("Type P3");
                if (typeP3 != null)
                {
                    typeP3.SetActive(false);
                }
                UpdateOverlay();
                return;
            }
            string bTAClone = "Blue Teleport Ability(Clone)";
            if (collision.gameObject.name == bTAClone)
            {
                Destroy(collision.gameObject);
                hasAbility["BlueTeleport"] = true;
                activeColor = colorBlue;
                render.color = colorBlue;
                renderb.color = colorBlue;
                gameObject.layer = LayerMask.NameToLayer("Player");
                runSpeed = 8f;
                deathCounter = 0;
                UpdateOverlay();
                return;
            }
            string yTAClone = "Yellow Teleport Ability(Clone)";
            if (collision.gameObject.name == yTAClone)
            {
                gameObject.SetActive(false);
                Destroy(collision.gameObject);
                gameManager.GameWin();
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerDamage(other.gameObject);
        if ((other.gameObject.name.Substring(0, 6) == "Health" || other.gameObject.name == "Health(Clone)") & deathCounter != 0)
        {
            Destroy(other.gameObject);
            deathCounter -= 1 * Mathf.RoundToInt(other.gameObject.transform.localScale.x);
            deathCounter = deathCounter < 0 ? 0 : deathCounter;
            UpdateOverlay();
        }
    }

        // Color change event.

        void ChangeColor()
    {
        PolygonCollider2D playerCollider = GetComponent<PolygonCollider2D>();
        bool isMovingDiagonally = body.velocity.x != 0 && body.velocity.y != 0;
        bool isTouchingColorWall = playerCollider.IsTouchingLayers(LayerMask.GetMask("White Wall")) || playerCollider.IsTouchingLayers(LayerMask.GetMask("Green Wall")) || playerCollider.IsTouchingLayers(LayerMask.GetMask("Red Wall")) || playerCollider.IsTouchingLayers(LayerMask.GetMask("Blue Wall"));
        bool collidingWithWalls = playerCollider.IsTouchingLayers(LayerMask.GetMask("Wall")) && isTouchingColorWall && isMovingDiagonally;
        if (colorChange.TryGetValue(activeColor, out Color newColor) && !playerCollider.isTrigger && !collidingWithWalls)
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

    public IEnumerator Flicker(GameObject player)
    {
        SpriteRenderer render = player.GetComponent<SpriteRenderer>();
        render.enabled = false;
        yield return new WaitForSeconds(0.1f);
        render.enabled = true;
    }

    void PlayerDamage(GameObject obj)
    {
        if (!pauseDamage && !blockScript.blockOn)
        {
            if ((obj.CompareTag("Bullet") && obj.layer == LayerMask.NameToLayer("Enemy Bullet")) || obj.CompareTag("Enemy"))
            {
                deathCounter++;
                SpriteRenderer rendereb = obj.GetComponent<SpriteRenderer>();
                Color enemyBulletColor = rendereb.color;
                if (enemyBulletColor.r == 1 && deathCounter < 4)
                {
                    deathCounter++;
                }
                if (enemyBulletColor.b == 1 && deathCounter < 3)
                {
                    deathCounter += 2;
                }
                if (deathCounter > 5)
                {
                    deathCounter = 5;
                }
                damagedCount++;
                UpdateOverlay();
                if (obj != null)
                {
                    switch (deathCounter)
                    {
                        case 4:
                            FlickerOrange();
                            int attempts = 0;
                            while (activeColor != colorWhite && attempts < 5)
                            {
                                ChangeColor();
                                attempts++;
                            }
                            break;
                        case 5:
                            // Game over event.
                            gameObject.SetActive(false);
                            gameManager.GameOver();
                            break;
                        default:
                            FlickerOrange();
                            break;
                    }
                }
            }
        }
    }

    void FlickerOrange()
    {
        pauseDamage = true;
        InvokeRepeating("SwitchOrange", 0.2f, 0.2f);
        Invoke("CancelSwitchOrange", 2f);
    }

    void SwitchOrange()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        Color orange = new Color(1f, 0.5f, 0f);
        if (render.color == activeColor)
        {
            render.color = orange;
        }
        else
        {
            render.color = activeColor;
        }
    }

    void CancelSwitchOrange()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        CancelInvoke("SwitchOrange");
        render.color = activeColor;
        pauseDamage = false;
    }

    public void UpdateOverlay()
    {
        textComponent.font = arialFont;

        rectTransform.sizeDelta = originalDelta;

        string healthText = "      Health: ";
        int health = 5 - deathCounter;
        for (int i = 0; i < health; i++)
        {
            healthText += "<color=red>♥</color>";
        }
        for (int j = 0; j < deathCounter; j++)
        {
            healthText += "<color=grey>♥</color>";
        }
        string pointsText = "\n      Points: " + points;
        string damageText = "\n Times Hit: " + damagedCount;

        string rAbilityText = "";
        if (hasAbility["GreenWall"] || hasAbility["RedWall"] || hasAbility["BlueWall"])
        {
            rAbilityText = "\nR Abilities: ";
        }
        else
        {
            Vector2 delta = rectTransform.sizeDelta;
            delta.y -= 150f;
            rectTransform.sizeDelta = delta;
        }
        rAbilityText += hasAbility["GreenWall"] ? "<color=green>■ </color>" : "";
        rAbilityText += hasAbility["RedWall"] ? "<color=red>■ </color>" : "";
        rAbilityText += hasAbility["BlueWall"] ? "<color=blue>■ </color>" : "";

        string tAbilityText = "";
        if (hasAbility["GreenTeleport"] || hasAbility["RedTeleport"] || hasAbility["BlueTeleport"])
        {
            tAbilityText = "\n T Abilities: ";
        }
        else
        {
            Vector2 delta = rectTransform.sizeDelta;
            delta.y -= 75f;
            rectTransform.sizeDelta = delta;
        }
        tAbilityText += hasAbility["GreenTeleport"] ? "<color=green>● </color>" : "";
        tAbilityText += hasAbility["RedTeleport"] ? "<color=red>● </color>" : "";
        tAbilityText += hasAbility["BlueTeleport"] ? "<color=blue>● </color>" : "";

        textComponent.text = healthText 
                            + pointsText 
                            + damageText 
                            + "\n"
                            + rAbilityText 
                            + tAbilityText;
    }

}