using UnityEngine;
using UnityEngine.UI;

public class ChangeDayTime : MonoBehaviour
{
    public Light dlight;
    public Slider daySlider;

    enum DayTimes
    {
        DAY_DAWN,
        DAY_NOON,
        DAY_DUSK,
        DAY_NIGHT
    }

    public void SetTimeOfDay()
    {
        Quaternion rot = Quaternion.Euler(0,0,0);

        switch ((DayTimes)daySlider.value)
        {
            case DayTimes.DAY_DAWN:
                rot = Quaternion.Euler(5, -75, 0);
                dlight.color = SafespacesUtils.sunriseColor / 255;
                break;
            case DayTimes.DAY_NOON:
                rot = Quaternion.Euler(50, -75, 0);
                dlight.color = SafespacesUtils.noonColor / 255;
                break;
            case DayTimes.DAY_DUSK:
                rot = Quaternion.Euler(5, 75, 0);
                dlight.color = SafespacesUtils.sunsetColor / 255;
                break;
            case DayTimes.DAY_NIGHT:
                rot = Quaternion.Euler(-25, 75, 0);
                dlight.color = SafespacesUtils.nightColor / 255;
                break;
            default:
                break;
        }

        dlight.transform.SetPositionAndRotation(dlight.transform.position, rot);
    }
}
