using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public GameObject ball;
    private const float VELOCITY = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateBall()
    {
        Rigidbody newBall = Instantiate(ball, new Vector3(-4, 2, 27), Quaternion.identity).transform.GetChild(0).GetComponent<Rigidbody>();
        newBall.AddForce(Random.Range(-VELOCITY, VELOCITY), Random.Range(-VELOCITY, VELOCITY), Random.Range(-VELOCITY, VELOCITY), ForceMode.VelocityChange);
    }
}
