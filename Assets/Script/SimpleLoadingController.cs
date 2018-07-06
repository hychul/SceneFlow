using System.Collections;
using System.Collections.Generic;
using SceneFlow;
using UnityEngine;
using UnityEngine.UI;

public class SimpleLoadingController : SceneFlowLoadingController
{
    private const string LOADING_TEXT = "LOADING...";
    private const string LOADED_TEXT = "TAP TO CONTINUE";
	
    [SerializeField] private Text txtLoading;
    [SerializeField] private Slider sldrLoading;
	
    protected override void OnInitialize()
    {
        txtLoading.text = LOADING_TEXT;
    }
	
    protected override void OnProgressUpdate(float progress)
    {
        sldrLoading.value = progress;
		
        if (progress < 1f)
            txtLoading.text = LOADING_TEXT;
        else
        {
            txtLoading.text = LOADED_TEXT;
			
            if (Input.GetMouseButtonDown(0))
                AllowSceneActivation();
        }
    }
}
