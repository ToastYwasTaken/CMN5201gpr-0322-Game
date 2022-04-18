using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    protected int _iD;
    public int ID { get => _iD; }

    protected string _name;
    public string Name { get => _name; }

    protected string _description;
    public string Description { get => _description; }

    protected bool _isStackable;
    public bool IsStackable { get => _isStackable; }
}
