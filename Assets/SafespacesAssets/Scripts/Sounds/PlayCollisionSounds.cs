using UnityEngine;

public class BounceSound : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        audioSource.volume = collision.relativeVelocity.magnitude / 10;
        audioSource.Play();
    }
}
