/*****************************************************************************
* Project: TANKPATROL
* File   : UISingleton.cs
* Date   : 11.04.2022
* Author : Dennis Braunmueller (DB)
*
* Singleton for ui.
*
* History:
*	11.04.2022	    DB	    Created
*	13.04.2022      DB      Edited
*   03.06.2022      DB      Edited
******************************************************************************/
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