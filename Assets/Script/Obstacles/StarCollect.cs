using UnityEngine;
using UnityEngine.SceneManagement;

public class StarCollect : MonoBehaviour
{
    public int scoreGain = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.instance.AddScore(scoreGain);
            Destroy(gameObject);
        }
    }
}
