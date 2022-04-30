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
        protected GameObject Prefab;
        protected Vector3 Position;
        protected Quaternion Rotation;

        public Tile(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            Prefab = prefab;
            Position = position;
            Rotation = rotation;
        }

    }
}
