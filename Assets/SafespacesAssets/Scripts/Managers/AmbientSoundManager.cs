using System;
using UnityEngine;
using UnityEngine.UI;

public class AmbientSoundManager : MonoBehaviour
{
    public GameObject soundMenu;
    public GameObject volumeSliderMenu;
    public Outline buttonOutline;
    public AudioSource[] sounds;
    public Slider volumeSlider;

    private AudioSource selectedSound;
    private int selectedIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectedSound = null;
        selectedIndex = -1;
        soundMenu.SetActive(false);
        volumeSliderMenu.SetActive(false);
    }

    public void ToggleMenu()
    {
        if (soundMenu.activeSelf)
            DisableMenu();
        else
            EnableMenu();
    }

    public void EnableMenu()
    {
        soundMenu.SetActive(true);
        buttonOutline.enabled = true;
    }

    public void DisableMenu()
    {
        soundMenu.SetActive(false);
        volumeSliderMenu.SetActive(false);

        // deselect our sound
        if (selectedIndex != -1)
            soundMenu.transform.GetChild(selectedIndex + 1).GetComponent<Outline>().effectColor = SafespacesUtils.cyan;
        selectedSound = null;
        selectedIndex = -1;

        buttonOutline.enabled = false;
    }

    /*
     * There's probably a better way to handle both toggling an AudioSource
     * and its respective Button, but I can't really think of one right now.
     */
    public void ToggleSound(int index)
    {
        bool soundEnabled = sounds[index].isPlaying;
        if (soundEnabled && index == selectedIndex)
        {
            // double click a sound button to disable it
            selectedSound.Stop();
            selectedSound = null;
            selectedIndex = -1;
            volumeSliderMenu.SetActive(false);
            soundMenu.transform.GetChild(index + 1).GetComponent<Outline>().enabled = false;
        }
        else
        {
            // otherwise play it/bring up its menu
            selectedSound = sounds[index];
            if (!soundEnabled)
            {
                selectedSound.Play();
            }
            selectedIndex = index;

            // open up and pre-set the volume slider
            volumeSliderMenu.SetActive(true);
            volumeSlider.value = selectedSound.volume;

            Outline o = soundMenu.transform.GetChild(index + 1).GetComponent<Outline>();
            o.enabled = true;
            o.effectColor = SafespacesUtils.green;

            // set outlines
            // fixme: cache outlines so we don't need use GetComponent()?
            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].isPlaying)
                {
                    if (i != index)
                        soundMenu.transform.GetChild(i + 1).GetComponent<Outline>().effectColor = SafespacesUtils.cyan;
                }
            }
        }
    }

    public void SetAudioVolume()
    {
        selectedSound.volume = volumeSlider.value;
    }
}
