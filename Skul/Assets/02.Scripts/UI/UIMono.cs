using UnityEngine;

namespace Skul.UI
{
    public abstract class UIMono : MonoBehaviour, IUI
    {
        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
    }
}