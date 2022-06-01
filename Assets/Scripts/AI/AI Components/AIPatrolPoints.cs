using UnityEngine;
using UnityEngine.AI;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AIPatrolPoints.cs
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
    /// Determines a fixed patrol route for the agent
    /// </summary>
    public class AIPatrolPoints
    {
        private int _currentPoint = 0;
        public Transform[] PatrolPoints { get; set; }
        public Transform CurrentPoint => PatrolPoints[_currentPoint];

        /// <summary>
        /// Returns the next patrol point
        /// </summary>
        /// <returns></returns>
        public Transform GetNext()
        {
            Transform point = PatrolPoints[_currentPoint];
            _currentPoint = (_currentPoint + 1) % PatrolPoints.Length;
            return point;
        }

        /// <summary>
        /// Returns whether the point has been reached
        /// </summary>
        /// <param name="agent"></param>
        /// <returns>bool</returns>
        public bool HasReached(NavMeshAgent agent)
        {
            if (agent.pathPending) return false;

            return agent.remainingDistance <= agent.stoppingDistance;

        }
    }
}