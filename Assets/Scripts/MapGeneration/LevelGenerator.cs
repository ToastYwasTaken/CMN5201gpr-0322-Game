using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Only Unity dependent class for Map-Generation
/// Assign prefabs and roomCount in Inspector
/// </summary>

namespace Assets.Scripts.MapGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        /// <summary>
        /// For optimal settings choose width and height 4x bigger than partitionWidth / partitionHeight
        /// </summary>
        [SerializeField]
        GameObject _mapMotherGO;
        [Tooltip("total map Width")]
        [SerializeField]
        int _mapWidth;
        [Tooltip("total map Height")]
        [SerializeField]
        int _mapHeight;
        [SerializeField]
        [Tooltip("minimal width required for splitting a map into 2 new ones")]
        int _minPartitionWidth;
        [SerializeField]
        [Tooltip("minimal height required for splitting a map into 2 new ones")]
        int _minPartitionHeight;
        [SerializeField]
        GameObject _emptyPrefab;
        [SerializeField]
        GameObject _groundPrefab;
        [SerializeField]
        GameObject _wallPrefab;
        [SerializeField]
        GameObject _borderPrefab;
        [SerializeField]
        GameObject _cornerPrefab;
        [SerializeField]
        GameObject _debugGroundPrefab;
        [SerializeField]
        bool turnOffHallWays;

        private BSPMap mapRoot;
        //containt all maps, the ones generated and their originals
        private List<BSPMap> _allMaps = new List<BSPMap>();
        //smallest leafs contains the smallest Leafs / partitions that remain after splitting the original x times
        private List<BSPMap> _onlySmallestPartitions = new List<BSPMap>();

        void Awake()
        {
            //Assign motherGO of Map
            if (_mapMotherGO == null)
            {
                _mapMotherGO = new GameObject("Map");
                _mapMotherGO.transform.position = new Vector3(0, 0, 0);
            }
            //create original map root
            mapRoot = new BSPMap(0, 0, _mapWidth, _mapHeight);
            //add original map
            _allMaps.Add(mapRoot);
            //Create BSP Map
            CreateBSPMap();
            Debug.Log("successfully created BSP map");
            //Level Generation only, no prefab instantiation
            mapRoot.CreateRooms(_emptyPrefab, _groundPrefab, _wallPrefab, _borderPrefab,_cornerPrefab, _debugGroundPrefab);
            InstantiateRooms();
        }

        /// <summary>
        /// Create BSP Map
        /// </summary>
        private void CreateBSPMap()
        {
            bool didSplit = true;
            int splitAmount = 0;
            int securityBreakCounter = 0;
            //Set static boarder max / min values for partitions
            BSPMap.AssignMinValues(_minPartitionWidth, _minPartitionHeight);
            do
            {
                if (securityBreakCounter > 200)
                {
                    throw new StackOverflowException($"security break counter {securityBreakCounter} was reached! Catched infinite loop");
                }
                didSplit = false;
                foreach (BSPMap map in _allMaps)
                {
                    if (map.FirstMap == null && map.SecondMap == null)
                    {
                        if (map.SplitMap())
                        {
                            //Add map to list
                            _allMaps.Add(map.FirstMap);
                            _allMaps.Add(map.SecondMap);
                            //Add splitted rooms / remove room that was split
                            _onlySmallestPartitions.Remove(map);
                            _onlySmallestPartitions.Add(map.FirstMap);
                            _onlySmallestPartitions.Add(map.SecondMap);
                            didSplit = true;
                            splitAmount++;
                            break;
                        }
                    }
                }
                securityBreakCounter++;
            } while (didSplit);
            Debug.Log($"Splitted original map '{splitAmount}' times to create '{_allMaps.Count}' rooms over all.\nAfter removing rooms that were splitted, '{_onlySmallestPartitions.Count}' rooms remain");
        }

        /// <summary>
        /// Instantiates rooms for each smallest Leaf of BSPMap 
        /// </summary>
        private void InstantiateRooms()
        {
            //Override last room as Boss room (kinda cheated but idk how else to solve it
            Room lastRoom = BSPMap.s_allRooms[BSPMap.s_allRooms.Count-1];
            int lastRoomX = lastRoom.X;
            int lastRoomY = lastRoom.Y;
            int lastRoomWidth = lastRoom.Width;
            int lastRoomHeight = lastRoom.Height;
            BSPMap.s_allRooms.Remove(lastRoom);
            BSPMap.s_allRooms.Add(new BossRoom(_emptyPrefab, _groundPrefab, _wallPrefab, _borderPrefab, _cornerPrefab, lastRoomX, lastRoomY, lastRoomWidth, lastRoomHeight));
            InstantiateNormalRooms();
            if(turnOffHallWays == false)
            {
            InitHallWays();
            }
        }

        /// <summary>
        /// Instantiate Normal rooms / pass instantiating boss room
        /// </summary>
        private void InstantiateNormalRooms()
        {
            GameObject newRoomTile;
            GameObject motherOfRoom;
            //Iterate over all rooms
            for (int h = 0; h < BSPMap.s_allRooms.Count; h++)
            {
                if (BSPMap.s_allRooms.Count != h+1)
                {
                    motherOfRoom = new GameObject($"Mother of room {h}");
                    motherOfRoom.transform.parent = _mapMotherGO.transform;
                    //Iterate over 1st dimension of array
                    for (int i = 0; i < BSPMap.s_allRooms[h].PTiles.GetLength(0); i++)
                    {
                        //Iterate over 2nd dimension of array
                        for (int j = 0; j < BSPMap.s_allRooms[h].PTiles.GetLength(1); j++)
                        {
                            //Instantiating normal rooms
                            newRoomTile = Instantiate(BSPMap.s_allRooms[h].PTiles[i, j].Prefab, BSPMap.s_allRooms[h].PTiles[i, j].Position, BSPMap.s_allRooms[h].PTiles[i, j].Rotation);
                            newRoomTile.transform.name = $"Normal Room: {BSPMap.s_allRooms[h].PTiles[i, j].Prefab.name} Tile [{BSPMap.s_allRooms[h].PTiles[i, j].Position.x}|{BSPMap.s_allRooms[h].PTiles[i, j].Position.y}]";
                            newRoomTile.transform.parent = motherOfRoom.transform;
                        }
                    }
                }
                else InstantiateBossRoom(h);
            }
        }

        /// <summary>
        /// Instantiate Boss room
        /// </summary>
        /// <param name="passCounter"></param>
        private void InstantiateBossRoom(int passCounter)
        {
            GameObject newRoomTile;
            GameObject motherOfRoom = new GameObject($"Mother of boss room {passCounter}");
            motherOfRoom.transform.parent = _mapMotherGO.transform;
            //Iterate over 1st dimension of array
            for (int i = 0; i < BSPMap.s_allRooms[passCounter].PTiles.GetLength(0); i++)
            {
                //Iterate over 2nd dimension of array
                for (int j = 0; j < BSPMap.s_allRooms[passCounter].PTiles.GetLength(1); j++)
                {
                    //Instantiating boss room
                    newRoomTile = Instantiate(BSPMap.s_allRooms[passCounter].PTiles[i, j].Prefab, BSPMap.s_allRooms[passCounter].PTiles[i, j].Position, BSPMap.s_allRooms[passCounter].PTiles[i, j].Rotation);
                    newRoomTile.transform.name = $" Boss room: {BSPMap.s_allRooms[passCounter].PTiles[i, j].Prefab.name} Tile [{BSPMap.s_allRooms[passCounter].PTiles[i, j].Position.x}|{BSPMap.s_allRooms[passCounter].PTiles[i, j].Position.y}]";
                    newRoomTile.transform.parent = motherOfRoom.transform;
                }
            }
        }

        /// <summary>
        /// Instantiate Hallway connections
        /// </summary>
        private void InitHallWays()
        {
            GameObject newRoomTile;
            GameObject motherOfRoom = new GameObject($"Mother of Hallways");
            for (int h = 0; h < BSPMap.s_allHallWays.Count; h++)
            {
                motherOfRoom.transform.parent = _mapMotherGO.transform;
                for (int i = 0; i < BSPMap.s_allHallWays[h].PTiles.GetLength(0); i++)
                {
                    for (int j = 0; j < BSPMap.s_allHallWays[h].PTiles.GetLength(1); j++)
                    {
                        newRoomTile = Instantiate(BSPMap.s_allHallWays[h].PTiles[i, j].Prefab, BSPMap.s_allHallWays[h].PTiles[i, j].Position, BSPMap.s_allHallWays[h].PTiles[i, j].Rotation);
                        newRoomTile.transform.name = $"Hallway: {BSPMap.s_allHallWays[h].PTiles[i, j].Prefab.name} Tile [{BSPMap.s_allHallWays[h].PTiles[i, j].Position.x}|{BSPMap.s_allHallWays[h].PTiles[i, j].Position.y}]";
                        newRoomTile.transform.parent = motherOfRoom.transform;
                    }
                }
            }
        }


    }
}
