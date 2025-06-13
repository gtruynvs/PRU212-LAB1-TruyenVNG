using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private Material defaultMaterial;
    [SerializeField] private Material whiteMaterial;

    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private int lives;
    [SerializeField] private Sprite[] asteroidSprites;
    
    public GameObject starPrefab;
    public int starCount = 3;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer.sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length)];
        float pushX = Random.Range(-1f, 0);
        float pushY = Random.Range(-1f, 1f);
        rb.linearVelocity = new Vector2(pushX, pushY);
        float randomScale = Random.Range(2f, 5f);
        transform.localScale = new Vector2(randomScale,randomScale);
    }

    void Update()
    {
        float moveX = (GameManager.instance.worldSpeed * PlayerController.instance.boost) * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);
        if (transform.position.x < -11)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool hitByBullet = collision.gameObject.CompareTag("Bullet");
        bool hitByPlayer = collision.gameObject.CompareTag("Player");

        if (hitByBullet || hitByPlayer)
        {
            AudioManager.instance.PlaySound(AudioManager.instance.hit);
            spriteRenderer.material = whiteMaterial;
            StartCoroutine("ResetMaterial");
            AudioManager.instance.PlayModifiedSound(AudioManager.instance.hitRock);
            lives--;

            if (lives <= 0)
            {
                Instantiate(destroyEffect, transform.position, transform.rotation);
                AudioManager.instance.PlayModifiedSound(AudioManager.instance.boom2);

                if (hitByBullet && starPrefab != null)
                {
                    for (int i = 0; i < starCount; i++)
                    {
                        Vector3 spawnPos = transform.position + (Vector3)(Random.insideUnitCircle * 0.5f);
                        Instantiate(starPrefab, spawnPos, Quaternion.identity);
                    }
                }

                Destroy(gameObject);
            }
        }
    }


    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMaterial;
    }

}
