using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject room;
    public GameObject pond;

    public XRRayInteractor interactor;

    private InputAction moveAction;
    private InputAction cancelAction;

    private bool roomActive;

    private GameObject attachedObject;
    private Vector3 origPosition;
    private Quaternion origRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roomActive = true;
        room.SetActive(true);
        pond.SetActive(false);

        InputActionAsset inputActions = GameObject.Find("XR Origin (XR Rig)").GetComponent<InputActionManager>().actionAssets[0];
        moveAction = inputActions.FindActionMap("XRI Right Interaction").FindAction("Activate");
        cancelAction = inputActions.FindActionMap("XRI Right Interaction").FindAction("Secondary Button");
        moveAction.performed += MoveObject;
    }

    void Update()
    {
        if (attachedObject)
        {
            interactor.TryGetCurrent3DRaycastHit(out RaycastHit raycast);

            // did this hit the floor?
            if (raycast.normal == Vector3.up)
            {
                attachedObject.transform.position = raycast.point;
                Vector3 angles = attachedObject.transform.eulerAngles;
                angles.y = interactor.rayOriginTransform.eulerAngles.y;
                attachedObject.transform.eulerAngles = angles;
            }
        }
    }

    public void ToggleEnvironment()
    {
        roomActive = !roomActive;
        room.SetActive(roomActive);
        pond.SetActive(!roomActive);
    }

    public void MoveObject(InputAction.CallbackContext context)
    {
        if (attachedObject)
        {
            DetachObject();
        }
        else
        {
            interactor.TryGetCurrent3DRaycastHit(out RaycastHit raycast);
            if (raycast.transform)
            {
                GameObject gameObject = raycast.transform.root.gameObject;
                if (gameObject)
                {
                    XRSimpleInteractable obj = gameObject.GetComponent<XRSimpleInteractable>();
                    if (obj && (obj.interactionLayers.value & 1 << 2) > 0)
                    {
                        AttachObject(obj);
                    }
                }
            }
        }
    }

    public void CancelMove(InputAction.CallbackContext context)
    {
        if (attachedObject)
        {
            attachedObject.transform.position = origPosition;
            attachedObject.transform.rotation = origRotation;
            DetachObject();
        }
    }

    // Attach an object to our ray interactor for moving capability.
    public void AttachObject(XRSimpleInteractable interactable)
    {
        attachedObject = interactable.gameObject;

        origPosition = attachedObject.transform.position;
        origRotation = attachedObject.transform.rotation;

        cancelAction.performed += CancelMove;

        foreach (Renderer renderer in attachedObject.gameObject.GetComponentsInChildren<Renderer>())
        {
            if (renderer)
            {
                foreach (Material mat in renderer.materials)
                {
                    if (mat.HasColor("_BaseColor"))
                    {
                        // make this object transparent
                        mat.SetFloat("_Surface", 1);

                        Color c = mat.GetColor("_BaseColor");
                        c.a = 0.5f;
                        mat.SetColor("_BaseColor", c);

                        mat.SetFloat("_Blend", 0.0f);
                        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                        mat.SetInt("_ZWrite", 0);
                        mat.DisableKeyword("_ALPHATEST_ON");
                        mat.EnableKeyword("_ALPHABLEND_ON");
                        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                        mat.renderQueue = 3000;
                    }
                }
            }
        }

        // disable raycasts on this object so it doesn't get hit by the ray interactor when moving this object
        int layer = LayerMask.NameToLayer("Ignore Raycast");
        attachedObject.layer = layer;
        foreach (Transform child in attachedObject.transform)
        {
            child.gameObject.layer = layer;
        }
    }

    public void DetachObject()
    {
        foreach (Renderer renderer in attachedObject.gameObject.GetComponentsInChildren<Renderer>())
        {
            if (renderer)
            {
                foreach (Material mat in renderer.materials)
                {
                    if (mat.HasColor("_BaseColor"))
                    {
                        // make this object opaque
                        mat.SetFloat("_Surface", 0.0f);

                        Color c = mat.GetColor("_BaseColor");
                        c.a = 1f;
                        mat.SetColor("_BaseColor", c);

                        mat.SetFloat("_Blend", 0.0f);
                        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                        mat.SetInt("_ZWrite", 1);
                        mat.DisableKeyword("_ALPHATEST_ON");
                        mat.DisableKeyword("_ALPHABLEND_ON");
                        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                        mat.renderQueue = -1;
                    }
                }
            }
        }

        int layer = LayerMask.NameToLayer("Default");
        attachedObject.layer = layer;
        foreach (Transform child in attachedObject.transform)
        {
            child.gameObject.layer = layer;
        }

        attachedObject = null;

        cancelAction.performed -= CancelMove;
    }
}
