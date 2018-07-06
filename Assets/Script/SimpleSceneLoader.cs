using System.Collections;
using System.Collections.Generic;
using SceneFlow;
using UnityEngine;

public class SimpleSceneLoader : MonoBehaviour {

	public void LoadScene(string sceneName) {
		SceneFlowManager.LoadScene(sceneName, viaScene: "Loading");
	}

	public void LoadPreviousScene() {
		SceneFlowManager.LoadPreviousScene(viaScene: "Loading");
	}
}
