using UnityEngine;

public class AudioZone : MonoBehaviour
{
    private LayerMask theCollider;
    public AudioSource audio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            audio.Play();
            audio.loop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            audio.Stop();
            audio.loop = false;
        }
    }

}
