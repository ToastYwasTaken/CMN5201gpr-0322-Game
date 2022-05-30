using UnityEngine;
using UnityEngine.EventSystems;

namespace Dennis.UI
{
    public class SelectableAnimationUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Animator anim;

        void Start()
        {
            anim = GetComponent<Animator>();
        }

        /// <summary>
        /// Plays selected animation when selected.
        /// </summary>
        public void OnPointerEnter(PointerEventData _eventData)
        {
            anim.SetTrigger("Selected");
            anim.ResetTrigger("Deselected");
        }

        /// <summary>
        /// Plays deselected animation when deselected.
        /// </summary>
        public void OnPointerExit(PointerEventData _eventData)
        {
            anim.SetTrigger("Deselected");
            anim.ResetTrigger("Selected");
        }
    }
}

