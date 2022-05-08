using System;
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

    public class LevelGenerator : MonoBehaviour
    {
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
        GameObject _groundPrefab;
        [SerializeField]
        GameObject _wallPrefab;
        [SerializeField]
        GameObject _borderPrefab;

        private int _roomCount;
        private BSPMap mapRoot;
        //containt all maps, the ones generated and their originals
        private List<BSPMap> _allMaps = new List<BSPMap>();
        //smallest leafs contains the smallest Leafs / partitions that remain after splitting the original x times
        private List<BSPMap> _onlySmallestPartitions = new List<BSPMap>();

        void Awake()
        {
            //Assign motherGO of Map
            if(_mapMotherGO == null)
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
            mapRoot.CreateRooms(_groundPrefab, _wallPrefab, _borderPrefab);
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
                if(securityBreakCounter > 200)
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
            //Assign roomCount
            _roomCount = _onlySmallestPartitions.Count;
            Debug.Log($"Splitted original map '{splitAmount}' times to create '{_allMaps.Count}' rooms over all.\nAfter removing rooms that were splitted, '{_roomCount}' rooms remain");
        }

        /// <summary>
        /// Instantiates rooms per tile
        /// </summary>
        private void InstantiateRooms()
        {
            //System.Random rdm = new System.Random();
            //float rdmFloat = (float)rdm.NextDouble();
            //Iterate over all rooms
            for (int h = 0; h < BSPMap.s_roomsList.Count; h++)
            {
                GameObject newRoomTile;
                GameObject motherOfRoom = new GameObject($"Mother of room {h}");
                motherOfRoom.transform.parent = _mapMotherGO.transform;
                //Iterate over 1st dimension of array
                for (int i = 0; i < BSPMap.s_roomsList[h].PTiles.GetLength(0); i++)
                {
                    //Iterate over 2nd dimension of array
                    for (int j = 0; j < BSPMap.s_roomsList[h].PTiles.GetLength(1); j++)
                    {
                        newRoomTile = Instantiate(BSPMap.s_roomsList[h].PTiles[i, j].Prefab, BSPMap.s_roomsList[h].PTiles[i, j].Position, BSPMap.s_roomsList[h].PTiles[i, j].Rotation);
                        newRoomTile.transform.name = $"{BSPMap.s_roomsList[h].PTiles[i, j].Prefab.name} Tile [{BSPMap.s_roomsList[h].PTiles[i, j].Position.x}|{BSPMap.s_roomsList[h].PTiles[i, j].Position.y}]";
                        // for Debugging: newRoomTile.GetComponent<SpriteRenderer>().color = new Color(rdmFloat, rdmFloat, rdmFloat, 1f);
                        newRoomTile.transform.parent = motherOfRoom.transform;
                    }
                }
                //rdmFloat = (float)rdm.NextDouble();
            }
            Debug.Log("Instantiated all rooms");
        }


    }
}
