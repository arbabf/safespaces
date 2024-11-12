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

    int objIndex = -1;

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
        if (objIndex > -1)
            DisableObjectMode();
    }

    // create an object at wherever our left controller is pointing
    public void CreateObject(InputAction.CallbackContext context)
    {
        selectedObject = objSpawner.objectPrefabs[objIndex]; // probs change this if the user wants random spawns
        objSpawner.spawnOptionIndex = objIndex;

        interactor.TryGetCurrent3DRaycastHit(out RaycastHit raycast);
        Vector3 spawnLocation = raycast.point;
        // move our object back so it doesn't spawn inside a collider
        Vector3 extents = selectedObject.GetComponent<Collider>().bounds.extents;
        spawnLocation += raycast.normal.Multiply(extents);
        objSpawner.TrySpawnObject(spawnLocation, raycast.normal);
    }

    public void EnableObjectMode()
    {
        normalMap.Disable();
        objectMap.Enable();
        objectAction.performed += CreateObject;
    }

    public void DisableObjectMode()
    {
        normalMap.Enable();
        objectMap.Disable();
        objectAction.performed -= CreateObject;
    }

    public void HandleObjectMode()
    {
        objIndex += 1;
        if (objIndex >= objSpawner.objectPrefabs.Count)
        {
            objIndex = -1;
        }
        Debug.Log(objIndex);
        if (objIndex == -1)
        {
            DisableObjectMode();
        }
        else
        {
            EnableObjectMode();
        }
    }
}
