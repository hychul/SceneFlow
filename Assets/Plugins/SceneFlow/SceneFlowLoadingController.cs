using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneFlow
{
    public abstract class SceneFlowLoadingController : MonoBehaviour
    {
        private const float LOAD_READY_PERCENTAGE = 0.9f;
        private const float LOAD_EPSILON = 0.001f;

        private AsyncOperation sceneLoading;

        public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            sceneLoading = SceneFlowManager.LoadSceneAsync(sceneName, mode: mode);
            sceneLoading.allowSceneActivation = false;
            
            StartCoroutine(LoadingSceneProgress());
        }

        private IEnumerator LoadingSceneProgress()
        {
            while (LOAD_EPSILON < Math.Abs(LOAD_READY_PERCENTAGE - sceneLoading.progress))
            {
                OnProgressUpdate(sceneLoading.progress);

                yield return null;
            }
            
            OnProgressEnd();
        }

        protected abstract void OnProgressUpdate(float progress);

        protected abstract void OnProgressEnd();

        protected void AllowSceneActivation()
        {
            if (sceneLoading == null)
                return;
            
            sceneLoading.allowSceneActivation = true;
        }
    }
}
