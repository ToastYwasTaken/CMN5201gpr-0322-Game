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
        private LoadingScreenUI loadingScreen;

        private AudioManager _audioManager;

        protected override void Awake()
        {
            _audioManager = GameObject.Find("/AudioManager").GetComponent<AudioManager>();
            _audioManager.PlayMelody(_audioManager.MusicMainMenu);

            _versionText.text = string.Format("VERSION: {0}", Application.version);

            _playButton.onClick.AddListener(Play);
            _settingsButton.onClick.AddListener(Settings);
            _creditsButton.onClick.AddListener(Credits);
            _quitButton.onClick.AddListener(Quit);

            base.Awake();
        }

        private void Play()
        {
            // ToDo: Add logic to start game!
            //SceneManager.LoadScene(1);
            loadingScreen.LoadScene(1);
        }

        private void Settings()
        {
            WindowController.s_Instance.OpenWindow(_settingsWindow);
        }

        private void Credits()
        {
            WindowController.s_Instance.OpenWindow(_creditsWindow);
        }

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