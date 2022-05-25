using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

namespace Dennis.UI
{
    public class SettingsUI : MonoBehaviour
    {
        [Header("Fullscreen")]
        [SerializeField]
        private Toggle fullscreenToggle;

        [Header("V-Sync")]
        [SerializeField]
        private Toggle vSyncToggle;

        [Header("Quality")]
        [SerializeField]
        private TMP_Dropdown qualityDropdown;

        [Header("AudioMixer")]
        [SerializeField]
        private AudioMixer masterMixer;

        [Header("Master Volume")]
        [SerializeField]
        private Slider masterVolumeSlider;
        [SerializeField]
        private TMP_Text masterPercentText;

        [Header("SFX Volume")]
        [SerializeField]
        private Slider sfxVolumeSlider;
        [SerializeField]
        private TMP_Text sfxPercentText;

        [Header("Music Volume")]
        [SerializeField]
        private Slider musicVolumeSlider;
        [SerializeField]
        private TMP_Text musicPercentText;

        private void Start()
        {
            LoadValues();
        }

        public void SetFullscreen()
        {
            if(fullscreenToggle.isOn)
            {
                Screen.fullScreen = true;
            }
            else
            {
                Screen.fullScreen = false;
            }
        }

        public void SetVSync()
        {
            if(vSyncToggle.isOn)
            {
                QualitySettings.vSyncCount = 1;
            }
            else
            {
                QualitySettings.vSyncCount = 0;
            }
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
            PlayerPrefs.SetInt("QualityIndex", qualityIndex);
        }

        public void SetMasterVolume(float sliderValue)
        {
            masterMixer.SetFloat("MasterVolume", Mathf.Log(sliderValue) * 20);
            PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
            masterPercentText.text = Mathf.RoundToInt(sliderValue * 100) + "%";
        }

        public void SetSFXVolume(float sliderValue)
        {
            masterMixer.SetFloat("SFXVolume", Mathf.Log(sliderValue) * 20);
            PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
            sfxPercentText.text = Mathf.RoundToInt(sliderValue * 100) + "%";
        }

        public void SetMusicVolume(float sliderValue)
        {
            masterMixer.SetFloat("MusicVolume", Mathf.Log(sliderValue) * 20);
            PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
            musicPercentText.text = Mathf.RoundToInt(sliderValue * 100) + "%";
        }

        private void LoadValues()
        {
            fullscreenToggle.isOn = Screen.fullScreen;

            if(QualitySettings.vSyncCount == 0)
            {
                vSyncToggle.isOn = false;
            }
            else
            {
                vSyncToggle.isOn = true;
            }

            qualityDropdown.value = PlayerPrefs.GetInt("QualityIndex", 5);

            masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1);
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        }
    }
}