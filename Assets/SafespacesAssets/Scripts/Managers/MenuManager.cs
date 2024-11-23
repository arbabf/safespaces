using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private ColourManager colourManager;
    private AmbientSoundManager ambientSoundManager;
    private ObjectManager objectManager;
    private BubbleManager bubbleManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colourManager = GameObject.Find("ColourManager").GetComponent<ColourManager>();
        ambientSoundManager = GameObject.Find("AmbientSoundManager").GetComponent<AmbientSoundManager>();
        objectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
        bubbleManager = GameObject.Find("BubbleManager").GetComponent<BubbleManager>();
    }

    public void HandleColourMenu()
    {
        colourManager.ToggleColourPicker();
        ambientSoundManager.DisableMenu();
        objectManager.DisableObjectMode();
        bubbleManager.DisableBubbleMode();
    }

    public void HandleSoundMenu()
    {
        ambientSoundManager.ToggleMenu();
        colourManager.DisableColourPicker();
        objectManager.DisableObjectMode();
        bubbleManager.DisableBubbleMode();
    }

    public void HandleObjectMenu()
    {
        objectManager.ToggleObjectMode();
        colourManager.DisableColourPicker();
        ambientSoundManager.DisableMenu();
        bubbleManager.DisableBubbleMode();
    }

    public void HandleBubbleMode()
    {
        bubbleManager.ToggleBubbleMode();
        colourManager.DisableColourPicker();
        ambientSoundManager.DisableMenu();
        objectManager.DisableObjectMode();
    }
}
