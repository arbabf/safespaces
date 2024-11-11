using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleWristMenu : MonoBehaviour
{
    public InputActionAsset inputActions;

    private Canvas wristMenuCanvas;
    private InputAction menu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wristMenuCanvas = GetComponent<Canvas>();
        // Change when we start using a proper controller instead of kb/m
        //menu = inputActions.FindActionMap("XRI Left Interaction").FindAction("Menu");
        menu = inputActions.FindActionMap("Controller").FindAction("Menu");
        menu.Enable();
        menu.performed += ToggleMenu;
    }

    private void OnDestroy()
    {
        menu.performed -= ToggleMenu;
    }
    
    public void ToggleMenu(InputAction.CallbackContext wristCallbackContext)
    {
        wristMenuCanvas.enabled = !wristMenuCanvas.enabled;
    }
}
