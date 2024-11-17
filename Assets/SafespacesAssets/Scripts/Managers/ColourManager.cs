using UnityEngine;

public class ColourManager : MonoBehaviour
{
    public GameObject colourPicker;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colourPicker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleColourPicker()
    {
        colourPicker.SetActive(!colourPicker.activeSelf);
    }
}
