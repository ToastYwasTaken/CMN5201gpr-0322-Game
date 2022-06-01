using UnityEngine;
using UnityEngine.AI;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AIRandomPatrol.cs
* Date : 29.05.2022
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
    /// Generates random patrol points for the agent
    /// </summary>
    public class AIRandomPatrol
    {
        /// <summary>
        /// Generates a random point on the NavMesh and returns it as Vector3. 
        /// Additionally it will return a bool (true = point on mesh / false = not on mesh)
        /// </summary>
        /// <param name="center"></param>
        /// <param name="range"></param>
        /// <param name="maxDistance"></param>
        /// <param name="result"></param>
        /// <returns>bool; out Vector3</returns>
        public bool GetRandomWaypoint(Vector3 center, float range, float maxDistance, out Vector3 result)
        {
            Vector3 rndPoint = center + (Random.insideUnitSphere * range);
            if (NavMesh.SamplePosition(rndPoint, out NavMeshHit hit,
                maxDistance, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
        }

        /// <summary>
        /// Returns whether the point has been reached
        /// </summary>
        /// <param name="agent"></param>
        /// <returns>bool</returns>
        public bool HasReached(NavMeshAgent agent)
        {
            if (agent.pathPending) return false;

            if (!(agent.remainingDistance <= agent.stoppingDistance)) return false;
            return !agent.hasPath || agent.velocity.sqrMagnitude == 0f;
        }

        /// <summary>
        /// Returns whether the point was reached based on the distance
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="distance"></param>
        /// <returns>bool</returns>
        public bool HasReachedByDistance(NavMeshAgent agent, float distance)
        {
            Vector3 currentTarget = agent.destination;

            float remainingDistance = Vector3.Distance(agent.transform.position, currentTarget);
            // Debug.Log(remainingDistance);
            return remainingDistance <= distance;
        }
    }
}