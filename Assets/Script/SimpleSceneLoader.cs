using SceneFlow;
using UnityEngine;

public class SimpleSceneLoader : MonoBehaviour {

	public void LoadScene(string sceneName) 
	{
		var param = new SceneParam();
		param.PutParam("loading_scene", sceneName);
		SceneFlowManager.PushCurrentScene();
		SceneFlowManager.LoadScene("Loading", sceneParam: param);
	}

	public void LoadPreviousScene()
	{
		var preScene = SceneFlowManager.PopPreviousScene();
		
		if (preScene == string.Empty)
		{
			Debug.Log("No previous scene");
			return;
		}

		var param = new SceneParam();
		param.PutParam("loading_scene", preScene);
		
		SceneFlowManager.LoadScene("Loading", sceneParam: param);
	}
}
