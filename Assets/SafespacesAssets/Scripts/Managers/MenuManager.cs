using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private ColorManager colorManager;
    private AmbientSoundManager ambientSoundManager;
    private ObjectManager objectManager;
    private BubbleManager bubbleManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorManager = GameObject.Find("ColorManager").GetComponent<ColorManager>();
        ambientSoundManager = GameObject.Find("AmbientSoundManager").GetComponent<AmbientSoundManager>();
        objectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
        bubbleManager = GameObject.Find("BubbleManager").GetComponent<BubbleManager>();
    }

    public void HandleColorMenu()
    {
        colorManager.ToggleMenu();
        ambientSoundManager.DisableMenu();
        objectManager.DisableObjectMode();
        bubbleManager.DisableBubbleMode();
    }

    public void HandleSoundMenu()
    {
        ambientSoundManager.ToggleMenu();
        colorManager.DisableMenu();
        objectManager.DisableObjectMode();
        bubbleManager.DisableBubbleMode();
    }

    public void HandleObjectMenu()
    {
        objectManager.ToggleObjectMode();
        colorManager.DisableMenu();
        ambientSoundManager.DisableMenu();
        bubbleManager.DisableBubbleMode();
    }

    public void HandleBubbleMode()
    {
        bubbleManager.ToggleBubbleMode();
        colorManager.DisableMenu();
        ambientSoundManager.DisableMenu();
        objectManager.DisableObjectMode();
    }
}
