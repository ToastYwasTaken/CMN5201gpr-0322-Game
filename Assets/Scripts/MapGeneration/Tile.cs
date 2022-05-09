using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds specific information about each cell of a room
/// </summary>

namespace MapGeneration
{
    public class Tile
    {
        public GameObject Prefab;
        public Vector3 Position;
        public Quaternion Rotation;

        public Tile(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            Prefab = prefab;
            Position = position;
            Rotation = rotation;
        }

    }
}
