using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvBackground : MonoBehaviour, IDropHandler
{
    [SerializeField] private RectTransform _rectTransform;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData != null)
        {
            ItemDragDrop item = eventData.pointerDrag.GetComponent<ItemDragDrop>();
            if (item != null)
                Destroy(item.gameObject);
        }
    }
}
