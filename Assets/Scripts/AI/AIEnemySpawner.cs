using System.Collections.Generic;
using Assets.Scripts.MapGeneration;
using UnityEngine;

namespace AISystem
{
    public class AIEnemySpawner : MonoBehaviour
    {
        // [SerializeField] private bool _spawnByStart = true;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private int _countOfEnemys = 5;
        [SerializeField] private float _rangeFromCenter = 10f;
        [SerializeField] private float _maxDistance = 1f;

        private float[] _roomSizes = new float[] { 501, 1001, 1501 };
        private int[] _spawnMultiplier = new int[] { 1, 2, 3 };

        // private void Start()
        // {
        //     if (_spawnByStart) Spawn();
        // }

        public void Spawn()
        {
            Spawn(_enemyPrefab, _countOfEnemys);
        }

        private void Spawn(GameObject prefab, int count)
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
                int spawnRate = count * _spawnMultiplier[0];

                // Calculate Spawn rate by Roomsize
                for (int r = 0; r < _roomSizes.Length; r++)
                {
                    if (roomSize <= _roomSizes[r])
                    {
                        spawnRate = count * _spawnMultiplier[r];
                        break;
                    }

                    if (roomSize >= _roomSizes[r])
                    {
                        spawnRate = count * _spawnMultiplier[r];
                        //break;
                    }

                }
                Debug.Log($"RoomInfo: Size: {roomSize} | Enemies: {spawnRate}");

                // Spawn Enemies
                for (int j = 0; j < spawnRate; j++)
                {
                    float offsetX = Random.Range(-2.5f, 2.5f);
                    float offsetY = Random.Range(-2.5f, 2.5f);
                    var center = new Vector3(x + offsetX, y + offsetY, 0f);
                    GameObject enemy = Instantiate(prefab, center, Quaternion.identity);
                    enemy.name = $"{prefab.name}-{enemyCount++}";
                    enemy.transform.parent = parentObject.transform;
                    enemy.GetComponent<AIFSMAgent>().SetPositionRandomAtNavMesh(_rangeFromCenter, _maxDistance);
                }
            }
        }
    }
}