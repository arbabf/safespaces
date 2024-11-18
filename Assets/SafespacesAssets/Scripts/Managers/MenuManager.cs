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
        ambientSoundManager = GameObject.Find("AmbientSounds").GetComponent<AmbientSoundManager>();
        objectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
        bubbleManager = GameObject.Find("BubbleManager").GetComponent<BubbleManager>();
    }

    public void HandleColourMenu()
    {
        colourManager.ToggleColourPicker();
        objectManager.DisableObjectMode();
    }

    public void HandleSoundMenu()
    {
        ambientSoundManager.PlayAudioSource("aa");
    }

    public void HandleObjectMenu()
    {
        objectManager.HandleObjectMode();
        colourManager.DisableColourPicker();
    }

    public void HandleBubbleMode()
    {
        bubbleManager.CreateBubble();
    }
}
