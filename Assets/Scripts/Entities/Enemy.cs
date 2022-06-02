using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EntityStats
{
    [SerializeField] private LootTable _lootTable;
    [SerializeField] private GameObject _itemPrefab;

    [SerializeField] private LootTable _pickupTable;
    [SerializeField] private GameObject _pickupPrefab;

    protected override void Death()
    {
        DropLoot(_lootTable);
        DropPickupables(_pickupTable);
        Destroy(transform.parent.parent.parent.gameObject);
    }

    protected virtual void DropLoot(LootTable lootTable)
    {
        if (_lootTable == null) return;

        Item droppedLoot = _lootTable.DetermineLoot();
        if (droppedLoot != null) CreateItem(droppedLoot, _itemPrefab, transform);
    }

    GameObject CreateItem(Item item, GameObject itemPrefab, Transform position)
    {
        GameObject newItem = Instantiate(itemPrefab, position);
        newItem.GetComponent<ItemContainer>().SetupItem(item);

        return newItem;
    }

    protected virtual void DropPickupables(LootTable pickupTable)
    {
        if (_pickupTable == null) return;

        Item droppedLoot = _pickupTable.DetermineLoot();
        if(droppedLoot != null) CreatePickup(droppedLoot, _itemPrefab, transform);
    }

    GameObject CreatePickup(Item item, GameObject itemPrefab, Transform position)
    {
        GameObject newItem = Instantiate(itemPrefab, position);
        newItem.GetComponent<PickupableContainer>().SetupPickupables((Pickupables)item);

        return newItem;
    }
}
