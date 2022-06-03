using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Only Unity dependent class for Map-Generation
/// Assign prefabs and roomCount in Inspector
/// </summary>

namespace Assets.Scripts.MapGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        /// <summary>
        /// For optimal settings choose width and height ~ 4x bigger t han partitionWidth / partitionHeight
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
        GameObject[] _groundPrefabs;
        [SerializeField]
        GameObject[] _obstaclePrefabs;
        [SerializeField]
        GameObject _wallPrefab;
        [SerializeField]
        GameObject _cornerPrefab;
        [SerializeField]
        GameObject[] _doorPrefabs;
        [SerializeField]
        bool _turnOffHallWays;

        private System.Random _rdm;
        private int _rdmIntGround;
        private int _rdmIntDoor;
        private BSPMap _mapRoot;
        private float _elapsedTime;
        //containt all maps, the ones generated and their originals
        private List<BSPMap> _allMaps = new List<BSPMap>();
        //smallest leafs contains the smallest Leafs / partitions that remain after splitting the original x times
        private List<BSPMap> _onlySmallestPartitions = new List<BSPMap>();

        void Awake()
        {
            _elapsedTime = Time.realtimeSinceStartup;
            //Clear map mother
            foreach (Transform child in _mapMotherGO.transform)
            {
                Debug.Log(child.name);
                if (child.name != "NavMesh" && child.name != "NavMesh (Boss)")
                {
                    Destroy(child.gameObject);
                }
            }
            //Clear all lists
            if (_allMaps.Count != 0)
            {
                _allMaps.Clear();
            }
            if (_onlySmallestPartitions.Count != 0)
            {
                _onlySmallestPartitions.Clear();
            }
            if (GlobalValues.sDoorByPos.Count != 0)
            {
                GlobalValues.sDoorByPos.Clear();
            }
            //create original map root
            _mapRoot = new BSPMap(0, 0, _mapWidth, _mapHeight);
            //add original map
            _allMaps.Add(_mapRoot);
            //Create BSP Map
            CreateBSPMap();
            //Choose random Prefab for ground
            _rdm = new System.Random();
            do
            {
                _rdmIntGround = _rdm.Next(0, _groundPrefabs.Length);
            } while (_groundPrefabs[_rdmIntGround] == null);
            //Repeat for door
            do
            {
                _rdmIntDoor = _rdm.Next(0, _doorPrefabs.Length);
            } while (_doorPrefabs[_rdmIntDoor] == null);
            //Removing null obcects if any in _ostaclePrefabs
            _obstaclePrefabs = _obstaclePrefabs.Where(x => x != null).ToArray();
            //Level Generation only, no prefab instantiation
            _mapRoot.CreateRooms(_groundPrefabs[_rdmIntGround], _obstaclePrefabs, _wallPrefab, _cornerPrefab, _doorPrefabs[_rdmIntDoor]);
            //Now instantiating
            InstantiateRooms();
            Debug.Log($"successfully instantiated the Level [elapsed time: {Time.realtimeSinceStartup - _elapsedTime}]");
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
            BSPMap.AssignMinPartitionValues(_minPartitionWidth, _minPartitionHeight);
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
            BSPMap.s_allRooms.Add(new BossRoom(_groundPrefabs[_rdmIntGround], _obstaclePrefabs, _wallPrefab, _cornerPrefab, lastRoomX, lastRoomY, lastRoomWidth, lastRoomHeight));
            InstantiateNormalRooms();
            if (_turnOffHallWays == false)
            {
                InstantiateHallWays();
            }
        }

        /// <summary>
        /// Instantiate Normal rooms / pass instantiating boss room
        /// </summary>
        private void InstantiateNormalRooms()
        {
            GameObject newRoomTile;
            GameObject motherOfRoom;
            if (BSPMap.s_allRooms == null ||BSPMap.s_allRooms.Count == 0)
            {
                return;
            }
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
                            //Don't instantiate border where the HallWay is
                            if (!HallWay.s_connections.Contains(BSPMap.s_allRooms[h].PTiles[i, j].Position))
                            {
                                //Instantiating normal rooms
                                newRoomTile = Instantiate(BSPMap.s_allRooms[h].PTiles[i, j].Prefab, BSPMap.s_allRooms[h].PTiles[i, j].Position, BSPMap.s_allRooms[h].PTiles[i, j].Rotation);
                                newRoomTile.transform.name = $"Normal Room: {BSPMap.s_allRooms[h].PTiles[i, j].Prefab.name} Tile [{BSPMap.s_allRooms[h].PTiles[i, j].Position.x}|{BSPMap.s_allRooms[h].PTiles[i, j].Position.y}]";
                                newRoomTile.transform.parent = motherOfRoom.transform;
                            }
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
                    //Don't instantiate border where the HallWay is
                    if (!HallWay.s_connections.Contains(BSPMap.s_allRooms[passCounter].PTiles[i, j].Position))
                    {
                        //Instantiating boss room
                        newRoomTile = Instantiate(BSPMap.s_allRooms[passCounter].PTiles[i, j].Prefab, BSPMap.s_allRooms[passCounter].PTiles[i, j].Position, BSPMap.s_allRooms[passCounter].PTiles[i, j].Rotation);
                        newRoomTile.transform.name = $" Boss room: {BSPMap.s_allRooms[passCounter].PTiles[i, j].Prefab.name} Tile [{BSPMap.s_allRooms[passCounter].PTiles[i, j].Position.x}|{BSPMap.s_allRooms[passCounter].PTiles[i, j].Position.y}]";
                        newRoomTile.transform.parent = motherOfRoom.transform;
                    }
                }
            }
        }

        /// <summary>
        /// Instantiate Hallway connections
        /// </summary>
        private void InstantiateHallWays()
        {
            GameObject newRoomTile;
            GameObject motherOfRoom = new GameObject($"Mother of Hallways");
            if (BSPMap.s_allHallWays == null ||BSPMap.s_allHallWays.Count == 0)
            {
                return;
            }
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
