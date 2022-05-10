using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [SerializeField] protected int _iD;
    public int ID { get => _iD; }

    [SerializeField] protected string _name;
    public string Name { get => _name; }

    [SerializeField] protected string _description;
    public string Description { get => _description; }

    [SerializeField] protected bool _isStackable;
    public bool IsStackable { get => _isStackable; }
}
