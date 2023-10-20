using System;
using System.Reflection;//��Ÿ���߿� ����� ���� �ڵ忡 �����ϴ� �뵵(��Ÿ������)�� ����� �����ϴ� ���ӽ����̽�

namespace Skul.Tools
{
    public class SingletonBase<T>
        where T : SingletonBase<T>
    {
        //�̱��� ������ ��Ƽ������ ȯ�濡�� �ٸ� �ν��Ͻ��� �������� �ʰ� �ϱ����� ����
        private static readonly object _spinLock = new object();
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_spinLock)
                    {
                        //Activator : �ν��Ͻ��� ����µ� ������ �ִ� Ŭ����
                        _instance = Activator.CreateInstance<T>();
                        _instance.Init();
                    }
                }
                return _instance;
            }
        }
        private static T _instance;

        protected virtual void Init() { }
    }
}