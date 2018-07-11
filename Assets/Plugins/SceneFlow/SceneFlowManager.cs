using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneFlow
{
	public class SceneFlowManager
	{
		private static readonly Stack<string> _sceneStack = new Stack<string>();
		private static int _stackSize = 5;

		private static SceneParam _sceneParam;

		public static SceneParam GetParam()
		{
			return _sceneParam;
		}
		
		public static void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single, SceneParam sceneParam = null)
		{
			_sceneParam = sceneParam;

			SceneManager.LoadScene(sceneName, mode);
		}

		public static AsyncOperation LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single, SceneParam sceneParam = null)
		{
			_sceneParam = sceneParam;

			return SceneManager.LoadSceneAsync(sceneName, mode);
		}

		public static void SetSceneStackSize(int size)
		{
			_stackSize = Math.Max(0, size);

			var margin = _sceneStack.Count - _stackSize;
			for (var i = 0; i < margin; i++)
				_sceneStack.PopFirst();
		}

		public static string PopPreviousScene()
		{
			return _sceneStack.Empty ? string.Empty : _sceneStack.PopLast();
		}

		public static void PushCurrentScene()
		{
			_sceneStack.PushLast(SceneManager.GetActiveScene().name);

			if (_stackSize < _sceneStack.Count)
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
