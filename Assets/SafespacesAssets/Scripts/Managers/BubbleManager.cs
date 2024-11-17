using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public GameObject bubble;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void CreateBubble()
    {
        GameObject b = Instantiate(bubble, new Vector3(-4, 1, 25), Quaternion.identity);
        Destroy(b, Random.Range(3, 6));
    }
}
