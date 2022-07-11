/*****************************************************************************
* Project: TANKPATROL
* File   : MainWindowUI.cs
* Date   : 11.04.2022
* Author : Dennis Braunmueller (DB)
*
* MainMenu logic.
*
* History:
*	11.04.2022	    DB	    Created
*   13.04.2022      DB      Edited
*   24.05.2022      DB      Edited
*   03.06.2022      DB      Edited
******************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace Dennis.UI
{    
     /// <summary>
     /// Changelog:
     /// -------------------------
     /// Franz: added Music pausing / unpausing
     /// Deactivated Quit
     /// </summary>
    public class MainWindowUI : WindowWithAnimator
    {
        [Header("UI Buttons")]
        [SerializeField]
        private Button _playButton;
        [SerializeField]
        private Button _settingsButton;
        [SerializeField]
        private Button _creditsButton;
        [SerializeField]
        private Button _quitButton;

        [Header("UI Windows")]
        [SerializeField]
        private Window _settingsWindow;
        [SerializeField]
        private Window _creditsWindow;

        [Header("Misc")]
        [SerializeField]
        private TMP_Text _versionText;
        [SerializeField]
        private LoadingScreenUI _loadingScreen;

        private AudioManager _audioManager;

        protected override void Awake()
        {
            _audioManager = GameObject.Find("/AudioManager").GetComponent<AudioManager>();
            _audioManager.PlayMelody(_audioManager.MusicMainMenu);

            _versionText.text = string.Format("VERSION: {0}", Application.version);

            _playButton.onClick.AddListener(Play);
            _settingsButton.onClick.AddListener(Settings);
            _creditsButton.onClick.AddListener(Credits);
            //_quitButton.onClick.AddListener(Quit);

            base.Awake();
        }

        /// <summary>
        /// Play the game.
        /// </summary>
        private void Play()
        {
            _loadingScreen.LoadScene(1);
        }

        /// <summary>
        /// Switch to settings window.
        /// </summary>
        private void Settings()
        {
            WindowController.s_Instance.OpenWindow(_settingsWindow);
        }

        /// <summary>
        /// Switch to credits window.
        /// </summary>
        private void Credits()
        {
            WindowController.s_Instance.OpenWindow(_creditsWindow);
        }

        /// <summary>
        /// Quit the game.
        /// </summary>
        private void Quit()
        {
            #if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying = false;

            #else

            Application.Quit();

            #endif
        }
    }
}