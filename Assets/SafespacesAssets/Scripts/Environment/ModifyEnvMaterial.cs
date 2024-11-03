using UnityEngine;

public class ModifyEnvMaterial : MonoBehaviour
{
    const float DELTA = 0.01f;

    public Material roomMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Renderer>().material = roomMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Color c = roomMaterial.color;
        for (int i = 0; i < 3; i++)
        {
            c[i] = Mathf.Clamp(c[i] + Random.Range(-DELTA, DELTA), 0, 1);
        }

        roomMaterial.color = c;
    }
}
