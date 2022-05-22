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
        private GameObject loadingScreen;
        [SerializeField]
        private Slider loadingBar;
        [SerializeField]
        private TMP_Text loadingText;

        private void Start()
        {
            loadingScreen.SetActive(false);
        }

        public void LoadScene(int sceneIndex)
        {
            StartCoroutine(LoadAsynchronously(sceneIndex));
        }

        private IEnumerator LoadAsynchronously(int sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

            loadingScreen.SetActive(true);

            while(!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);

                loadingBar.value = progress;
                loadingText.text = progress * 100f + "%";

                yield return null;
            }
        }
    }
}