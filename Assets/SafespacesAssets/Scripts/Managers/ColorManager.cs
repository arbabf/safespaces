using HSVPicker;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public ColorPicker colorPicker;
    public GameObject colorMenu;
    public Outline buttonOutline;
    public Material[] materials;
    public GameObject lights;
    public GameObject envRoom;
    public Material lightShadeMaterial;

    private int selectedIndex;
    private enum ColorModes
    {
        COLOR_ROOM,
        COLOR_BALL,
        COLOR_BLOCK,
        COLOR_LIGHT
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectedIndex = -1;
        colorMenu.SetActive(false);
        colorPicker.gameObject.SetActive(false);

        lightShadeMaterial.color = lights.transform.GetChild(0).GetComponent<Light>().color;

        colorPicker.onValueChanged.AddListener(color =>
        {
            SetColorFromPicker(color);
        });
    }

    public void ToggleMenu()
    {
        if (colorMenu.activeSelf)
            DisableMenu();
        else
            EnableMenu();
    }

    public void EnableMenu()
    {
        colorMenu.SetActive(true);
        buttonOutline.enabled = true;
    }

    public void DisableMenu()
    {
        // disable our selected colour mode by selecting its index
        if (colorMenu.activeSelf)
        {
            colorMenu.SetActive(false);
            ToggleColorMode(selectedIndex);
            buttonOutline.enabled = false;
        }
    }

    public void ToggleColorMode(int index)
    {
        if (index == selectedIndex)
        {
            // double click a button to disable the picker
            colorPicker.gameObject.SetActive(false);
            Outline line = colorMenu.transform.GetChild(index + 1).GetComponent<Outline>();
            if (line)
                line.enabled = false;
            selectedIndex = -1;
        }
        else
        {    
            // unset outline for previous selection
            if (selectedIndex != -1)
                colorMenu.transform.GetChild(selectedIndex + 1).GetComponent<Outline>().enabled = false;

            selectedIndex = index;

            // bring up its menu
            colorPicker.gameObject.SetActive(true);
            colorPicker.CurrentColor = GetColorForIndex(index);

            // set outline for new selection
            Outline o = colorMenu.transform.GetChild(index + 1).GetComponent<Outline>();
            o.enabled = true;
            o.effectColor = SafespacesUtils.green;
        }
    }

    public Color GetColorForIndex(int index)
    {
        Color c = new Color(0,0,0);

        if (index != (int)ColorModes.COLOR_LIGHT)
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
        if (selectedIndex != (int)ColorModes.COLOR_LIGHT)
        {
            materials[selectedIndex].color = color;
        }
        else
        {
            for (int i = 0; i < lights.transform.childCount; i++)
            {
                lights.transform.GetChild(i).GetComponent<Light>().color = color;
                lightShadeMaterial.color = color;
            }
        }
    }
}
