using UnityEngine;

public class StartSFX : MonoBehaviour
{
    [SerializeField] private bool _isWaterAudioPlaying = false;
    [SerializeField] private bool _isFireAudioPlaying = false;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;

        if (_isWaterAudioPlaying)
            audioManager.PlayWater(audioManager.water);

        if (_isFireAudioPlaying)
            audioManager.PlayFire(audioManager.fire);
    }
}