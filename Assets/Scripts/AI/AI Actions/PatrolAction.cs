using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{

    [CreateAssetMenu(menuName = "AI FSM/Actions/Patrol")]
    public class PatrolAction : AIStateAction
    {
        [Header("Settings")] 
        [SerializeField] private Transform[] _patrolPoints = default;
        [SerializeField] private float _velocityOffset = 0f;

        // [Header("AI Events")]
        // [SerializeField] private AIEvent OnStateEntered;
        // [SerializeField] private AIEvent OnHasReachedWaypoint;
        // [SerializeField] private AIEvent OnAgentMoveForward;
        // [SerializeField] private AIEvent OnAgentMoveBack;
        // [SerializeField] private AIEvent OnAgentTurnLeft;
        // [SerializeField] private AIEvent OnAgentTurnRight;
        // [SerializeField] private AIEvent OnAgentStopped;

        private NavMeshAgent _navMeshAgent;

        public override void Initialize(AIFSMAgent stateMachine)
        {
            if (OnStateEntered != null) OnStateEntered.Raise();

            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        }

        public override void Execute(AIFSMAgent stateMachine)
        {
            OnUpdateSettings();

            if (_navMeshAgent.velocity.sqrMagnitude >= _velocityOffset)
            {
                if (OnAgentMoveForward != null)
                    OnAgentMoveForward.Raise();
            }
            else 
            {
                if (OnAgentStopped != null)
                    OnAgentStopped.Raise();
            }
            //var navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            var patrol = new AIPatrolPoints
            {
                PatrolPoints = _patrolPoints
            };

            if (!patrol.HasReached(_navMeshAgent)) return;
            if (OnHasReachedWaypoint != null) OnHasReachedWaypoint.Raise();

            _navMeshAgent.SetDestination(patrol.GetNext().position);


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

