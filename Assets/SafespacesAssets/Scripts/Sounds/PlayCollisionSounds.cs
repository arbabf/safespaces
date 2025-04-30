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
        audioSource.volume = collision.relativeVelocity.magnitude / 20;
        float distance = Vector3.Distance(GameObject.Find("XR Origin (XR Rig)").transform.position, transform.position);
        // custom attenuation hack
        audioSource.volume *= Mathf.Max((30 - distance) / 30, 0);
        audioSource.Play();
    }
}
