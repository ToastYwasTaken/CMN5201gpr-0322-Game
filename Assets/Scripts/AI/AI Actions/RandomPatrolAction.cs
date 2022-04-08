using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Actions/Random Patrol")]
    public class RandomPatrolAction : AIStateAction
    {
        [Header("Settings")]
        [SerializeField] private float _range = 10.0f;
        [SerializeField] private float _velocityOffset = 0.2f;

        [Header("AI Events")]
        [SerializeField] private AIEvent OnStateEntered;
        [SerializeField] private AIEvent OnHasReachedWaypoint;
        [SerializeField] private AIEvent OnAgentMoveForward;
        [SerializeField] private AIEvent OnAgentMoveBack;
        [SerializeField] private AIEvent OnAgentTurnLeft;
        [SerializeField] private AIEvent OnAgentTurnRight;
        [SerializeField] private AIEvent OnAgentStopped;

        private NavMeshAgent navMeshAgent;

        public override void Initialize(AIFSMAgent stateMachine)
        {
            if (OnStateEntered != null) OnStateEntered.Raise();

            navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        }

        public override void Execute(AIFSMAgent stateMachine)
        {
            OnUpdateSettings();
            // Debug.Log(navMeshAgent.velocity.sqrMagnitude);
            if (navMeshAgent.velocity.sqrMagnitude >= _velocityOffset)
            {
                OnAgentMoveForward.Raise();
            }
            else 
            {
                OnAgentStopped.Raise();
            }

            var patrolPoints = stateMachine.GetComponent<AIRandomPatrol>();

            if (!patrolPoints.HasReached(navMeshAgent)) return;
            if (OnHasReachedWaypoint != null) OnHasReachedWaypoint.Raise();

            if (patrolPoints.GetRandomWaypoint(navMeshAgent.transform.position, _range, out var point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                navMeshAgent.SetDestination(point);
            }
        }

        public override void OnUpdateSettings()
        {
            navMeshAgent.speed = AIConifg.speed;
            navMeshAgent.angularSpeed = AIConifg.angularSpeed;
            navMeshAgent.acceleration = AIConifg.acceleration;
            navMeshAgent.stoppingDistance = AIConifg.stoppingDistance;
            navMeshAgent.autoBraking = AIConifg.autoBraking;

        }
    }
}