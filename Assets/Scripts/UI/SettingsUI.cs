/*****************************************************************************
* Project: TANKPATROL
* File   : SettingsUI.cs
* Date   : 02.05.2022
* Author : Dennis Braunmueller (DB)
*
* Handles all the settings, available in the game.
*
* History:
*	02.05.2022	    DB	    Created
*	03.05.2022      DB      Edited
*	05.05.2022      DB      Edited
*	23.05.2022      DB      Edited
*	02.06.2022      DB      Edited
******************************************************************************/
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
        private Toggle _fullscreenToggle;

        [Header("V-Sync")]
        [SerializeField]
        private Toggle _vSyncToggle;

        [Header("Quality")]
        [SerializeField]
        private TMP_Dropdown _qualityDropdown;

        [Header("AudioMixer")]
        [SerializeField]
        private AudioMixer _masterMixer;

        [Header("Master Volume")]
        [SerializeField]
        private Slider _masterVolumeSlider;
        [SerializeField]
        private TMP_Text _masterPercentText;

        [Header("UI Volume")]
        [SerializeField]
        private Slider _uiVolumeSlider;
        [SerializeField]
        private TMP_Text _uiPercentText;

        [Header("SFX Volume")]
        [SerializeField]
        private Slider _sfxVolumeSlider;
        [SerializeField]
        private TMP_Text _sfxPercentText;

        [Header("Music Volume")]
        [SerializeField]
        private Slider _musicVolumeSlider;
        [SerializeField]
        private TMP_Text _musicPercentText;

        private void Start()
        {
            LoadValues();
        }

        /// <summary>
        /// Set fullscreen.
        /// </summary>
        public void SetFullscreen()
        {
            if(_fullscreenToggle.isOn)
            {
                Screen.fullScreen = true;
            }
            else
            {
                Screen.fullScreen = false;
            }
        }

        /// <summary>
        /// Set V-Sync.
        /// </summary>
        public void SetVSync()
        {
            if(_vSyncToggle.isOn)
            {
                QualitySettings.vSyncCount = 1;
            }
            else
            {
                QualitySettings.vSyncCount = 0;
            }
        }

        /// <summary>
        /// Set the quality level of the game.
        /// </summary>
        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
            PlayerPrefs.SetInt("QualityIndex", qualityIndex);
        }

        /// <summary>
        /// Set the master volume of the game.
        /// </summary>
        public void SetMasterVolume(float sliderValue)
        {
            _masterMixer.SetFloat("MasterVolume", Mathf.Log(sliderValue) * 20);
            PlayerPrefs.SetFloat("MasterVolume", _masterVolumeSlider.value);
            _masterPercentText.text = Mathf.RoundToInt(sliderValue * 100) + "%";
        }

        /// <summary>
        /// Set the ui volume of the game.
        /// </summary>
        public void SetUIVolume(float sliderValue)
        {
            _masterMixer.SetFloat("UIVolume", Mathf.Log(sliderValue) * 20);
            PlayerPrefs.SetFloat("UIVolume", _uiVolumeSlider.value);
            _uiPercentText.text = Mathf.RoundToInt(sliderValue * 100) + "%";
        }
        
        /// <summary>
        /// Set the sfx volume of the game.
        /// </summary>
        public void SetSFXVolume(float sliderValue)
        {
            _masterMixer.SetFloat("SFXVolume", Mathf.Log(sliderValue) * 20);
            PlayerPrefs.SetFloat("SFXVolume", _sfxVolumeSlider.value);
            _sfxPercentText.text = Mathf.RoundToInt(sliderValue * 100) + "%";
        }

        /// <summary>
        /// Set the music volume of the game.
        /// </summary>
        public void SetMusicVolume(float sliderValue)
        {
            _masterMixer.SetFloat("MusicVolume", Mathf.Log(sliderValue) * 20);
            PlayerPrefs.SetFloat("MusicVolume", _musicVolumeSlider.value);
            _musicPercentText.text = Mathf.RoundToInt(sliderValue * 100) + "%";
        }

        /// <summary>
        /// Load all variables.
        /// </summary>
        private void LoadValues()
        {
            _fullscreenToggle.isOn = Screen.fullScreen;

            if(QualitySettings.vSyncCount == 0)
            {
                _vSyncToggle.isOn = false;
            }
            else
            {
                _vSyncToggle.isOn = true;
            }

            _qualityDropdown.value = PlayerPrefs.GetInt("QualityIndex", 5);

            _masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1);
            _uiVolumeSlider.value = PlayerPrefs.GetFloat("UIVolume", 1);
            _sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
            _musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        }
    }
}