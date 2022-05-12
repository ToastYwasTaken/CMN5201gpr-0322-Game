using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    /// <summary>
    /// Generiert NavMeshes zur Laufzeit
    /// </summary>
    public class AINavMeshGenerator : MonoBehaviour
    {
        public NavMeshSurface[] Meshes;
        public bool BuildMeshByStart = false;
        public bool SpawnEnemy = false;
        public AIEnemySpawner Spawner;
        
        public void BuildMesh()
        {
            foreach (NavMeshSurface mesh in Meshes)
            {
                mesh.BuildNavMesh();
            }
        }

        private void Start()
        {
            if (!BuildMeshByStart) return;
            BuildMesh();
            if (!SpawnEnemy) return;
            Spawner.Spawn();
        }
    }
}


