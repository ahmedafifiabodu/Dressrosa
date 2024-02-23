using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip[] footstepSounds; 
    public float stepInterval = 0.5f; 
    private AudioSource audioSource;
    private Vector3 lastPosition; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, lastPosition) > 0.01f)
        {
            PlayFootstepSound();
            lastPosition = transform.position;
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void PlayFootstepSound()
    {
        if (!audioSource.isPlaying)
        {
      
            AudioClip footstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.clip = footstepSound;
            audioSource.Play();
        }
    }
}
