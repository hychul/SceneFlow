using System.Collections;
using SceneFlow;
using UnityEngine;
using UnityEngine.UI;

public class SimpleLoadingController : SceneFlowLoadingController
{
    private const float LOADING_DELAY = 0.3f;

    private const string LOADING_TEXT = "LOADING...";
    private const string LOADED_TEXT = "TAP TO CONTINUE";
	
    [SerializeField] private Text txtLoading;
    [SerializeField] private Slider sldrLoading;

    private void Awake()
    {
        txtLoading.text = LOADING_TEXT;
    }
    
    private void Start()
    {
        StartCoroutine("DelayLoading", LOADING_DELAY);
    }

    private IEnumerator DelayLoading(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        var param = SceneFlowManager.GetParam();
        LoadScene(param.GetParam<string>("loading_scene"));
    }
	
    protected override void OnProgressUpdate(float progress)
    {
        sldrLoading.value = progress;
        txtLoading.text = LOADING_TEXT;
    }

    protected override void OnProgressEnd()
    {
        sldrLoading.value = 1.0f;
        txtLoading.text = LOADED_TEXT;

        StartCoroutine("TapToContinue");
    }

    private IEnumerator TapToContinue()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
                AllowSceneActivation();

            yield return null;
        }
    }
}
