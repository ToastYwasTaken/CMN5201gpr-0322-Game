using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [Header("Item Settings")]
    [SerializeField] protected int _iD;
    public int ID { get => _iD; }

    [SerializeField] protected string _name;
    public string Name { get => _name; }

    [SerializeField] protected string _description;
    public string Description { get => _description; }

    [SerializeField] protected bool _isStackable;
    public bool IsStackable { get => _isStackable; }

    [SerializeField] eItemRarity _itemRarity = eItemRarity.Common;
    public eItemRarity ItemRarity { get => _itemRarity; }

    [SerializeField] eItemType _itemType;
    public eItemType ItemType { get => _itemType; }

    [SerializeField] private Sprite _itemSprite;
    public Sprite ItemSprite { get => _itemSprite; }
}

public enum eItemRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}