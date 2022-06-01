using System;
using System.Collections.Generic;
using Assets.Scripts.MapGeneration;
using UnityEngine;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AIEnemySpawner.cs
* Date : 12.05.2022
* Author : René Kraus (RK)
*
* These coded instructions, statements, and computer programs contain
* proprietary information of the author and are protected by Federal
* copyright law. They may not be disclosed to third parties or copied
* or duplicated in any form, in whole or in part, without the prior
* written consent of the author.
******************************************************************************/
namespace AISystem
{
    /// <summary>
    /// Spawn enemies on the NavMesh in the rooms
    /// </summary>
    public class AIEnemySpawner : MonoBehaviour
    {
        [Header("Enemies Info")]
        [SerializeField] private EnemyInfo[] _enemyInfo;
        [SerializeField] private float _rangeFromCenter = 10f;
        [SerializeField] private float _maxDistance = 1f;

        [Header("Room Info")]
        [SerializeField] RoomInfo[] _roomInfos;

        [Header("Boss Room Info")]
        [SerializeField] private GameObject _bossPrefab;
        [SerializeField] private int _spawnCount = 1;
        [SerializeField] private bool _spawnEnemySupporter = false;
        [SerializeField] private GameObject _enemySupporter;
        [SerializeField] private int _spawnSupporterCount = 0;
        [SerializeField] private Transform _enemiesParent;

        /// <summary>
        /// Spawn Enemies and the Boss Enemy
        /// </summary>
        public void SpawnEnemies()
        {
            for (int i = 0; i < _enemyInfo.Length; i++)
            {
                Spawn(_enemyInfo[i].EnemyPrefab, _enemyInfo[i].SpawnCount);
            }

            if (_bossPrefab == null) return;
            SpawnBoss();
        }

        /// <summary>
        /// Spawn Enemies
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="count"></param>
        public void Spawn(GameObject prefab, int count)
        {
            // Get all rooms
            List<Room> rooms = BSPMap.s_allRooms;

            if (rooms.Count == 0)
            {
                Debug.LogError("No Rooms for spawnig Enemys!");
                return;
            }

            // Create parent GameObject for Enemies
            var parentObject = new GameObject(prefab.name);

            parentObject.transform.parent = _enemiesParent;

            int enemyCount = 0;
            for (int i = 0; i < rooms.Count; i++)
            {
                // Calculate the center of the Room in the World
                float x = rooms[i].X + (rooms[i].Width * 0.5f);
                float y = rooms[i].Y + (rooms[i].Height * 0.5f);

                float roomSize = rooms[i].Width * rooms[i].Height;

                int spawnRate = count * _roomInfos[0].SpawnMultiplier;

                // Calculate Spawn rate by roomsize
                for (int r = 0; r < _roomInfos.Length; r++)
                {
                    if (roomSize <= _roomInfos[r].Size)
                    {
                        spawnRate = count * _roomInfos[r].SpawnMultiplier;
                        break;
                    }

                    if (roomSize >= _roomInfos[r].Size)
                    {
                        spawnRate = count * _roomInfos[r].SpawnMultiplier;
                    }

                }
                Debug.Log($"RoomInfo: Size: {roomSize} | Enemies: {spawnRate}");

                GameObject roomParent = Instantiate(new GameObject($"Room{i}"));///////////////////////////
                roomParent.transform.SetParent(parentObject.transform);

                // Spawn Enemies
                for (int j = 0; j < spawnRate; j++)
                {
                    float offsetX = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float offsetY = UnityEngine.Random.Range(-2.5f, 2.5f);
                    var center = new Vector3(x + offsetX, y + offsetY, 0f);
                    GameObject enemy = Instantiate(prefab, center, Quaternion.identity);
                    enemy.name = $"{prefab.name}-{enemyCount++}";
                    enemy.transform.parent = roomParent.transform; ////////////////////////////////
                    enemy.GetComponent<AIFSMAgent>().SetPositionRandomAtNavMesh(_rangeFromCenter, _maxDistance);
                }
            }
        }

        /// <summary>
        /// Spawn the Boss Enemy and the Boss supporter Enemies in the last room
        /// </summary>
        public void SpawnBoss()
        {
            List<Room> rooms = BSPMap.s_allRooms;

            if (rooms.Count == 0)
            {
                Debug.LogError("No Rooms for spawnig Enemies!");
                return;
            }

            var parentObject = new GameObject($"BOSS: {_bossPrefab.name}");
            int enemyCount = 0;

            // Calculate the center of the Room in the World
            // The last Room in the list, is the Boss Room
            int lastRoomIdx = rooms.Count - 1;
            float x = rooms[lastRoomIdx].X + (rooms[lastRoomIdx].Width * 0.5f);
            float y = rooms[lastRoomIdx].Y + (rooms[lastRoomIdx].Height * 0.5f);

            // Spawn Enemies
            for (int j = 0; j < _spawnCount; j++)
            {
                float offsetX = UnityEngine.Random.Range(-2.5f, 2.5f);
                float offsetY = UnityEngine.Random.Range(-2.5f, 2.5f);
                var rotation = Quaternion.Euler(0f, 0f, 180f);
                var center = new Vector3(x + offsetX, y + offsetY, 0f);
                GameObject enemy = Instantiate(_bossPrefab, center, rotation);
                enemy.name = $"{_bossPrefab.name}-{enemyCount++}";
                enemy.transform.parent = parentObject.transform;
                enemy.GetComponent<AIFSMAgent>().SetPositionRandomAtNavMesh(_rangeFromCenter, _maxDistance);
            }

            // Spawn supporter enemies, when active
            if (_spawnEnemySupporter)
            {
                for (int j = 0; j < _spawnSupporterCount; j++)
                {
                    float offsetX = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float offsetY = UnityEngine.Random.Range(-2.5f, 2.5f);

                    var center = new Vector3(x + offsetX, y + offsetY, 0f);
                    GameObject enemy = Instantiate(_enemySupporter, center, Quaternion.identity);
                    enemy.name = $"{_enemySupporter.name}-{enemyCount++}";
                    enemy.transform.parent = parentObject.transform;
                    enemy.GetComponent<AIFSMAgent>().SetPositionRandomAtNavMesh(_rangeFromCenter, _maxDistance);
                }
            }

        }
    }

    [Serializable]
    public class RoomInfo
    {
        public float Size = 0f;
        public int SpawnMultiplier = 1;
    }

    [Serializable]
    public class EnemyInfo
    {
        public GameObject EnemyPrefab;
        public int SpawnCount = 0;
    }

}