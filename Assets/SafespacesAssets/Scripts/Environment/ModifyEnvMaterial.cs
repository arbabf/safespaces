using HSVPicker;
using UnityEngine;

public class ModifyEnvMaterial : MonoBehaviour
{
    const float DELTA = 0.01f;

    public Material roomMaterial;
    public ColorPicker picker;
    public GameObject lights;
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
            SetLightColor(color);
        });
        //roomMaterial.color = picker.CurrentColor;
    }

    void SetLightColor(Color color)
    {
        for (int i = 0; i < lights.transform.childCount; i++)
        {
            lights.transform.GetChild(i).GetComponent<Light>().color = color;
        }
    }
}
