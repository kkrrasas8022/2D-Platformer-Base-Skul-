using System.Collections;
using System.Collections.Generic;
using Skul.UI;
using UnityEngine;

namespace Skul.Tools
{
    //����Ƽ���� ���Ǵ� Monobehaviour�� ��ӹ޴� �̱���
    //���� ������Ʈ ������ ��ü�μ� �����ϴ� ��(ex : inputManager)���� �����Ų��
    public class SingletonUIBase<T> : UIMono
    where T : SingletonUIBase<T>
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