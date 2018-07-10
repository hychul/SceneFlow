using System.Collections;
using UnityEngine;

namespace SceneFlow
{
    public abstract class SceneFlowLoadingController : MonoBehaviour
    {
        private const float LOADING_DELAY = 0.3f;
        
        private const float LOAD_READY_PERCENTAGE = 0.9f;

        private AsyncOperation sceneLoading;

        private void Awake()
        {
            OnInitialize();
        }

        protected abstract void OnInitialize();

        public void LoadScene(string sceneName)
        {
            sceneLoading = SceneFlowManager.LoadSceneAsync(sceneName);
            sceneLoading.allowSceneActivation = false;
            
            StartCoroutine(LoadingSceneProgress());
        }

        IEnumerator LoadingSceneProgress()
        {
            yield return new WaitForSeconds(LOADING_DELAY);
            
            while (!sceneLoading.isDone)
            {
                if (LOAD_READY_PERCENTAGE <= sceneLoading.progress)
                    OnProgressUpdate(1f);
                else
                    OnProgressUpdate(sceneLoading.progress);

                yield return null;
            }
        }

        protected abstract void OnProgressUpdate(float progress);

        protected void AllowSceneActivation()
        {
            if (sceneLoading == null)
                return;
            
            sceneLoading.allowSceneActivation = true;
        }
    }
}
