using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource masterVolume;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource WalkSFXSource;
    [SerializeField] private AudioSource dialogSource;

    [SerializeField] private AudioSource waterSource;
    [SerializeField] private AudioSource fireSource;

    [Header("Audio Clip")]
    [SerializeField] private AudioClip background;

    [SerializeField] internal AudioClip walk;
    [SerializeField] internal AudioClip interact;
    [SerializeField] internal AudioClip timetravel;

    [SerializeField] internal AudioClip dialogSound;
    [SerializeField] internal AudioClip win;

    [SerializeField] internal AudioClip water;
    [SerializeField] internal AudioClip fire;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start() => PlayBackground();

    internal void PlaySFX(AudioClip clip) => SFXSource.PlayOneShot(clip);

    internal void PlayWalkSFX(AudioClip clip)
    {
        WalkSFXSource.clip = clip;
        WalkSFXSource.loop = true;
        WalkSFXSource.Play();
    }

    internal void StopWalkSFX()
    {
        WalkSFXSource.Stop();
        WalkSFXSource.loop = false;
    }

    internal void PlayDialog(AudioClip clip) => dialogSource.PlayOneShot(clip);

    internal void PlayWater(AudioClip clip)
    {
        Debug.Log("Playing water");
        waterSource.clip = clip;
        waterSource.Play();
    }

    internal void PlayFire(AudioClip clip)
    {
        fireSource.clip = clip;
        fireSource.Play();
    }

    internal void PlayBackground()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}