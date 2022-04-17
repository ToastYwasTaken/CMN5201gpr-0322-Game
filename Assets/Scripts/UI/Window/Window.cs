using System;
using UnityEngine;
using Dennis.UI.Extensions;

namespace Dennis.UI
{
    public abstract class Window : MonoBehaviour
    {
        public Action OnEnableAction;
        public Action OnDisableAction;

        private void OnEnable()
        {
            OnEnableAction.SafeInvoke();
        }

        private void OnDisable()
        {
            OnDisableAction.SafeInvoke();
        }

        public abstract void Open();
        public abstract void Close();
        protected virtual void Awake() { }
    }
}