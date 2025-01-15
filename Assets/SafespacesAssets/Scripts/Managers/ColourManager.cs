using HSVPicker;
using System;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.UI;

public class ColourManager : MonoBehaviour
{
    public ColorPicker colourPicker;
    public GameObject colourMenu;
    public Outline buttonOutline;
    public Material[] materials;
    public GameObject lights;
    public GameObject envRoom;

    private int selectedIndex;
    private enum ColourModes
    {
        COLOUR_ROOM,
        COLOUR_BALL,
        COLOUR_BLOCK,
        COLOUR_LIGHT
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectedIndex = -1;
        colourMenu.SetActive(false);
        colourPicker.gameObject.SetActive(false);

        foreach (Transform child in envRoom.transform)
        {
            child.GetComponent<Renderer>().material = materials[0]; // room material
        }

        colourPicker.onValueChanged.AddListener(color =>
        {
            SetColorFromPicker(color);
        });
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
        buttonOutline.enabled = true;
    }

    public void DisableMenu()
    {
        colourMenu.SetActive(false);
        colourPicker.gameObject.SetActive(false);
        buttonOutline.enabled = false;
    }

    public void ToggleColourMode(int index)
    {
        if (index == selectedIndex)
        {
            // double click a button to disable the picker
            colourPicker.gameObject.SetActive(false);
            colourMenu.transform.GetChild(index + 1).GetComponent<Outline>().enabled = false;
            selectedIndex = -1;
        }
        else
        {    
            // unset outline for previous selection
            if (selectedIndex != -1)
                colourMenu.transform.GetChild(selectedIndex + 1).GetComponent<Outline>().enabled = false;

            selectedIndex = index;

            // bring up its menu
            colourPicker.gameObject.SetActive(true);
            colourPicker.CurrentColor = GetColorForIndex(index);

            // set outline for new selection
            Outline o = colourMenu.transform.GetChild(index + 1).GetComponent<Outline>();
            o.enabled = true;
            o.effectColor = SafespacesUtils.green;
        }
    }

    public Color GetColorForIndex(int index)
    {
        Color c = new Color(0,0,0);

        if (index != (int)ColourModes.COLOUR_LIGHT)
        {
            c = materials[index].color;
        }
        else
        {
            c = lights.transform.GetChild(index).GetComponent<Light>().color;
        }
        
        return c;
    }

    public void SetColorFromPicker(Color color)
    {
        if (selectedIndex != (int)ColourModes.COLOUR_LIGHT)
        {
            materials[selectedIndex].color = color;
        }
        else
        {
            for (int i = 0; i < lights.transform.childCount; i++)
            {
                lights.transform.GetChild(i).GetComponent<Light>().color = color;
            }
        }
    }
}
