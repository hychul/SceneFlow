using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneFlow
{
	public class SceneFlowManager
	{
		private const int MAX_STACK_SIZE = 5;

		private static readonly Stack<string> sceneStack = new Stack<string>();

		private static string cachedScene;

		public static void LoadScene(string sceneName, string viaScene = null)
		{
			PushCurrentScene();

			if (viaScene == null)
			{
				SceneManager.LoadScene(sceneName);
			}
			else
			{
				cachedScene = sceneName;
				SceneManager.LoadScene(viaScene);
			}
		}

		public static void LoadPreviousScene(string viaScene = null)
		{
			if (sceneStack.Empty)
			{
				Debug.LogError("Previous scene is NOT EXIST");
				return;
			}

			if (viaScene == null)
			{
				SceneManager.LoadScene(sceneStack.PopLast());				
			}
			else
			{
				cachedScene = sceneStack.PopLast();
				SceneManager.LoadScene(viaScene);
			}
		}

		public static void LoadCachedScene()
		{
			if (string.IsNullOrEmpty(cachedScene))
			{
				Debug.LogError("Cached scene is NOT EXIST");
				return;
			}

			var sceneName = cachedScene;
			cachedScene = string.Empty;
			
			SceneManager.LoadScene(sceneName);
		}

		public static AsyncOperation LoadSceneAsync(string sceneName, string viaScene = null)
		{
			PushCurrentScene();

			if (viaScene == null)
				return SceneManager.LoadSceneAsync(sceneName);
			
			cachedScene = sceneName;
			return SceneManager.LoadSceneAsync(viaScene);
		}

		public static AsyncOperation LoadPreviousSceneAsync(string viaScene = null)
		{
			if (sceneStack.Empty)
			{
				Debug.LogError("Previous scene is NOT EXIST");
				return null;
			}
			
			if (viaScene == null)
				return SceneManager.LoadSceneAsync(sceneStack.PopLast());
			
			cachedScene = sceneStack.PopLast();
			return SceneManager.LoadSceneAsync(viaScene);
		}

		public static AsyncOperation LoadCachedSceneAsync()
		{
			if (string.IsNullOrEmpty(cachedScene))
			{
				Debug.LogError("Cached scene is NOT EXIST");
				return null;
			}

			var sceneName = cachedScene;
			cachedScene = string.Empty;
			
			return SceneManager.LoadSceneAsync(sceneName);
		}

		private static void PushCurrentScene()
		{
			sceneStack.PushLast(SceneManager.GetActiveScene().name);

			if (MAX_STACK_SIZE < sceneStack.Count)
				sceneStack.PopFirst();
		}
	}

	public class Stack<T>
	{
		private LinkedList<T> list;

		public bool Empty
		{
			get { return list.Count < 1; }
		}

		public int Count
		{
			get { return list.Count; }
		}

		public Stack()
		{
			list = new LinkedList<T>();
		}

		public void PushLast(T element)
		{
			list.AddLast(element);
		}

		public T PopLast()
		{
			var element = list.Last.Value;
			list.RemoveLast();
			return element;
		}

		public void PushFirst(T element)
		{
			list.AddFirst(element);
		}

		public T PopFirst()
		{
			var element = list.First.Value;
			list.RemoveFirst();
			return element;
		}

		public override string ToString()
		{
			var builder = new StringBuilder();
			builder.Append(string.Format("Count : {0}\n", list.Count));
			for (var i = 0; i < list.Count; i++)
				builder.Append(string.Format("{0} : {1} \n", i, list.ElementAt(i)));

			return builder.ToString();
		}
	}
}
