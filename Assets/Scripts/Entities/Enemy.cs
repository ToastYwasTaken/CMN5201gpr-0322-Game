using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EntityStats
{

    [Header("Spawn Loot")]
    [SerializeField] private LootTable _lootTable;
    [SerializeField] private GameObject _itemPrefab;

    [SerializeField] private LootTable _pickupTable;
    [SerializeField] private GameObject _pickupPrefab;

    [SerializeField] private Vector3 _offset = Vector3.zero;
    [SerializeField] private Vector3 _randomizeIntesnity = new Vector3(0.5f, 0f, 0f);

    private bool _droppedLoot = false;

    protected override void Death()
    {
        if (!_droppedLoot) 
        {
            DropLoot(_lootTable);
            DropPickupables(_pickupTable);

            _droppedLoot = true;
        }
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
        GameObject newItem = Instantiate(itemPrefab, position.position, Quaternion.identity);

        newItem.transform.SetParent(null);
        newItem.transform.localScale = Vector3.one/4;
        newItem.transform.position = SpawnPositionOffset(newItem.transform.position, _offset, _randomizeIntesnity);

        newItem.GetComponent<ItemContainer>().SetupItem(item);

        return newItem;
    }

    protected virtual void DropPickupables(LootTable pickupTable){
        if (_pickupTable == null) return;

        Item droppedLoot = _pickupTable.DetermineLoot();
        if(droppedLoot != null) CreatePickup(droppedLoot, _pickupPrefab, transform);
    }

    GameObject CreatePickup(Item item, GameObject itemPrefab, Transform position)
    {
        GameObject newItem = Instantiate(itemPrefab, position.position, Quaternion.identity);

        newItem.transform.SetParent(null);
        newItem.transform.localScale = Vector3.one/4;
        newItem.transform.position = SpawnPositionOffset(newItem.transform.position, _offset, _randomizeIntesnity);

        newItem.GetComponent<PickupableContainer>().SetupPickupables((Pickupables)item);

        return newItem;
    }

    private Vector3 SpawnPositionOffset(Vector3 startPosition, Vector3 offset, Vector3 randomizeIntesnity)
    {
        startPosition += _offset;
        startPosition += new Vector3(Random.Range(-_randomizeIntesnity.x, _randomizeIntesnity.x),
                                               Random.Range(-_randomizeIntesnity.y, _randomizeIntesnity.y),
                                               Random.Range(-_randomizeIntesnity.z, _randomizeIntesnity.z));

        return startPosition;
    }

}
