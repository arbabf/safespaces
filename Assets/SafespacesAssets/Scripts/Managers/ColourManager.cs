using UnityEngine;
using UnityEngine.UI;

public class ColourManager : MonoBehaviour
{
    public GameObject colourPicker;
    public GameObject colourMenu;
    public Outline buttonOutline;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colourMenu.SetActive(false);
    }

    public void ToggleMenu()
    {
        if (colourMenu.activeSelf)
            DisableMenu();
        else
            EnableMenu();
    }

    public void EnableMenu()
    {
        colourMenu.SetActive(true);
        colourPicker.SetActive(true);
        buttonOutline.enabled = true;
    }

    public void DisableMenu()
    {
        colourMenu.SetActive(false);
        colourPicker.SetActive(false);
        buttonOutline.enabled = false;
    }
}
