using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skul.Tools
{
    //유니티에서 사용되는 Monobehaviour를 상속받는 싱글톤
    //게임 프로젝트 내에서 객체로서 존재하는 것(ex : inputManager)에게 적용시킨다
    public class SingletonMonoBase<T> : MonoBehaviour
    where T : SingletonMonoBase<T>
    {
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