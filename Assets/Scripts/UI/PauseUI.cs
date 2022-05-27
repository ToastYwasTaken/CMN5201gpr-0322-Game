using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        private Window PauseWindow;
        [SerializeField]
        private Window SettingsWindow;

        [Header("Buttons")]
        [SerializeField]
        private Button ResumeButton;
        [SerializeField]
        private Button SettingsButton;
        [SerializeField]
        private Button ExitButton;

        [Header("Misc")]
        [SerializeField]
        private TMP_Text versionText;
        [SerializeField]
        private LoadingScreenUI loadingScreen;

        private AudioManager _audioManager;
        public bool IsPaused = false;

        private WindowController WindowController { get { return WindowController.s_Instance; } }

        // Start is called before the first frame update
        void Start()
        {
            if(GameObject.Find("/AudioManager") != null)
            {
            _audioManager = GameObject.Find("/AudioManager").GetComponent<AudioManager>();
            }else _audioManager = new AudioManager();

            versionText.text = string.Format("VERSION: {0}", Application.version);

            ResumeButton.onClick.AddListener(() => WindowController.OnBack());
            SettingsButton.onClick.AddListener(OpenSettingsWindow);
            ExitButton.onClick.AddListener(ExitToMainMenu);
            PauseWindow.OnDisableAction += OnDisablePauseWindow;
        }

        // Update is called once per frame
        void Update()
        {
            if (WindowController.CurrentWindow == null && !PauseWindow.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
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
            PauseWindow.OnDisableAction -= OnDisablePauseWindow;
        }

        void SetTimeScale(float scale)
        {
            Time.timeScale = scale;
        }

        void Show()
        {
            WindowController.OpenWindow(PauseWindow);
            SetTimeScale(0f);
        }

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
            SetTimeScale(1f);
        }

        void OpenSettingsWindow()
        {
            WindowController.OpenWindow(SettingsWindow);
        }

        void ExitToMainMenu()
        {
            IsPaused = false;
            SetTimeScale(1f);

            loadingScreen.LoadScene(0);
        }
    }
}