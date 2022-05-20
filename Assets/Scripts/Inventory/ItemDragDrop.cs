using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Item _item;
    [SerializeField] private Image _image;

    public Canvas Canvas { set { _canvas = value; } }
    public Image Image { get { return _image; } set { _image = value; } }
    public Item Item { get { return _item; } set { _item = value; } }
    public InvSlot _currentSlot { get; set; }
    public bool IsDropSuccessful { private get; set; }
    public eItemType _itemType { get; private set; }
    public RectTransform RectTransform { get { return _rectTransform; } }

    private Vector2 _lastAnchorPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _lastAnchorPosition = _rectTransform.anchoredPosition;
        _canvasGroup.blocksRaycasts = false;
        IsDropSuccessful = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData) 
    {
        _canvasGroup.blocksRaycasts = true;
        if (!IsDropSuccessful) ResetPosition(); 
    }
    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void ResetPosition()
    {
        _rectTransform.anchoredPosition = _lastAnchorPosition;
    }
}
public struct InvSlot
{
    public int SlotIndex;
    public eItemType SlotType;
}
