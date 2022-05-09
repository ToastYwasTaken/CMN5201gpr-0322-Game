using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicSettingsInUI : MonoBehaviour
{
    [Header("Master Mixer")]
    [SerializeField]
    private AudioMixer masterMixer;

    [Header("Master Volume")]
    [SerializeField]
    private Slider masterVolumeSlider;

    [Header("Music Volume")]
    [SerializeField]
    private Slider musicVolumeSlider;

    [Header("SFX Volume")]
    [SerializeField]
    private Slider sfxVolumeSlider;

    private class Volumes
    {
        public int MasterVolume;
        public int MusicVolume;
        public int SFXVolume;
    }

    private Volumes DefaultVolumes = new Volumes
    {
        MasterVolume = 100,
        MusicVolume = 100,
        SFXVolume = 100
    };

    private void Start()
    {
        LoadVariables();
    }

    public void SetMasterVolume(float sliderValue)
    {
        masterMixer.SetFloat("masterVolume", Mathf.Log(sliderValue) * 20);
    }

    public void SetMusicVolume(float sliderValue)
    {
        masterMixer.SetFloat("musicVolume", Mathf.Log(sliderValue) * 20);
    }

    public void SetSFXVolume(float sliderValue)
    {
        masterMixer.SetFloat("sfxVolume", Mathf.Log(sliderValue) * 20);
    }

    public void SaveVariables()
    {
        PlayerPrefs.SetFloat("masterVolume", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolumeSlider.value);
    }

    public void LoadVariables()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    public void ResetToDefault()
    {
        masterVolumeSlider.value = DefaultVolumes.MasterVolume;
        musicVolumeSlider.value = DefaultVolumes.MusicVolume;
        sfxVolumeSlider.value = DefaultVolumes.SFXVolume;
    }
}
