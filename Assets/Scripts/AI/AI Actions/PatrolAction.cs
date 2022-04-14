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

        private NavMeshAgent navMeshAgent;

        public override void Initialize(AIFSMAgent stateMachine)
        {
            if (OnStateEntered != null) OnStateEntered.Raise();

            navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        }

        public override void Execute(AIFSMAgent stateMachine)
        {
            OnUpdateSettings();

            if (navMeshAgent.velocity.sqrMagnitude >= _velocityOffset)
            {
                OnAgentMoveForward.Raise();
            }
            else
            {
                OnAgentStopped.Raise();
            }
            //var navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            var patrol = new AIPatrolPoints
            {
                PatrolPoints = _patrolPoints
            };

            if (!patrol.HasReached(navMeshAgent)) return;
            if (OnHasReachedWaypoint != null) OnHasReachedWaypoint.Raise();

            navMeshAgent.SetDestination(patrol.GetNext().position);


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

