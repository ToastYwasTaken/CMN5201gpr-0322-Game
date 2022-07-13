/*****************************************************************************
* Project: TANKPATROL
* File   : VictoryWindowUI.cs
* Date   : 01.05.2022
* Author : Dennis Braunmueller (DB)
*
* Victory window if the player wins.
*
* History:
*	01.05.2022	    DB	    Created
*	24.05.2022      DB      Edited
******************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.MapGeneration;

namespace Dennis.UI
{
    public class VictoryWindowUI : MonoBehaviour
    {
        [Header("Window")]
        [SerializeField]
        private GameObject _victoryWindow;

        [Header("Button")]
        [SerializeField]
        private Button _restartButton;
        [SerializeField]
        private Button _exitButton;

        [Header("Misc")]
        [SerializeField]
        private LoadingScreenUI _loadingScreen;

        private LevelGenerator _levelGenerator;

        private void Start()
        {
            _restartButton.onClick.AddListener(Restart);
            _exitButton.onClick.AddListener(ExitToMainMenu);
        }

        /// <summary>
        /// Show victory window.
        /// </summary>
        public void ShowVictoryWindow()
        {
            SetTimeScale(0f);

            GlobalValues.sCurrentLevel++;
            GlobalValues.sIsPlayerActive = false;

            _victoryWindow.SetActive(true);
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
            _levelGenerator = GameObject.Find("/LevelGenerator").GetComponent<LevelGenerator>();
            _levelGenerator.ClearLevel();
            _loadingScreen.LoadScene(1);
        }

        /// <summary>
        /// Exit to MainMenu scene.
        /// </summary>
        void ExitToMainMenu()
        {
            SetTimeScale(1f);
            _levelGenerator = GameObject.Find("/LevelGenerator").GetComponent<LevelGenerator>();
            _levelGenerator.ClearLevel();
            _loadingScreen.LoadScene(0);
        }
    }
}

