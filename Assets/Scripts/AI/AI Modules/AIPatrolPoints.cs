using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    public class AIPatrolPoints : MonoBehaviour
    {
        [SerializeField] private Transform[] _patrolPoints;

        private int _currentPoint = 0;
        public Transform CurrentPoint => _patrolPoints[_currentPoint];

        public Transform GetNext()
        {
            var point = _patrolPoints[_currentPoint];
            _currentPoint = (_currentPoint + 1) % _patrolPoints.Length;
            return point;
        }
        
        public bool HasReached(NavMeshAgent agent)
        {
            if (agent.pathPending) return false;

            return agent.remainingDistance <= agent.stoppingDistance;
            
        }
    }
}