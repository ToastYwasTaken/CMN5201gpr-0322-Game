using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile 
{
    private GameObject _prefab;
    private Vector3 _position;
    private Quaternion _rotation;

    public Tile(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        _prefab = prefab;
        _position = position;
        _rotation = rotation;
    }

}
