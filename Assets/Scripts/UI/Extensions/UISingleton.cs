using UnityEngine;

namespace Dennis.UI.Extensions
{
    public abstract class UISingleton<T> : MonoBehaviour where T : UISingleton<T>
    {
        [SerializeField]
        private bool _dontDestroyOnLoad;

        private static T s_instance;

        public static T s_Instance { get { return s_instance; } }

        private void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this as T;

                if (_dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }

                AwakeSingleton();
            }
            else
            {
                Destroy(gameObject.GetComponent<T>());
            }
        }

        protected abstract void AwakeSingleton();
    }
}