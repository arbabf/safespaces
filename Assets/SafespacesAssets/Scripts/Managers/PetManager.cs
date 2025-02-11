using System;
using UnityEngine;
using UnityEngine.UI;

public class PetManager : MonoBehaviour
{
    public GameObject bed;
    public GameObject[] pets;

    public GameObject petMenu;
    public Outline buttonOutline;

    private int currPet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisableMenu();
        currPet = -1;
    }

    public void ToggleMenu()
    {
        if (petMenu.activeSelf)
            DisableMenu();
        else
            EnableMenu();
    }

    public void EnableMenu()
    {
        petMenu.SetActive(true);
        buttonOutline.enabled = true;
    }

    public void DisableMenu()
    {
        petMenu.SetActive(false);
        buttonOutline.enabled = false;
    }

    public void SetPet(int index)
    {
        // todo: don't run any of this code if the player has a gameobject selected, we can narrow it down to a bed later
        if (currPet == index)
        {
            pets[index].SetActive(false);
            bed.SetActive(false);
            petMenu.transform.GetChild(index + 1).GetComponent<Outline>().enabled = false;
            currPet = -1;
        }
        else
        {
            if (currPet != -1)
                pets[currPet].SetActive(false);

            pets[index].SetActive(true);
            bed.SetActive(true);

            // unset outline for previous selection
            if (currPet != -1)
                petMenu.transform.GetChild(currPet + 1).GetComponent<Outline>().enabled = false;

            petMenu.transform.GetChild(index + 1).GetComponent<Outline>().enabled = true;

            currPet = index;
        }
    }
}
