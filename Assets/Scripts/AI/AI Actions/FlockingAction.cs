using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Actions/Flocking")]
    public class FlockingAction : AIStateAction
    {
        [Header("Settings")]
        [SerializeField] private GameObject _destinationObject;
        [SerializeField] private string _neigbhorTag = "Enemy";
        [SerializeField] private float _maxVelocity = 7f;
        [SerializeField, Range(0f, 50f)] private float _alignmentWeight = 1f;
        [SerializeField, Range(0f, 50f)] private float _cohesionWeight = 1f;
        [SerializeField, Range(0f, 50f)] private float _separationWeight = 1f;
        [SerializeField, Range(0f, 50f)] private float _alignmentDistance = 2f;
        [SerializeField, Range(0f, 50f)] private float _cohesionDistance = 2f;
        [SerializeField, Range(0f, 50f)] private float _separationDistance = 3f;

        [Header("Event Settings")]
        [SerializeField] private float _velocityOffset = 0.2f;

        private AIFlocking flocking;
        private Vector3 _desiredVelocity = Vector3.zero;
        private NavMeshAgent _navMeshAgent;
        private GameObject _owner;
        public Vector3 AgentDestination { get; set; }

        public override void Initialize(AIFSMAgent stateMachine)
        {
            if (OnStateEntered != null) OnStateEntered.Raise();
            _owner = stateMachine.Owner;
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            flocking = new AIFlocking(_owner);

            if (flocking == null) return;
            flocking.SetAllNeighborsWithTag(_neigbhorTag);
            
            if (!_destinationObject)
            {
                Debug.LogWarning("AI: Flocking State has no Destination!");
            }
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

            // TODO: Set flocking neighbors

            ExecuteBehaviour();
        }

        private void ExecuteBehaviour()
        {
            Vector3 alignment = flocking.CalculateAlignment(_alignmentDistance, _alignmentWeight, _maxVelocity, _navMeshAgent.speed);
            Vector3 cohesion = flocking.CalculateCohesion(_cohesionDistance, _cohesionWeight);
            Vector3 separation = flocking.CalculateSeparation(_separationDistance, _separationWeight, _maxVelocity, _navMeshAgent.speed);
            Vector3 ownerPosition = _navMeshAgent.transform.position;

            _desiredVelocity = alignment + cohesion + separation;
            _navMeshAgent.SetDestination(ownerPosition + _desiredVelocity);
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