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

		private static readonly Stack<string> _sceneStack = new Stack<string>();

		private static SceneParam _sceneParam;
		
		public static void LoadScene(string sceneName, SceneParam sceneParam = null, bool stack = true)
		{
			if (stack)
				PushCurrentScene();
			
			_sceneParam = sceneParam;

			SceneManager.LoadScene(sceneName);
		}

		public static AsyncOperation LoadSceneAsync(string sceneName, SceneParam sceneParam = null, bool stack = true)
		{
			if (stack)
				PushCurrentScene();
			
			_sceneParam = sceneParam;

			return SceneManager.LoadSceneAsync(sceneName);
		}

		public static string GetPreviousScene()
		{
			if (_sceneStack.Empty)
				return string.Empty;

			return _sceneStack.PopLast();
		}

		public static SceneParam GetParam()
		{
			return _sceneParam;
		}

		private static void PushCurrentScene()
		{
			_sceneStack.PushLast(SceneManager.GetActiveScene().name);

			if (MAX_STACK_SIZE < _sceneStack.Count)
				_sceneStack.PopFirst();
		}

		private class Stack<T>
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
}
