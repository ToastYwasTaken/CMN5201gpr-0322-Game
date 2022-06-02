using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EntityStats
{
    protected override void Death()
    {
        DropLoot(_lootTable);
        Destroy(transform.parent.parent.parent.gameObject);
    }

    [SerializeField] private LootTable _lootTable;
    [SerializeField] private GameObject _itemPrefab;
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
}
