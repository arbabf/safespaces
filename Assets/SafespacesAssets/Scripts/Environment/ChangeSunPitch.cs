using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class ChangeSunPitch : MonoBehaviour
{
    public Light dlight;
    public Slider volumeSlider;
    public void SetSunPitch()
    {
        dlight.transform.localEulerAngles = new Vector3(volumeSlider.value,
           dlight.transform.localEulerAngles.y, dlight.transform.localEulerAngles.z);
    }
}
