using UnityEngine;

public class Boom : MonoBehaviour
{
    [SerializeField] private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            float moveX = (GameManager.instance.worldSpeed * PlayerController.instance.boost) * Time.deltaTime;
            transform.position += new Vector3(-moveX, 0);
            if (transform.position.x < -11)
            {
                Destroy(gameObject);
            }
        }
    }
}
