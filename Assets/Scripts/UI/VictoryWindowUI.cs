using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.MapGeneration;

namespace Dennis.UI
{
    public class VictoryWindowUI : MonoBehaviour
    {
        [Header("Window")]
        [SerializeField]
        private GameObject VictoryWindow;

        [Header("Button")]
        [SerializeField]
        private Button RestartButton;
        [SerializeField]
        private Button ExitButton;

        [Header("Misc")]
        [SerializeField]
        private LoadingScreenUI loadingScreen;

        private LevelGenerator _levelGenerator;

        private void Start()
        {
            RestartButton.onClick.AddListener(Restart);
            ExitButton.onClick.AddListener(ExitToMainMenu);
        }

        /// <summary>
        /// Show victory window.
        /// </summary>
        public void ShowVictoryWindow()
        {
            SetTimeScale(0f);

            GlobalValues.sCurrentLevel++;
            GlobalValues.sIsPlayerActive = false;

            VictoryWindow.SetActive(true);
        }

        /// <summary>
        /// Set the time scale.
        /// </summary>
        void SetTimeScale(float scale)
        {
            Time.timeScale = scale;
        }

        /// <summary>
        /// Restart the current scene.
        /// </summary>
        void Restart()
        {
            SetTimeScale(1f);

            loadingScreen.LoadScene(1);
        }

        /// <summary>
        /// Exit to MainMenu scene.
        /// </summary>
        void ExitToMainMenu()
        {
            SetTimeScale(1f);
            _levelGenerator = GameObject.Find("/LevelGenerator").GetComponent<LevelGenerator>();
            _levelGenerator.ClearLevel();
            loadingScreen.LoadScene(0);
        }
    }
}

