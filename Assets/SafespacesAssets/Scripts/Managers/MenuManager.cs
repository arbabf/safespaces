using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class MenuManager : MonoBehaviour
{
    public ColorManager colorManager;
    public AmbientSoundManager ambientSoundManager;
    public ObjectManager objectManager;
    public DayTimeManager dayTimeManager;
    public FurnitureManager furnitureManager;
    public PetManager petManager;
    public GameObject wristMenu1;
    public GameObject wristMenu2;
    public GameObject xrOrigin;

    private InputAction menuAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wristMenu1.SetActive(true);
        wristMenu2.SetActive(false);

        InputActionAsset inputActions = xrOrigin.GetComponent<InputActionManager>().actionAssets[0];
        menuAction = inputActions.FindActionMap("XRI Left Interaction").FindAction("Secondary Button");
        menuAction.performed += SwitchMenus;
    }

    /*
     * Primary menu --- menu 1
     * Handles colour, sound, objects, and environment.
     * Default menu.
     */
    public void HandleColorMenu()
    {
        colorManager.ToggleMenu();
        ambientSoundManager.DisableMenu();
        objectManager.DisableObjectMode();
    }

    public void HandleSoundMenu()
    {
        ambientSoundManager.ToggleMenu();
        colorManager.DisableMenu();
        objectManager.DisableObjectMode();
    }

    public void HandleObjectMenu()
    {
        objectManager.ToggleObjectMode();
        colorManager.DisableMenu();
        ambientSoundManager.DisableMenu();
    }

    public void SwitchMenus(InputAction.CallbackContext context)
    {
        wristMenu1.SetActive(!wristMenu1.activeSelf);
        wristMenu2.SetActive(!wristMenu2.activeSelf);

        if (!wristMenu1.activeSelf)
        {
            // call each individual disable function so that we don't have a menu jumpscare when switching with multiple levels of menu
            colorManager.DisableMenu();
            ambientSoundManager.DisableMenu();
            objectManager.DisableObjectMode();
        }
        else
        {
            dayTimeManager.DisableMenu();
            furnitureManager.DisableMenu();
            petManager.DisableMenu();
        }
    }

    /*
     * Secondary menu --- menu 2
     * Handles time of day, furniture, and animals.
     * Not the default menu.
     */
    public void HandleDayTimeMenu()
    {
        dayTimeManager.ToggleMenu();
        furnitureManager.DisableMenu();
        petManager.DisableMenu();
    }

    public void HandleFurnitureMenu()
    {
        furnitureManager.ToggleMenu();
        dayTimeManager.DisableMenu();
        petManager.DisableMenu();
    }

    public void HandlePetMenu()
    {
        petManager.ToggleMenu();
        dayTimeManager.DisableMenu();
        furnitureManager.DisableMenu();
    }
}
