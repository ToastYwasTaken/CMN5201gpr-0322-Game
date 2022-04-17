using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    public enum ERoomSize
    {
        Random = -1,
        Normal = 0,
    }
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField]
        GameObject _roomPrefab;
        [SerializeField]
        GameObject _wallPrefab;
        [SerializeField]
        int _roomCount;

        private Level _level;
        void Awake()
        {
            _level = new Level(_roomCount, _wallPrefab, _roomPrefab, ERoomSize.Random);
        }
        
        private void CreateLevel()
        {
            foreach (var room in _level.Rooms)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
