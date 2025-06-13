using UnityEngine;

public class StarEffect : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Random v?n t?c r?i (t?ng x n?u mu?n l?ch sang b�n)
        float randomX = Random.Range(-0.5f, 0.5f);
        float randomY = Random.Range(-2f, -5f);
        rb.linearVelocity = new Vector2(randomX, randomY);

        // Random scale
        float scale = Random.Range(0.8f, 1.2f);
        transform.localScale = new Vector3(scale, scale, 1f);

        // Random xoay (t�y ch?n)
        float rotation = Random.Range(-200f, 200f);
        rb.angularVelocity = rotation;

        // T�y ch?n: random m�u
        GetComponent<SpriteRenderer>().color = new Color(
            Random.Range(0.8f, 1f), // R
            Random.Range(0.8f, 1f), // G
            Random.Range(0.8f, 1f), // B
            1f                      // A
        );
    }
}
