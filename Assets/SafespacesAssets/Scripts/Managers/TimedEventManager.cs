using TMPro;
using UnityEngine;

public class TimedEventManager : MonoBehaviour
{
    int sleepTick;
    public TextMeshPro sleepText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sleepTick = 0;
        InvokeRepeating("AnimateSleepText", 0, 0.5f);
    }

    void AnimateSleepText()
    {
        if (sleepTick >= 3)
            sleepTick = 0;

        sleepText.text = "zzz" + new string('.', ++sleepTick);
    }
}
