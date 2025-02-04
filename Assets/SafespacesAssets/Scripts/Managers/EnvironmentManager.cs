using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject room;
    public GameObject pond;

    public XRRayInteractor interactor;

    private InputAction moveAction;
    private bool roomActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roomActive = true;
        room.SetActive(true);
        pond.SetActive(false);

        InputActionAsset inputActions = GameObject.Find("XR Origin (XR Rig)").GetComponent<InputActionManager>().actionAssets[0];
        moveAction = inputActions.FindActionMap("XRI Right Interaction").FindAction("Activate");
        moveAction.performed += MoveObject;
    }

    public void ToggleEnvironment()
    {
        roomActive = !roomActive;
        room.SetActive(roomActive);
        pond.SetActive(!roomActive);
    }

    public void MoveObject(InputAction.CallbackContext context)
    {

    }
}
