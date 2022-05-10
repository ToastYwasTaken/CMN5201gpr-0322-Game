using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Actions/Random Patrol")]
    public class RandomPatrolAction : AIStateAction
    {
        [SerializeField] private AIEvent OnHasReachedWaypoint;

        [Header("Settings")]
        [SerializeField] private float _distanceToWaypoint = 7f;
        [SerializeField] private float _waypointDistance = 10.0f;
        [SerializeField] private float _maxDistance = 2.0f;
        [SerializeField] private float _velocityOffset = 0.2f;

        private NavMeshAgent _navMeshAgent = default;
        private AIRandomPatrol _aIRandomPatrol;
        private GameObject _owner;
        private Vector3 _ownerPosition;

        public override void Initialize(AIFSMAgent stateMachine)
        {
            if (OnStateEntered != null) OnStateEntered.Raise();

            _owner = stateMachine.Owner;
            _ownerPosition = _owner.transform.position;
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            _aIRandomPatrol = new AIRandomPatrol();
        }

        public override void Execute(AIFSMAgent stateMachine)
        {
            OnUpdateSettings();
            // Debug.Log(navMeshAgent.velocity.sqrMagnitude);
            if (_navMeshAgent.velocity.sqrMagnitude >= _velocityOffset)
            {
                if (OnAgentMoving != null)
                    OnAgentMoving.Raise();
            }
            else
            {
                if (OnAgentStopped != null)
                    OnAgentStopped.Raise();
            }

        
            //if (!_aIRandomPatrol.HasReached(_navMeshAgent)) return;

            // Sorgt für meine weichere Bewegung zwischen den Wegpunkten
            if (!_aIRandomPatrol.ChangePointByDistance(_navMeshAgent, _distanceToWaypoint)) return; 
        
            if (OnHasReachedWaypoint != null) OnHasReachedWaypoint.Raise();

            // Generiert einen neuen Wegpunkt
            if (!_aIRandomPatrol.GetRandomWaypoint(_navMeshAgent.transform.position, _waypointDistance, _maxDistance,out Vector3 point)) return;
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