using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    public class AIPatrolPoints 
    {
        public Transform[] PatrolPoints {get; set;}

        private int _currentPoint = 0;
        public Transform CurrentPoint => PatrolPoints[_currentPoint];

        public Transform GetNext()
        {
            Transform point = PatrolPoints[_currentPoint];
            _currentPoint = (_currentPoint + 1) % PatrolPoints.Length;
            return point;
        }
        
        public bool HasReached(NavMeshAgent agent)
        {
            if (agent.pathPending) return false;

            return agent.remainingDistance <= agent.stoppingDistance;
            
        }
    }
}