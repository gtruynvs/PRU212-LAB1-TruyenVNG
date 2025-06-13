using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float resetPositionX = -20f; // vị trí khi ra khỏi màn
    [SerializeField] private float startPositionX = 20f;  // vị trí đặt lại đằng sau

    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * PlayerController.instance.boost * Time.deltaTime);

        if (transform.position.x < resetPositionX)
        {
            Vector3 newPos = transform.position;
            newPos.x = startPositionX;
            transform.position = newPos;
        }
    }
}
