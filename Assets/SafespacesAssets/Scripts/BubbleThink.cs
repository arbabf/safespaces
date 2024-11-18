using UnityEngine;

public class BubbleThink : MonoBehaviour
{
    private Vector3 moveDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveDirection = Vector3.up * 0.005f;
        InvokeRepeating(nameof(MoveUp), 0.0f, 0.016f);
    }

    void MoveUp()
    {
        transform.position += moveDirection;
        moveDirection += new Vector3(Random.Range(-0.001f, 0.001f), Random.Range(-0.00025f, 0.00025f), Random.Range(-0.001f, 0.001f));
        if (moveDirection.y <= 0.0025f)
        {
            moveDirection.y = 0.0025f; // keep it moving upwards
        }
    }
}
