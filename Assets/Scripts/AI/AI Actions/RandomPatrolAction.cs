using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Actions/Random Patrol")]
    public class RandomPatrolAction : AIStateAction
    {
        [SerializeField] private AIEvent OnHasReachedWaypoint;

        [Header("Settings")]
        [SerializeField] private float _range = 10.0f;
        [SerializeField] private float _velocityOffset = 0.2f;

        private NavMeshAgent _navMeshAgent = default;
        private GameObject _owner;
        private Vector3 _ownerPosition;

        public override void Initialize(AIFSMAgent stateMachine)
        {
            if (OnStateEntered != null) OnStateEntered.Raise();

            _owner = stateMachine.Owner;
            _ownerPosition = _owner.transform.position;
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        }

        public override void Execute(AIFSMAgent stateMachine)
        {
            OnUpdateSettings();
            // Debug.Log(navMeshAgent.velocity.sqrMagnitude);
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

            var patrolPoints = new AIRandomPatrol();


            if (!patrolPoints.HasReached(_navMeshAgent)) return;
            if (OnHasReachedWaypoint != null) OnHasReachedWaypoint.Raise();

            if (!patrolPoints.GetRandomWaypoint(_navMeshAgent.transform.position, _range, out Vector3 point)) return;
            Debug.DrawRay(point, Vector3.up, Color.magenta, 1.0f);

            // TODO line of Sight check

            _navMeshAgent.SetDestination(point);
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