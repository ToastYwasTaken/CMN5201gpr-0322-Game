using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.MapGeneration;
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
        GameObject _mapMotherGO;
        [SerializeField]
        int _mapWidth;
        [SerializeField]
        int _mapHeight;
        [SerializeField]
        int _minMapWidth;
        [SerializeField]
        int _minMapHeight;
        [SerializeField]
        int _maxSize;
        [SerializeField]
        GameObject _groundPrefab;
        [SerializeField]
        GameObject _wallPrefab;
        [SerializeField]
        GameObject _borderPrefab;
        [SerializeField]
        int _roomCount;

        private Level _level;
        private List<Map> _allMaps = new List<Map>();
        //smallest leafs contains the smallest Leafs that remain after splitting
        private List<Map> _onlySmallestLeafs = new List<Map>();
        void Awake()
        {
            //Create BSP Map
            CreateBSPMap();
            Debug.Log("successfully created BSP map");
            //Level Generation only, no prefab instantiation
            _level = new Level(_roomCount, _groundPrefab, _wallPrefab, _borderPrefab, ERoomSize.Random);
        }
        
        /// <summary>
        /// Create BSP Map
        /// </summary>
        public void CreateBSPMap()
        {
            bool didSplit = true;
            int splitAmount = 0;
            //Set static boarder max / min values
            Map.AssignMinAndMaxValues(_minMapWidth, _minMapHeight, _maxSize);
            //create original map
            Map mapRoot = new Map(0, 0, _mapWidth, _mapHeight);
            //add original map
            _allMaps.Add(mapRoot);
            do
            {
                didSplit = false;
                foreach (Map map in _allMaps)
                {
                    if (map.FirstMap == null && map.SecondMap == null)
                    {
                        if (map.SplitMap())
                        {
                            //Add map to list
                            _allMaps.Add(map.FirstMap);
                            _allMaps.Add(map.SecondMap);
                            //Add splitted rooms / remove room that was split
                            _onlySmallestLeafs.Remove(map);
                            _onlySmallestLeafs.Add(map.FirstMap);
                            _onlySmallestLeafs.Add(map.SecondMap);
                            didSplit = true;
                            splitAmount++;
                            break;
                        }
                    }
                }
            } while (didSplit);
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
