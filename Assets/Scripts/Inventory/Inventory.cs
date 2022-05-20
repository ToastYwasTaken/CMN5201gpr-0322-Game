using System;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    [SerializeField] int _inventorySize;
    public int InvSize { get; private set; }
    private int _wpnCount;
    private int _ovdrCount;

    WeaponManager _wpnManager;
    OverdriveManager _overdriveManager;
    [SerializeField] GameObject _canvasPfab;
    [SerializeField] Transform _canvas;
    Canvas _canvasComp;

    [SerializeField] GameObject _itemDDpFab;
    [SerializeField] ItemSlot[] _itemSlots;

    int slotIndexCounter = 0;

    private void Awake()
    {
        RefLib.sInventory = this;

        _wpnManager = GetComponent<WeaponManager>();
        _overdriveManager = GetComponent<OverdriveManager>();
        _wpnCount = 0; // _wpnManager.WeaponsSlots.Length;
        _ovdrCount = 0; // _overdriveManager.OverdriveSlots.Length;
        InvSize = _inventorySize;

        if (_canvas == null)
        {
            GameObject canvas = Instantiate(_canvasPfab);
            canvas.name = "InventoryCanvas";
            _canvas = GameObject.Find("InventoryCanvas").transform;
        }
        _canvasComp = _canvas.GetComponent<Canvas>();

        if (_itemSlots.Length != InvSize + _wpnCount + _ovdrCount) print("wrong invSize...guess");
        //_itemSlots = new ItemSlot[InvSize + _wpnCount + _ovdrCount];
        //for(int i = 0; i < _itemSlots.Length; i++)
        //{
        //    _itemSlots[i] = new ItemSlot();
        //}

        InitSlots(eItemType.ALL, InvSize);
        InitSlots(eItemType.WEAPON, _wpnCount);
        InitSlots(eItemType.CHIP, _ovdrCount);
    }
    void InitSlots(eItemType type, int slotNum)
    {
        for(int i = 0; i < slotNum; i++) 
        {
            _itemSlots[slotIndexCounter].SlotData = 
                new InvSlot() { SlotIndex = slotIndexCounter, SlotType = type };
            slotIndexCounter++;
        }
    }

    public bool PickupItem(Item item, Sprite sprite)
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
        itemDD.Canvas = _canvasComp;
        itemDD.RectTransform.anchoredPosition = _itemSlots[slot].RectTransform.anchoredPosition;

        _itemSlots[slot].ItemDD = itemDD;
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
            if (itemDDto._itemType != slotFrom.SlotType || slotFrom.SlotType != eItemType.ALL)
                return false;

        itemDDfrom._currentSlot = slotTo;
        if(itemDDto!=null)
        {
            itemDDto._currentSlot = slotFrom;
            itemDDto.RectTransform.anchoredPosition = itemDDfrom.RectTransform.anchoredPosition;
        }

        _itemSlots[slotFrom.SlotIndex].ItemDD = itemDDto;
        _itemSlots[slotTo.SlotIndex].ItemDD = itemDDfrom;


        if(slotTo.SlotType != eItemType.ALL)
            EquipItem(itemDDfrom, slotTo.SlotIndex);
        if (itemDDto != null)
            if (slotFrom.SlotType != eItemType.ALL)
                EquipItem(itemDDto, slotFrom.SlotIndex);

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
                OverdriveChip ovdr = itemDD.Item as OverdriveChip;
                if (ovdr == null) return false;
                if (!_overdriveManager.EquipOverdriveChip(ovdr, slot - InvSize - _wpnCount)) 
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
        Destroy(item);
        return true;
    }
}

