using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class ChangeSunPitch : MonoBehaviour
{
    public Light dlight;
    public Slider volumeSlider;

    Vector3 rot = Vector3.zero;
    float oldValue;

    void Start()
    {
        oldValue = volumeSlider.value;
    }
    public void SetSunPitch()
    {
        rot.x = (volumeSlider.value - oldValue);
        dlight.transform.Rotate(rot, Space.World);
    }
}
