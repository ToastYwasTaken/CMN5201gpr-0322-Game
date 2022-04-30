using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Actions/Flocking")]
    public class FlockingAction : AIStateAction
    {
        [FormerlySerializedAs("_leaderObject")]
        [FormerlySerializedAs("_destinationObject")]
        [Header("Settings")]
        [SerializeField] private string _leaderTag = "Player";
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

        private AIFlocking _flocking;
        private Vector3 _desiredVelocity = Vector3.zero;
        private NavMeshAgent _navMeshAgent;
        private GameObject _owner;

        private GameObject _leader;
        //private Vector3 LeaderPosition => _leaderTag.transform.position;
        private Vector3 OwnerPosition => _owner.transform.position;

        public override void Initialize(AIFSMAgent stateMachine)
        {
            if (OnStateEntered != null) OnStateEntered.Raise();
            _owner = stateMachine.Owner;
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            _flocking = new AIFlocking(_navMeshAgent);

            _flocking?.SetAllNeighborsWithTag(_neigbhorTag);

            _leader = GameObject.FindWithTag(_leaderTag);


        }

        public override void Execute(AIFSMAgent stateMachine)
        {
            OnUpdateSettings();
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

            // TODO: Set flocking neighbors

            ExecuteBehaviour();
        }

        private void ExecuteBehaviour()
        {
            Vector3 alignment = _flocking.CalculateAlignment(_alignmentDistance, _alignmentWeight, _maxVelocity, _navMeshAgent.speed);
            Vector3 cohesion = _flocking.CalculateCohesion(_cohesionDistance, _cohesionWeight);
            Vector3 separation = _flocking.CalculateSeparation(_separationDistance, _separationWeight, _maxVelocity, _navMeshAgent.speed);

            //Debug.Log($"Enemy: {_owner.name} > alignment: {alignment} | cohesion: {cohesion} | separation: {separation}");
            _desiredVelocity = alignment + cohesion + separation;
            
            if (_leader != null)
            {
                Vector3 leader = _leader.transform.position;
                _navMeshAgent.SetDestination(leader + _desiredVelocity);
            }
            else
            {
                _navMeshAgent.SetDestination(OwnerPosition + _desiredVelocity);
                Debug.LogWarning("AI: Flocking State has no Destination!");
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