using UnityEngine;
using UnityEngine.AI;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AINavMeshGenerator.cs
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
    /// Generates NavMeshes at runtime
    /// </summary>
    [RequireComponent(typeof(AIEnemySpawner))]
    public class AINavMeshGenerator : MonoBehaviour
    {
        public NavMeshSurface[] Meshes;
        public bool BuildMeshByStart = false;
        public bool SpawnEnemies = true;

        /// <summary>
        /// Build Mesh and Spawn Enemies when active
        /// </summary> 
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


