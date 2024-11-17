using UnityEngine;

public class BubbleThink : MonoBehaviour
{
    private Vector3 moveDirection;
    private Vector3 pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveDirection = Vector3.up * 0.01f;
        InvokeRepeating("MoveUp", 0.0f, 0.016f);
    }

    void MoveUp()
    {
        transform.position += moveDirection;
        moveDirection += new Vector3(Random.Range(-0.002f, 0.002f), Random.Range(-0.0005f, 0.0005f), Random.Range(-0.002f, 0.002f));
        if (moveDirection.y <= 0.005f)
        {
            moveDirection.y = 0.005f; // keep it moving upwards
        }
    }
}
