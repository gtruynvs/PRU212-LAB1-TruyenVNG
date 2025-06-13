using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Material defaultMaterial;
    [SerializeField] private Material whiteMaterial;

    private Vector2 playerDirection;
    [SerializeField] private float moveSpeed;
    public float boost = 1f;
    private float boostPower = 5f;
    private bool isBoosting = false;

    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energyRegen;

    [SerializeField] private float health;
    [SerializeField] private float maxhealth;
    [SerializeField] private GameObject destroyEffect;

    private int score = 0;
    [SerializeField] private int maxScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;

        energy = maxEnergy;
        UIController.instance.UpdateEnergySlider(energy, maxEnergy);
        health = maxhealth;
        UIController.instance.UpdateHealthSlider(health, maxhealth);
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            animator.SetFloat("moveX", horizontalInput);
            animator.SetFloat("moveY", verticalInput);
            playerDirection = new Vector2(horizontalInput, verticalInput).normalized;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                EnterBoost();
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                ExitBoost();
            }

            if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetButtonDown("Fire1"))
            {
                PhaserWeapon.instance.Shoot();
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(playerDirection.x * moveSpeed, playerDirection.y * moveSpeed);
        if (isBoosting)
        {
            if (energy >= 0.2f) energy -= 0.2f;
            else
            {
                ExitBoost();
            }
        }
        else
        {
            if (energy < maxEnergy)
            {
                energy += energyRegen;
            }
        }
        UIController.instance.UpdateEnergySlider(energy, maxEnergy);
    }


    public void EnterBoost()
    {
        if (energy > 10)
        {
            AudioManager.instance.PlaySound(AudioManager.instance.fire);
            animator.SetBool("boosting", true);
            boost = boostPower;
            isBoosting = true;
        }

    }
    public void ExitBoost()
    {
        animator.SetBool("boosting", false);
        boost = 1f;
        isBoosting = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        UIController.instance.UpdateHealthSlider(health, maxhealth);
        AudioManager.instance.PlaySound(AudioManager.instance.hit);
        spriteRenderer.material = whiteMaterial;
        StartCoroutine(ResetMaterial());
        if (health <= 0)
        {
            boost = 0f;
            gameObject.SetActive(false);
            health = 0;
            Debug.Log("Player has died.");
            Instantiate(destroyEffect, transform.position, transform.rotation);
            GameManager.instance.GameOver();
            AudioManager.instance.PlaySound(AudioManager.instance.ice);
        }
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMaterial;
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);

        UIController.instance.UpdateScoreText(score, maxScore);
        if (score >= maxScore)
        {
            Debug.Log("Mission Complete!");
            GameManager.instance.MissionComplete();
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(1);
            // Destroy(collision.gameObject); 
        }

    }

}
