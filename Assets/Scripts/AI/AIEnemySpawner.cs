using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AISystem
{
    public class AIEnemySpawner : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface _navMesh;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private int _countOfEnemys = 5;
        [SerializeField] private float _rangeFormCenter = 10f;

        private void Start()
        {
           // Spawn(_enemyPrefab, _countOfEnemys);
        }

        public void Spawn()
        {
            Spawn(_enemyPrefab, _countOfEnemys);
        }
        
        private void Spawn(GameObject _prefab, int _count)
        {
            float x = _navMesh.size.x;
            float z = _navMesh.size.z;
            
            for (int i = 0; i < _count; i++)
            {
                var center = _navMesh.center; // new Vector3(Random.Range(1f, x),Random.Range(1f, z), 0f);
                GameObject enemy = Instantiate(_prefab, center, Quaternion.identity);
                enemy.GetComponent<AIFSMAgent>().SetPositionAtNavMesh(_rangeFormCenter);
            }
        }
    }
}