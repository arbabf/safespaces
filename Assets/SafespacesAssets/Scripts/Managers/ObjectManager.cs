using System;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ObjectManager : MonoBehaviour
{
    private GameObject selectedObject;
    private InputAction objectAction;
    private ObjectSpawner objSpawner;
    
    public XRRayInteractor interactor;
    public GameObject objectMenu;
    public Outline buttonOutline;

    int objIndex = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputActionAsset inputActions = GameObject.Find("XR Origin (XR Rig)").GetComponent<InputActionManager>().actionAssets[0];
        objectAction = inputActions.FindActionMap("XRI Left Interaction").FindAction("Activate");
        objSpawner = gameObject.GetComponent<ObjectSpawner>();
        objectMenu.SetActive(false);
    }

    private void OnDestroy()
    {
        DisableObjectMode();
    }

    // create/destroy an object at wherever our left controller is pointing
    public void ObjectModeAction(InputAction.CallbackContext context)
    {
        if (objIndex >= objSpawner.objectPrefabs.Count)
            return;

        if (objIndex == -1)
        {
            // destroy object
            interactor.TryGetCurrent3DRaycastHit(out RaycastHit raycast);
            Rigidbody rb = raycast.rigidbody;
            if (rb)
            {
                // fixme: this will destroy *anything* with a rigidbody attached to it.
                // what we really want is to be able to only destroy the spawnable physics objects
                Destroy(rb.gameObject);
            }
        }
        else
        {
            // create object
            selectedObject = objSpawner.objectPrefabs[objIndex]; // probs change this if the user wants random spawns
            objSpawner.spawnOptionIndex = objIndex;

            interactor.TryGetCurrent3DRaycastHit(out RaycastHit raycast);
            Vector3 spawnLocation = raycast.point;
            // move our object back so it doesn't spawn inside a collider
            Vector3 extents = selectedObject.GetComponent<Collider>().bounds.extents;
            spawnLocation += raycast.normal.Multiply(extents);
            objSpawner.TrySpawnObject(spawnLocation, raycast.normal);
        }
    }

    public void ToggleObjectMode()
    {
        if (objectMenu.activeSelf)
            DisableObjectMode();
        else
            EnableObjectMode();
    }

    public void EnableObjectMode()
    {
        objectMenu.SetActive(true);
        objectAction.performed += ObjectModeAction;
        buttonOutline.enabled = true;
    }

    public void DisableObjectMode()
    {
        objectMenu.SetActive(false);
        objIndex = -1;
        objectAction.performed -= ObjectModeAction;
        buttonOutline.enabled = false;

        // disable all outlines
        for (int i = 1; i < objectMenu.transform.childCount; i++)
        {
            objectMenu.transform.GetChild(i).GetComponent<Outline>().enabled = false;
        }
    }

    public void SetObjectIndex(int index)
    {
        // whatever we do we'll need to disable an outline
        if (objIndex != -1)
            objectMenu.transform.GetChild(objIndex + 1).GetComponent<Outline>().enabled = false;

        if (objIndex == index)
        {
            // deselect object
            objIndex = -1;
        }
        else
        {         
            // select object
            objectMenu.transform.GetChild(index + 1).GetComponent<Outline>().enabled = true;
            objIndex = index;
        }
    }
}
