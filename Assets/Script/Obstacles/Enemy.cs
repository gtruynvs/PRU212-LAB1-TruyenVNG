using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private int starCount = 2;

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
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (destroyEffect != null)
                Instantiate(destroyEffect, transform.position, transform.rotation);

            if (starPrefab != null)
            {
                for (int i = 0; i < starCount; i++)
                {
                    Vector3 spawnPos = transform.position + (Vector3)(Random.insideUnitCircle * 0.5f);
                    Instantiate(starPrefab, spawnPos, Quaternion.identity);
                }
            }

            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
