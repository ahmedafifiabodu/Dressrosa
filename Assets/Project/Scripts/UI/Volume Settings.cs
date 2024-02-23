using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSidler;
    [SerializeField] private Slider SFXSidler;
    [SerializeField] private Slider dialogSidler;

    private void Start() => Checker();

    public void SetMusicVolume()
    {
        float volume = musicSidler.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSidler.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetDialogVolume()
    {
        float volume = dialogSidler.value;
        myMixer.SetFloat("Dialog", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("DialogVolume", volume);
    }

    private void LoadVolume()
    {
        musicSidler.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXSidler.value = PlayerPrefs.GetFloat("SFXVolume");
        dialogSidler.value = PlayerPrefs.GetFloat("DialogVolume");

        SetMusicVolume();
        SetSFXVolume();
        SetDialogVolume();
    }

    private void Checker()
    {
        if (PlayerPrefs.HasKey("MusicVolume")) LoadVolume();
        else
        {
            SetMusicVolume();
            SetSFXVolume();
            SetDialogVolume();
        }
    }
}