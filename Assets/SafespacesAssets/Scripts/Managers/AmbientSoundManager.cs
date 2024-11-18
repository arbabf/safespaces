using UnityEngine;
using UnityEngine.UI;

public class AmbientSoundManager : MonoBehaviour
{
    public Button[] soundButtons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundButtons[0].onClick.AddListener(delegate { PlayAudioSource("e"); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO: we already have all our AudioSources that exist and all that; we do not need to find an AudioSource by name.
    // The better way would be to link each AudioSource child in our prefab to an element in an array, and link our buttons
    // to that array.
    // For now, as a PoC, this is fine.
    public void PlayAudioSource(string sourceName)
    {
        AudioSource source = transform.Find("AmbientFan").GetComponent<AudioSource>();
        source.Play();
    }

    public void StopAudioSource(string sourceName)
    {

    }
}
