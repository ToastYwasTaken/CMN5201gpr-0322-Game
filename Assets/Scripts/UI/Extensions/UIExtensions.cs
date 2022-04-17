using System;
using UnityEngine;

namespace Dennis.UI.Extensions
{
    public static class UIExtensions
    {
        public static void SafeInvoke(this Action action)
        {
            if(action != null)
            {
                action.Invoke();
            }
        }

        public static void SetActive(this Component obj, bool value)
        {
            obj.gameObject.SetActive(value);
        }
    }
}