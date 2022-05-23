using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] int _inventorySize;
    public int InvSize { get; private set; }
    private int _wpnCount;
    private int _chipCount;

    WeaponManager _wpnManager;
    OverdriveManager _overdriveManager;
    [SerializeField] GameObject _canvasPfab;
    [SerializeField] Transform _canvas;
    Canvas _canvasComp;

    [SerializeField] GameObject _itemDDpFab;
    [SerializeField] ItemSlot[] _itemSlots;

    public ItemSlot[] ItemSlots { get { return _itemSlots; } }


    private void Awake()
    {
        RefLib.sInventory = this;

        _wpnManager = GetComponent<WeaponManager>();
        _overdriveManager = GetComponent<OverdriveManager>();
        _wpnCount = 2;//_wpnManager.WeaponsSlots.Length;
        _chipCount = 1; //_overdriveManager.OverdriveSlots.Length;
        InvSize = _inventorySize;

        if (_canvas == null)
        {
            SetupCanvas();
        }
        _canvasComp = _canvas.GetComponent<Canvas>();

        if (_itemSlots.Length != InvSize + _wpnCount + _chipCount) print("wrong invSize...guess");
        //_itemSlots = new ItemSlot[InvSize + _wpnCount + _ovdrCount];
        //for(int i = 0; i < _itemSlots.Length; i++)
        //{
        //    _itemSlots[i] = new ItemSlot();
        //}
        if (_itemSlots.Length<=0) return;
        InitSlots(eItemType.ALL, InvSize);
        InitSlots(eItemType.WEAPON, _wpnCount);
        InitSlots(eItemType.CHIP, _chipCount);

        AddSlotImages();
        SwitchShowInv();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchShowInv();
        }
    }
    List<Image> _allImages = new List<Image>();
    bool _showAllImages = true;

    void AddSlotImages()
    {
        foreach (var item in _itemSlots)
            _allImages.Add(item.gameObject.GetComponent<Image>());
    }
    void SwitchShowInv()
    {
        foreach(Image image in _allImages)
            image.enabled = !_showAllImages;
        _showAllImages = !_showAllImages;
    }

    void SetupCanvas()
    {
        GameObject canvas = Instantiate(_canvasPfab);
        canvas.transform.SetParent(null);
        _itemSlots = canvas.GetComponentsInChildren<ItemSlot>();
        _canvas = canvas.transform;
    }

    [SerializeField] bool isColorize;
    int slotIndexCounter = 0;
    void InitSlots(eItemType type, int slotNum)
    {
        for (int i = 0; i < slotNum; i++) 
        {
            _itemSlots[slotIndexCounter].SlotData = 
                new InvSlot() { SlotIndex = slotIndexCounter, SlotType = type };

            if(isColorize)
            {
                _itemSlots[slotIndexCounter].gameObject.GetComponent<Image>().color = TypeColor(type);
                _itemSlots[slotIndexCounter].gameObject.name = type.ToString() + "slot" + slotIndexCounter.ToString();
            }
            slotIndexCounter++;
        }
    }

    Color TypeColor(eItemType type)
    {
        switch (type)
        {
            case eItemType.ALL:
                return Color.white;
            case eItemType.WEAPON:
                return Color.red;
            case eItemType.CHIP:
                return Color.blue;
            default:
                return Color.black;
        }
    }

    public bool PickupItem(Item item, Sprite sprite, Color color, eItemType type)
    {
        int slot = 0;
        while (_itemSlots[slot].ItemDD != null)
        {
            slot++;
            if (slot >= InvSize)
            {
                //InventoryFull();
                return false;
            }
        }
        GameObject newItemDD = Instantiate(_itemDDpFab);
        newItemDD.transform.SetParent(_canvas);
        ItemDragDrop itemDD = newItemDD.GetComponent<ItemDragDrop>();

        itemDD.Item = item;
        itemDD.Image.sprite = sprite;
        itemDD.Image.color = color;
        itemDD.Canvas = _canvasComp;
        itemDD._currentSlot = _itemSlots[slot].SlotData;
        itemDD.RectTransform.anchoredPosition = 
            _itemSlots[slot].RectTransform.anchoredPosition;
        itemDD._itemType = type;
        _itemSlots[slot].ItemDD = itemDD;

        _allImages.Add(newItemDD.GetComponent<Image>());
        newItemDD.GetComponent<Image>().enabled = _showAllImages;
        return true;
    }

    public bool MoveItem(InvSlot slotFrom, InvSlot slotTo)
    {
        ItemDragDrop itemDDfrom = _itemSlots[slotFrom.SlotIndex].ItemDD;
        if (itemDDfrom == null || (itemDDfrom._itemType != slotTo.SlotType 
            && slotTo.SlotType != eItemType.ALL)) 
            return false;

        ItemDragDrop itemDDto = _itemSlots[slotTo.SlotIndex].ItemDD;
        if(itemDDto != null)
            if (itemDDto._itemType != slotFrom.SlotType && slotFrom.SlotType != eItemType.ALL)
                return false;

        itemDDfrom._currentSlot = slotTo;
        if(itemDDto!=null)
        {
            itemDDto._currentSlot = slotFrom;
            itemDDto.RectTransform.anchoredPosition = 
                ItemSlots[slotFrom.SlotIndex].RectTransform.anchoredPosition;
        }

        _itemSlots[slotFrom.SlotIndex].ItemDD = itemDDto;
        _itemSlots[slotTo.SlotIndex].ItemDD = itemDDfrom;


        if(slotTo.SlotType != eItemType.ALL)
            if(!EquipItem(itemDDfrom, slotTo.SlotIndex))
                return false;
        if (itemDDto != null)
            if (slotFrom.SlotType != eItemType.ALL)
                if(!EquipItem(itemDDto, slotFrom.SlotIndex))
                    return false;

        return true;
    }

    public bool EquipItem(ItemDragDrop itemDD, int slot)
    {
        switch (itemDD._itemType)
        {
            case eItemType.WEAPON:
                Weapon wpn = itemDD.Item as Weapon;
                if (wpn == null) return false;
                if (!_wpnManager.EquipWeapon(wpn, slot - InvSize)) 
                    return false;
                return true;

            case eItemType.CHIP:
                OverdriveChip chip = itemDD.Item as OverdriveChip;
                if (chip == null) return false;
                if (!_overdriveManager.EquipOverdriveChip(chip, slot - InvSize - _wpnCount)) 
                    return false;
                return true;

                //ShootBehaviour
            default:
                return false;
        }
    }
    public bool DiscardItem(InvSlot slot)
    {
        switch (slot.SlotType)
        {
            case eItemType.ALL:
                break;

            case eItemType.WEAPON:
                _wpnManager.WeaponsSlots[slot.SlotIndex - InvSize] = null;
                break;

            case eItemType.CHIP:
                _overdriveManager.OverdriveSlots[slot.SlotIndex - InvSize- _wpnCount] = null;
                break;

            default:
                return false;

        }
        ItemDragDrop item = _itemSlots[slot.SlotIndex].ItemDD;
        _itemSlots[slot.SlotIndex].ItemDD = null;
        _allImages.Remove(item.gameObject.GetComponent<Image>());
        Destroy(item);
        return true;
    }
}

