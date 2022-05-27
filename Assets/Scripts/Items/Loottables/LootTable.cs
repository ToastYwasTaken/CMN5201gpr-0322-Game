using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    [SerializeField] private GameObject _lootDrop;
    public GameObject LootDrop { get => _lootDrop; }
    [SerializeField] private float _dropChance;
    public float DropChance { get => _dropChance; }
}

[CreateAssetMenu(fileName = "New Loot Table", menuName = "Entities/Loot Table", order = 100)]
public class LootTable : ScriptableObject
{
    [SerializeField] private Loot[] _lootArray;

    public GameObject DetermineLoot()
    {

        for (int i = 0; i < _lootArray.Length; i++)
        {
            float randomChance = Random.Range(0f, 1f);

            if (_lootArray[i] == null) continue;

            float dropChance = _lootArray[i].DropChance / 100f;

            if (randomChance <= dropChance) return _lootArray[i].LootDrop;
        }

        return null;
    }
}
