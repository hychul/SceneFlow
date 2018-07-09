using SceneFlow;
using UnityEngine;

public class SimpleSceneLoader : MonoBehaviour {

	public void LoadScene(string sceneName) 
	{
		var intent = new SceneParam();
		intent.PutParam("loading_scene", sceneName);
		SceneFlowManager.LoadScene("Loading", sceneParam: intent);
	}

	public void LoadPreviousScene()
	{
		var preScene = SceneFlowManager.GetPreviousScene();
		
		var intent = new SceneParam();
		intent.PutParam("loading_scene", preScene);
		SceneFlowManager.LoadScene("Loading", sceneParam: intent, stack: false);
	}
}
