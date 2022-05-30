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

    protected virtual void DropLoot(LootTable lootTable)
    {
        if (_lootTable == null) return;

        GameObject droppedLoot = _lootTable.DetermineLoot();
        if (droppedLoot != null) Instantiate(droppedLoot, transform.position, Quaternion.identity);
    }
}
