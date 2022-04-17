using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Dennis.UI
{
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

        protected override void Awake()
        {
            _versionText.text = string.Format("Version: {0}", Application.version);

            _playButton.onClick.AddListener(Play);
            _settingsButton.onClick.AddListener(Settings);
            _creditsButton.onClick.AddListener(Credits);
            _quitButton.onClick.AddListener(Quit);

            base.Awake();
        }

        private void Play()
        {
            // ToDo: Add logic to start game!
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