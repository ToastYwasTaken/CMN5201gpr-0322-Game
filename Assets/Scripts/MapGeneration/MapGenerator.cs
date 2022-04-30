using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Only Unity dependent class for Map-Generation
/// Assign prefabs and roomCount in Inspector
/// </summary>

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
        GameObject _mapMother;
        [SerializeField]
        GameObject _groundPrefab;
        [SerializeField]
        GameObject _wallPrefab;
        [SerializeField]
        GameObject _borderPrefab;
        [SerializeField]
        int _roomCount;

        private Level _level;
        void Awake()
        {
            //Level Generation only, no prefab instantiation
            _level = new Level(_roomCount, _groundPrefab, _wallPrefab, _borderPrefab, ERoomSize.Random);
        }
        
        /// <summary>
        /// Creates a new Level by instantiating the prefabs accordingly
        /// </summary>
        public void CreateLevel()
        {
            int count = 0;
            foreach (Room room in _level.PRooms)
            {
                Room currRoom = _level.PRooms[count];
                foreach (Tile tile in currRoom.PTiles)
                {
                    //Instantiate Prefabs
                }
            }
        }


    }
}
