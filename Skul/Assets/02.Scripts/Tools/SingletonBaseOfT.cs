using System;
using System.Reflection;

namespace Skul.Tools
{
    public class SingletonBase<T>
        where T : SingletonBase<T>
    {
        private static readonly object _spinLock = new object();
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_spinLock)
                    {
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