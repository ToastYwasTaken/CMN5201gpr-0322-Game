/*****************************************************************************
* Project: TANKPATROL
* File   : LoadingScreenUI.cs
* Date   : 16.05.2022
* Author : Dennis Braunmueller (DB)
*
* Loads the scenes asynchronously.
*
* History:
*	16.05.2022	    DB	    Created
*	18.05.2022      DB      Edited
*	23.05.2022      DB      Edited
******************************************************************************/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace Dennis.UI
{
    public class LoadingScreenUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _loadingScreen;
        [SerializeField]
        private Slider _loadingBar;
        [SerializeField]
        private TMP_Text _loadingText;

        private AudioManager _audioManager;

        private void Start()
        {
            _loadingScreen.SetActive(false);
            _audioManager = GameObject.Find("/AudioManager")?.GetComponent<AudioManager>();
        }

        /// <summary>
        /// Loads a scene with an index.
        /// </summary>
        public void LoadScene(int sceneIndex)
        {
            StartCoroutine(LoadAsynchronously(sceneIndex));
            //Start game melody when done loading
            _audioManager.ChangeMelody(_audioManager.MusicLevel);
        }

        /// <summary>
        /// Coroutine for loading a scene.
        /// </summary>
        private IEnumerator LoadAsynchronously(int sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

            _loadingScreen.SetActive(true);

            while(!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);

                _loadingBar.value = progress;
                _loadingText.text = progress * 100f + "%";

                yield return null;
            }
        }
    }
}