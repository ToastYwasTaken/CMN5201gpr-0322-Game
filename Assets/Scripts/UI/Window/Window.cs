/*****************************************************************************
* Project: TANKPATROL
* File   : Window.cs
* Date   : 11.04.2022
* Author : Dennis Braunmueller (DB)
*
* Normal window without animations.
*
* History:
*	11.04.2022	    DB	    Created
*	13.04.2022      DB      Edited
*   24.05.2022      DB      Edited
*   03.06.2022      DB      Edited
******************************************************************************/
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