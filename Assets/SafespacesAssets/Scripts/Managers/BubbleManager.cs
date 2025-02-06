using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class BubbleManager : MonoBehaviour
{
    public GameObject bubble;
    public GameObject controller;
    public Outline buttonOutline;

    private InputAction bubbleAction;
    private Transform bubbleLaunchOrigin;
    private InputAction bubbleModeAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputActionAsset inputActions = GameObject.Find("XR Origin (XR Rig)").GetComponent<InputActionManager>().actionAssets[0];
        bubbleAction = inputActions.FindActionMap("XRI Left Interaction").FindAction("Activate");
        bubbleModeAction = inputActions.FindActionMap("XRI Left Interaction").FindAction("Primary Button");
        bubbleModeAction.performed += EnableBubbleMode;
        bubbleModeAction.canceled += DisableBubbleMode;
        bubbleLaunchOrigin = controller.transform.Find("Poke Interactor");
    }

    void StartBubbleStream(InputAction.CallbackContext context)
    {
        InvokeRepeating(nameof(CreateBubble), 0.0f, 0.1f);
    }

    void StopBubbleStream(InputAction.CallbackContext context)
    {
        CancelInvoke();
    }

    void CreateBubble()
    {
        GameObject b = Instantiate(bubble, bubbleLaunchOrigin.position, Quaternion.identity);
        b.GetComponent<Rigidbody>().AddRelativeForce(bubbleLaunchOrigin.forward * 0.1f, ForceMode.Impulse);
        b.transform.localScale *= Random.Range(0.2f, 1.0f);
        Destroy(b, Random.Range(3, 6));
    }

    public void EnableBubbleMode(InputAction.CallbackContext context)
    {
        bubbleAction.performed += StartBubbleStream;
        bubbleAction.canceled += StopBubbleStream;
    }

    public void DisableBubbleMode(InputAction.CallbackContext context)
    {
        bubbleAction.performed -= StartBubbleStream;
        bubbleAction.canceled -= StopBubbleStream;
    }
}
