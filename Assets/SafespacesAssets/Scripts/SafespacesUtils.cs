using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// by @kurtdekker - to make a simple Unity singleton that has no
// predefined data associated with it, eg, a high score manager.
//
// To use: access with SingletonSimple.Instance
//
// To set up:
//	- Copy this file (duplicate it)
//	- rename class SingletonSimple to your own classname
//	- rename CS file too
//
// DO NOT PUT THIS IN ANY SCENE; this code auto-instantiates itself once.
//
// I do not recommend subclassing unless you really know what you're doing.

public class SafespacesUtils : MonoBehaviour
{
    // This is really the only blurb of code you need to implement a Unity singleton
    private static SafespacesUtils _Instance;
    public static SafespacesUtils Instance
    {
        get
        {
            if (!_Instance)
            {
                _Instance = new GameObject().AddComponent<SafespacesUtils>();
                // name it for easy recognition
                _Instance.name = _Instance.GetType().ToString();
                // mark root as DontDestroyOnLoad();
                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }

    public static Color cyan = new(0, 255, 50, 255); // enabled colour
    public static Color green = new(0, 255, 0, 255); // selected colour

    public static Color sunriseColor = new (255, 161, 108);
    public static Color noonColor = new(255, 244, 214);
    public static Color sunsetColor = new(255, 147, 107);
    public static Color nightColor = new(100, 150, 209);

    public static void SetOutlineAlpha(Outline o, float a)
    {
        o.effectColor = new(o.effectColor.r, o.effectColor.g, o.effectColor.b, a);
    }
}