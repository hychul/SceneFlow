using System;
using System.Collections.Generic;
using UnityEngine;

namespace SceneFlow
{
    public class SceneParam
    {
        private Dictionary<string, Value> pairByName;
		
        public SceneParam()
        {
            pairByName = new Dictionary<string, Value>();
        }

        public void PutParam(string name, object value)
        {
            if (pairByName.ContainsKey(name))
            {
                Debug.LogWarning("SceneParam overwrote a extra value with the same key.");
                pairByName.Remove(name);
            }

            pairByName.Add(name, new Value(value));
        }

        public T GetParam<T>(string name)
        {
            if (!pairByName.ContainsKey(name))
                return default(T);

            var pair = pairByName[name];
            if (typeof(T) != pair.type)
                return default(T);
			
            return (T) pair.value;
        }
		
        private sealed class Value
        {
            public Type type;
            public object value;
			
            public Value(object value)
            {
                this.type = value.GetType();
                this.value = value;
            }
        }   
    }
}