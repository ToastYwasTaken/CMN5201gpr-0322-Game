using System;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public class Inventory
{
    int _inventorySize;
    int _wpnCount;
    public int InvSize { get => _inventorySize; }
    public int WpnCount { get => 2; } //wpnManager.weaponSlotAmount; }

    Item[] _itemSlots;

    WeaponManager _wpnManager;
    //Upgrade slots

    UnityEvent _invChanged; //inventory has changed

    public Inventory(int size, WeaponManager wpnManager)
    {
        _inventorySize = size;
        _wpnManager = wpnManager;

        _invChanged = new UnityEvent();
        _itemSlots = new Item[InvSize];
    }
    public Item GetItem(int slot)
    {
        return _itemSlots[slot];
    }
    public bool PickupItem(Item item)
    {
        int slot = 0;
        while (_itemSlots[slot] != null)
        {
            slot++;
            if (slot >= InvSize)
            {
                InventoryFull();
                return false;
            }
        }
        _itemSlots[slot] = item;
        _invChanged.Invoke();
        return true;
    }
    public bool MoveItem(int slotFrom, int slotTo)
    {
        if(_itemSlots[slotFrom] == null) return false;

        Item itemTemp = _itemSlots[slotTo]; //item/null
        _itemSlots[slotTo] = _itemSlots[slotFrom];
        _itemSlots[slotFrom] = itemTemp;
        
        _invChanged.Invoke();
        return true;
    }
    public bool EquipWeapon(int invSlot, int wpnSlot = 0)
    {
        Weapon wpn = _itemSlots[invSlot] as Weapon;
        if (wpn == null)
        {
            WrongItemType();
            return false;
        }

        Weapon oldWpn = _wpnManager.WeaponsSlots[wpnSlot].WeaponItem;

        _wpnManager.EquipWeapon(wpn, wpnSlot);
        _itemSlots[invSlot] = oldWpn;

        _invChanged.Invoke();
        return true;
    }
    public bool EquipUpgrade(int invSlot, int upgSlot = 0)
    {
        Upgrade upg = _itemSlots[invSlot] as Upgrade;
        if (upg == null) return false;

        //Equip upg

        _itemSlots[invSlot] = null;
        _invChanged.Invoke();
        return true;
    }
    public void DiscardItem(int slot)
    {
        if(_itemSlots[slot] == null) return;
        _invChanged.Invoke();
        _itemSlots[slot] = null;
    }
    
    void InventoryFull()
    {
        Debug.Log("Inventory full");
    }
    void WrongItemType()
    {
        Debug.Log("Wrong ItemType");
    }
}
