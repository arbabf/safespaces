using UnityEngine;
using UnityEngine.UI;

public class ColourManager : MonoBehaviour
{
    public GameObject colourPicker;
    public Outline buttonOutline;

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
        if (colourPicker.activeSelf)
            DisableColourPicker();
        else
            EnableColourPicker();
    }

    public void EnableColourPicker()
    {
        colourPicker.SetActive(true);
        buttonOutline.enabled = true;
    }

    public void DisableColourPicker()
    {
        colourPicker.SetActive(false);
        buttonOutline.enabled = false;
    }
}
