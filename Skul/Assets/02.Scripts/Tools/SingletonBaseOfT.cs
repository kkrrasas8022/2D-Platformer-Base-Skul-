using System;
using System.Reflection;//런타임중에 어셈블리 등의 코드에 접근하는 용도(메타데이터)의 기능을 제공하는 네임스페이스

namespace Skul.Tools
{
    public class SingletonBase<T>
        where T : SingletonBase<T>
    {
        //싱글톤 패턴이 멀티스레드 환경에서 다른 인스턴스를 생성하지 않게 하기위한 구문
        private static readonly object _spinLock = new object();
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_spinLock)
                    {
                        //Activator : 인스턴스를 만드는데 도움을 주는 클래스
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