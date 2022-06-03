/*****************************************************************************
* Project: TANKPATROL
* File   : PauseUI.cs
* Date   : 09.05.2022
* Author : Dennis Braunmueller (DB)
*
* Handles the pause mechanic of the game.
*
* History:
*	09.05.2022	    DB	    Created
*	15.05.2022      DB      Edited
*	24.05.2022      DB      Edited
*	02.06.2022      DB      Edited
******************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.MapGeneration;

namespace Dennis.UI
{
    /// <summary>
    /// Changelog:
    /// -------------------------
    /// Franz: added Music pausing / unpausing in menu
    /// </summary>
    public class PauseUI : MonoBehaviour
    {
        [Header("Windows")]
        [SerializeField]
        private Window _pauseWindow;
        [SerializeField]
        private Window _settingsWindow;
        [SerializeField]
        private GameObject _playerUI;

        [Header("Buttons")]
        [SerializeField]
        private Button _resumeButton;
        [SerializeField]
        private Button _settingsButton;
        [SerializeField]
        private Button _exitButton;

        [Header("Misc")]
        [SerializeField]
        private TMP_Text _versionText;
        [SerializeField]
        private LoadingScreenUI _loadingScreen;

        private AudioManager _audioManager;
        public bool IsPaused = false;

        private LevelGenerator _levelGenerator;

        private WindowController WindowController { get { return WindowController.s_Instance; } }

        // Start is called before the first frame update
        void Start()
        {
            if(GameObject.Find("/AudioManager") != null)
            {
            _audioManager = GameObject.Find("/AudioManager").GetComponent<AudioManager>();
            }else _audioManager = new AudioManager();

            _versionText.text = string.Format("VERSION: {0}", Application.version);

            _resumeButton.onClick.AddListener(() => WindowController.OnBack());
            _settingsButton.onClick.AddListener(OpenSettingsWindow);
            _exitButton.onClick.AddListener(ExitToMainMenu);
            _pauseWindow.OnDisableAction += OnDisablePauseWindow;
        }

        // Update is called once per frame
        void Update()
        {
            if (WindowController.CurrentWindow == null && !_pauseWindow.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
            {
                if (!IsPaused)
                {
                    //Pause Audio when opening Pause menu
                    _audioManager.PauseMelody();
                    IsPaused = true;
                }
                Show();
            }
        }

        void OnDestroy()
        {
            StopAllCoroutines();
            _pauseWindow.OnDisableAction -= OnDisablePauseWindow;
        }

        /// <summary>
        /// Set the time scale.
        /// </summary>
        void SetTimeScale(float scale)
        {
            Time.timeScale = scale;
        }

        /// <summary>
        /// Show the window.
        /// </summary>
        void Show()
        {
            WindowController.OpenWindow(_pauseWindow);

            _playerUI.SetActive(false);

            SetTimeScale(0f);
        }

        /// <summary>
        /// Disable the window.
        /// </summary>
        void OnDisablePauseWindow()
        {
            if (WindowController.HasWindowHistory)
            {
                return;
            }

            //Continue Audio
            if (IsPaused)
            {
                Debug.Log("resuming melody");
                _audioManager.ContinueMelody();
                IsPaused = false;
            }

            _playerUI.SetActive(true);

            SetTimeScale(1f);
        }

        /// <summary>
        /// Open the settings window.
        /// </summary>
        void OpenSettingsWindow()
        {
            WindowController.OpenWindow(_settingsWindow);
        }

        /// <summary>
        /// Exit to main menu.
        /// </summary>
        void ExitToMainMenu()
        {
            IsPaused = false;
            SetTimeScale(1f);
            _levelGenerator = GameObject.Find("/LevelGenerator").GetComponent<LevelGenerator>();
            _levelGenerator.ClearLevel();
            _loadingScreen.LoadScene(0);
        }
    }
}