using UnityEngine;
using UnityEngine.UI;

public class FurnitureManager : MonoBehaviour
{
    public GameObject[] furniture;
    public GameObject furnitureMenu;
    public Outline buttonOutline;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisableMenu();
    }

    public void ToggleMenu()
    {
        if (furnitureMenu.activeSelf)
            DisableMenu();
        else
            EnableMenu();
    }

    public void EnableMenu()
    {
        furnitureMenu.SetActive(true);
        buttonOutline.enabled = true;
    }

    public void DisableMenu()
    {
        furnitureMenu.SetActive(false);
        buttonOutline.enabled = false;
    }

    public void ToggleFurniture(int index)
    {
        if (!furniture[index].activeSelf)
        {
            furniture[index].SetActive(true);
            furnitureMenu.transform.GetChild(index + 1).GetComponent<Outline>().enabled = true;
        }
        else
        {
            furniture[index].SetActive(false);
            furnitureMenu.transform.GetChild(index + 1).GetComponent<Outline>().enabled = false;
        }
    }
}
