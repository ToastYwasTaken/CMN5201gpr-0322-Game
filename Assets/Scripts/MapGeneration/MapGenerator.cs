using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializedField]
        GameObject _roomPrefab;
        [SerializedField]
        GameObject _wallPrefab;
        [SerializedField]
        GameObject _roomCount;

        public enum ERoomSize
        {
            Random = -1,
            Normal = 0,
        }

        void Awake()
        {
            new Level(_roomCount, _wallPrefab, _roomPrefab, ERoomSize.Random);
        }
    }
}
