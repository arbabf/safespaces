using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ObjectManager : MonoBehaviour
{
    private GameObject selectedObject;
    private InputAction objectAction;
    private ObjectSpawner objSpawner;
    
    public XRRayInteractor interactor;

    int objIndex = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputActionAsset inputActions = GameObject.Find("XR Origin (XR Rig)").GetComponent<InputActionManager>().actionAssets[0];
        objectAction = inputActions.FindActionMap("XRI Left Interaction").FindAction("Activate");
        objSpawner = gameObject.GetComponent<ObjectSpawner>();
    }

    private void OnDestroy()
    {
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

    public void HandleObjectMode()
    {
        objIndex += 1;
        if (objIndex >= objSpawner.objectPrefabs.Count)
        {
            DisableObjectMode();
        }
        objectAction.performed += CreateObject;
        Debug.Log(objIndex);
    }

    public void DisableObjectMode()
    {
        objIndex = -1;
        objectAction.performed -= CreateObject;
    }
}
