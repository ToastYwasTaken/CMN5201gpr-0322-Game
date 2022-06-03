/*****************************************************************************
* Project: TANKPATROL
* File   : SelectableAnimationUI.cs
* Date   : 23.05.2022
* Author : Dennis Braunmueller (DB)
*
* Allows a custom animation on a selectable UI object.
*
* History:
*	23.05.2022	    DB	    Created
*	24.05.2022      DB      Edited
******************************************************************************/
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dennis.UI
{
    public class SelectableAnimationUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Animator _anim;

        void Start()
        {
            _anim = GetComponent<Animator>();
        }

        /// <summary>
        /// Plays selected animation when selected.
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            _anim.SetTrigger("Selected");
            _anim.ResetTrigger("Deselected");
        }

        /// <summary>
        /// Plays deselected animation when deselected.
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            _anim.SetTrigger("Deselected");
            _anim.ResetTrigger("Selected");
        }
    }
}

