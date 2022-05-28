using System;
using System.Collections.Generic;
using Assets.Scripts.MapGeneration;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

namespace AISystem
{
    public class AIEnemySpawner : MonoBehaviour
    {
        // [SerializeField] private bool _spawnByStart = true;
        [Header("Enemies Info")]
        [SerializeField] private EnemyInfo[] _enemyInfo;
        [SerializeField] private float _rangeFromCenter = 10f;
        [SerializeField] private float _maxDistance = 1f;

        [Header("Room Info")]
        [SerializeField] RoomInfo[] _roomInfos;

        [Header("Boss Room Info")]
        [SerializeField] private GameObject _bossPrefab;
        [SerializeField] private int _spawnCount = 1;

        public void SpawnEnemies()
        {
            for (int i = 0; i < _enemyInfo.Length; i++)
            {
                Spawn(_enemyInfo[i].EnemyPrefab, _enemyInfo[i].SpawnCount);
            }

            if (_bossPrefab == null) return;
            SpawnBoss();
        }

        public void Spawn(GameObject prefab, int count)
        {
            List<Room> rooms = BSPMap.s_allRooms;

            if (rooms.Count == 0)
            {
                Debug.LogError("No Rooms for spawnig Enemys!");
                return;
            }

            var parentObject = new GameObject(prefab.name);
            int enemyCount = 0;
            for (int i = 0; i < rooms.Count; i++)
            {
                // Calculate the center of the Room in the World
                float x = rooms[i].X + (rooms[i].Width * 0.5f);
                float y = rooms[i].Y + (rooms[i].Height * 0.5f);

                float roomSize = rooms[i].Width * rooms[i].Height;

                int spawnRate = count * _roomInfos[0].SpawnMultiplier;

                // Calculate Spawn rate by Roomsize
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
                        //break;
                    }

                }
                Debug.Log($"RoomInfo: Size: {roomSize} | Enemies: {spawnRate}");

                // Spawn Enemies
                for (int j = 0; j < spawnRate; j++)
                {
                    float offsetX = UnityEngine.Random.Range(-2.5f, 2.5f);
                    float offsetY = UnityEngine.Random.Range(-2.5f, 2.5f);
                    var center = new Vector3(x + offsetX, y + offsetY, 0f);
                    GameObject enemy = Instantiate(prefab, center, Quaternion.identity);
                    enemy.name = $"{prefab.name}-{enemyCount++}";
                    enemy.transform.parent = parentObject.transform;
                    enemy.GetComponent<AIFSMAgent>().SetPositionRandomAtNavMesh(_rangeFromCenter, _maxDistance);
                }
            }
        }

        public void SpawnBoss()
        {
            List<Room> rooms = BSPMap.s_allRooms;

            if (rooms.Count == 0)
            {
                Debug.LogError("No Rooms for spawnig Enemys!");
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
                var center = new Vector3(x + offsetX, y + offsetY, 0f);
                GameObject enemy = Instantiate(_bossPrefab, center, Quaternion.identity);
                enemy.name = $"{_bossPrefab.name}-{enemyCount++}";
                enemy.transform.parent = parentObject.transform;
                enemy.GetComponent<AIFSMAgent>().SetPositionRandomAtNavMesh(_rangeFromCenter, _maxDistance);
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