using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ObjectManager : MonoBehaviour
{
    private GameObject selectedObject;
    private InputActionMap normalMap;
    private InputActionMap objectMap;
    private InputAction objectAction;
    

    public GameObject ballTemplate;
    public GameObject blockTemplate;
    public XRRayInteractor interactor;

    bool objectModeEnabled = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectedObject = ballTemplate;
        InputActionAsset inputActions = GameObject.Find("XR Origin (XR Rig)").GetComponent<InputActionManager>().actionAssets[0];
        normalMap = inputActions.FindActionMap("XRI Left Interaction");
        objectMap = inputActions.FindActionMap("XRI Left Interaction (Object Mode)");

        objectAction = objectMap.FindAction("CreateObject");
    }

    private void OnDestroy()
    {
        if (objectModeEnabled)
            ToggleObjectMode();
    }

    // create an object at wherever our left controller is pointing
    public void CreateObject(InputAction.CallbackContext context)
    {
        interactor.TryGetCurrent3DRaycastHit(out RaycastHit raycast);
        Vector3 spawnLocation = raycast.point;
        // move our object back so it doesn't spawn inside a collider
        Vector3 extents = selectedObject.GetComponent<Collider>().bounds.extents;
        spawnLocation += raycast.normal.Multiply(extents);
        Instantiate(selectedObject, spawnLocation, Quaternion.LookRotation(raycast.normal));
    }

    public void ToggleObjectMode()
    {
        objectModeEnabled = !objectModeEnabled;
        Debug.Log(objectModeEnabled);
        if (!objectModeEnabled)
        {
            normalMap.Enable();
            objectMap.Disable();
            objectAction.performed -= CreateObject;
        }
        else
        {
            normalMap.Disable();
            objectMap.Enable();
            objectAction.performed += CreateObject;
        }
    }
}
