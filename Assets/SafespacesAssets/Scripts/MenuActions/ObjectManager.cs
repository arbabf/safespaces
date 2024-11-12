using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ObjectManager : MonoBehaviour
{
    private GameObject selectedObject;
    private InputActionMap normalMap;
    private InputActionMap objectMap;
    private InputAction objectAction;
    private ObjectSpawner objSpawner;
    
    public XRRayInteractor interactor;

    bool objectModeEnabled = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputActionAsset inputActions = GameObject.Find("XR Origin (XR Rig)").GetComponent<InputActionManager>().actionAssets[0];
        normalMap = inputActions.FindActionMap("XRI Left Interaction");
        objectMap = inputActions.FindActionMap("XRI Left Interaction (Object Mode)");
        objectAction = objectMap.FindAction("CreateObject");
        objSpawner = gameObject.GetComponent<ObjectSpawner>();
    }

    private void OnDestroy()
    {
        if (objectModeEnabled)
            ToggleObjectMode();
    }

    // create an object at wherever our left controller is pointing
    public void CreateObject(InputAction.CallbackContext context)
    {
        selectedObject = objSpawner.objectPrefabs[0]; // probs change this if the user wants random spawns

        interactor.TryGetCurrent3DRaycastHit(out RaycastHit raycast);
        Vector3 spawnLocation = raycast.point;
        // move our object back so it doesn't spawn inside a collider
        Vector3 extents = selectedObject.GetComponent<Collider>().bounds.extents;
        spawnLocation += raycast.normal.Multiply(extents);
        objSpawner.TrySpawnObject(spawnLocation, raycast.normal);
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
