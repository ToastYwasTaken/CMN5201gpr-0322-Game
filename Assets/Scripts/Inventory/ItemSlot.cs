using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private RectTransform _rectTransform;
    public InvSlot SlotData { get; set; }
    public ItemDragDrop ItemDD { get; set; }
    public RectTransform RectTransform { get { return _rectTransform; } }

    //private void Awake()
    //{
    //    Slot = new InvSlot() { SlotIndex = _slotIndex, SlotType = SlotType };
    //}

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData != null)
        {
            ItemDragDrop item = eventData.pointerDrag.GetComponent<ItemDragDrop>();

            if(item != null)
                if(RefLib.sInventory.MoveItem(item._currentSlot, SlotData))
                {
                    item.IsDropSuccessful = true;
                    item.RectTransform.anchoredPosition = _rectTransform.anchoredPosition;
                }
        }
    }

    //public void UpdateInventory(ItemDragDrop item)
    //{
    //    if (RefLib.sInventory.MoveItem2(item._currentSlot, SlotData))
    //        item._currentSlot = SlotData;

    //    else item.ResetPosition();
    //}
}
