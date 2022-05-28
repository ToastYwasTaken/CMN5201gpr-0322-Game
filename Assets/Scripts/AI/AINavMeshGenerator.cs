using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    /// <summary>
    /// Generiert NavMeshes zur Laufzeit
    /// </summary>
    [RequireComponent(typeof(AIEnemySpawner))]
    public class AINavMeshGenerator : MonoBehaviour
    {
        public NavMeshSurface[] Meshes;
        public bool BuildMeshByStart = false;
        public bool SpawnEnemies = true;

        public void BuildMesh()
        {
            foreach (NavMeshSurface mesh in Meshes)
            {
                mesh.BuildNavMesh();
            }

            if (SpawnEnemies)
                GetComponent<AIEnemySpawner>().SpawnEnemies();
        }

        private void Start()
        {
            if (BuildMeshByStart) BuildMesh();

        }
    }
}


