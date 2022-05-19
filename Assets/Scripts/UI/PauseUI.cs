using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Dennis.UI
{
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

        private WindowController WindowController { get { return WindowController.s_Instance; } }

        // Start is called before the first frame update
        void Start()
        {
            ResumeButton.onClick.AddListener(() => WindowController.OnBack());
            SettingsButton.onClick.AddListener(OpenSettingsWindow);
            ExitButton.onClick.AddListener(ExitToMainMenu);

            PauseWindow.OnDisableAction += OnDisablePauseWindow;
        }

        // Update is called once per frame
        void Update()
        {
            if(WindowController.CurrentWindow == null && !PauseWindow.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
            {
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
            if(WindowController.HasWindowHistory)
            {
                return;
            }

            SetTimeScale(1f);
        }

        void OpenSettingsWindow()
        {
            WindowController.OpenWindow(SettingsWindow);
        }

        void ExitToMainMenu()
        {
            SetTimeScale(1f);

            SceneManager.LoadScene(0);
        }
    }
}