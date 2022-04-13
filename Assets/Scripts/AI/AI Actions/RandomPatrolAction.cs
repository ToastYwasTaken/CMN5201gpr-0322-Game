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

        private NavMeshAgent _navMeshAgent = default;

        public override void Initialize(AIFSMAgent stateMachine)
        {
            if (OnStateEntered != null) OnStateEntered.Raise();

            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        }

        public override void Execute(AIFSMAgent stateMachine)
        {
            OnUpdateSettings();
            // Debug.Log(navMeshAgent.velocity.sqrMagnitude);
            if (_navMeshAgent.velocity.sqrMagnitude >= _velocityOffset)
            {
                OnAgentMoveForward.Raise();
            }
            else 
            {
                OnAgentStopped.Raise();
            }

            var patrolPoints = new AIRandomPatrol();
            //AIRandomPatrol patrolPoints = stateMachine.GetComponent<AIRandomPatrol>();

            if (!patrolPoints.HasReached(_navMeshAgent)) return;
            if (OnHasReachedWaypoint != null) OnHasReachedWaypoint.Raise();

            if (patrolPoints.GetRandomWaypoint(_navMeshAgent.transform.position, _range, out Vector3 point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                _navMeshAgent.SetDestination(point);
            }
        }

        public override void OnUpdateSettings()
        {
            _navMeshAgent.speed = AIConifg.speed;
            _navMeshAgent.angularSpeed = AIConifg.angularSpeed;
            _navMeshAgent.acceleration = AIConifg.acceleration;
            _navMeshAgent.stoppingDistance = AIConifg.stoppingDistance;
            _navMeshAgent.autoBraking = AIConifg.autoBraking;

        }
    }
}