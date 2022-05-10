using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    public class AIRandomPatrol 
    {
        public bool GetRandomWaypoint(Vector3 center, float range, float maxDistance, out Vector3 result)
        {
            Vector3 rndPoint = center + (Random.insideUnitSphere* range);
            if (NavMesh.SamplePosition(rndPoint, out NavMeshHit hit, 
                maxDistance, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
        }

        public bool HasReached(NavMeshAgent agent)
        {
            if (agent.pathPending) return false;

            if (!(agent.remainingDistance <= agent.stoppingDistance)) return false;
            return !agent.hasPath || agent.velocity.sqrMagnitude == 0f;
        }

        public bool ChangePointByDistance(NavMeshAgent agent, float distance)
        {
            Vector3 currentTarget = agent.destination;

            float remainingDistance = Vector3.Distance(agent.transform.position, currentTarget);
            Debug.Log(remainingDistance);
            return remainingDistance <= distance;
        }
    }
}