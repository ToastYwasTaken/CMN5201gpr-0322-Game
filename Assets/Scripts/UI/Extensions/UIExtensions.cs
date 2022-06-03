/*****************************************************************************
* Project: TANKPATROL
* File   : UIExtensions.cs
* Date   : 11.04.2022
* Author : Dennis Braunmueller (DB)
*
* Extensions for ui.
*
* History:
*	11.04.2022	    DB	    Created
*	13.04.2022      DB      Edited
*   03.06.2022      DB      Edited
******************************************************************************/
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