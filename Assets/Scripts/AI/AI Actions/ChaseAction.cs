using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Actions/Chase")]
    public class ChaseAction : AIStateAction
    {
        public Transform Owner { get; set; }
        public Transform Target { get; set; }

        [Header("Settings")]
        [SerializeField] private float _velocityOffset = 0f;
        [SerializeField] private float _maxVelocity = 3f;
        [SerializeField] private float _seekForce = 0.005f;

        private Vector3 _velocity = Vector3.zero;

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
                OnAgentMoveForward.Raise();
            }
            else
            {
                OnAgentStopped.Raise();
            }


            var enemySightSensor = new AIInLineOfSight();
            {
                Owner = stateMachine.Owner.transform;
                
            };

            _navMeshAgent.SetDestination(enemySightSensor.Target.position);
        }

        private Vector3 CalculateSeekBehaviour()
        {
            Vector3 desiredVelocity = (Target.position - Owner.position).normalized * _maxVelocity;
            Vector3 steering = desiredVelocity - _velocity;
            return steering * _seekForce;

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


