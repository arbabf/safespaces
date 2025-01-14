using UnityEngine;

public class HandleBlinds : MonoBehaviour
{
    public void ToggleBlinds()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = !gameObject.GetComponent<MeshRenderer>().enabled;
    }
}
