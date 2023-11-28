using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Tools
{
    public class SingletonMonoBase<T> : MonoBehaviour
        where T : SingletonMonoBase<T>
    {
        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);
        }
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    T resources = Resources.Load<T>(typeof(T).Name);

                    if (resources)
                    {
                        _instance = Instantiate(resources);
                        DontDestroyOnLoad(_instance);
                    }
                    else
                    {
                        _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                        
                    }
                }
                return _instance;
            }
        }

        private static T _instance;
    }
}