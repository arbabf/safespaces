using UnityEngine;
using UnityEngine.UI;

public class DayTimeManager : MonoBehaviour
{
    public Light dlight;
    public GameObject dayTimeMenu;
    public Outline buttonOutline;

    enum DayTimes
    {
        DAY_DAWN,
        DAY_NOON,
        DAY_DUSK,
        DAY_NIGHT
    }

    void Start()
    {
        DisableMenu();
    }

    public void ToggleMenu()
    {
        if (dayTimeMenu.activeSelf)
            DisableMenu();
        else
            EnableMenu();
    }

    public void EnableMenu()
    {
        dayTimeMenu.SetActive(true);
        buttonOutline.enabled = true;
    }

    public void DisableMenu()
    {
        dayTimeMenu.SetActive(false);
        buttonOutline.enabled = false;
    }

    public void SetTimeOfDay(int dayPhase)
    {
        Quaternion rot = Quaternion.Euler(0,0,0);

        switch ((DayTimes)dayPhase)
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
