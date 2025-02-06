using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private ColorManager colorManager;
    private AmbientSoundManager ambientSoundManager;
    private ObjectManager objectManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorManager = GameObject.Find("ColorManager").GetComponent<ColorManager>();
        ambientSoundManager = GameObject.Find("AmbientSoundManager").GetComponent<AmbientSoundManager>();
        objectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
    }

    public void HandleColorMenu()
    {
        colorManager.ToggleMenu();
        ambientSoundManager.DisableMenu();
        objectManager.DisableObjectMode();
    }

    public void HandleSoundMenu()
    {
        ambientSoundManager.ToggleMenu();
        colorManager.DisableMenu();
        objectManager.DisableObjectMode();
    }

    public void HandleObjectMenu()
    {
        objectManager.ToggleObjectMode();
        colorManager.DisableMenu();
        ambientSoundManager.DisableMenu();
    }

    public void HandleBubbleMode()
    {
        colorManager.DisableMenu();
        ambientSoundManager.DisableMenu();
        objectManager.DisableObjectMode();
    }
}
