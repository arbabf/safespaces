using HSVPicker;
using UnityEngine;

public class ModifyEnvMaterial : MonoBehaviour
{
    const float DELTA = 0.01f;

    public Material roomMaterial;
    public ColorPicker picker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        picker.CurrentColor = Color.gray;

        foreach (Transform child in transform)
        {
            child.GetComponent<Renderer>().material = roomMaterial;
        }

        picker.onValueChanged.AddListener(color =>
        {
            roomMaterial.color = color;
        });
        roomMaterial.color = picker.CurrentColor;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
