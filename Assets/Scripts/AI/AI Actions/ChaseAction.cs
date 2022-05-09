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
        [SerializeField] private bool _lookToTarget = false;
        [SerializeField] private string _targetTag = "Player";
        [SerializeField] private LayerMask _ignoreLayer = 0;
        [SerializeField] private float _fightDistanceToTarget = 7f;
        
        [SerializeField] private float _velocityOffset = 0f;
        [SerializeField] private float _maxVelocity = 3f;
        [SerializeField] private float _seekForce = 0.005f;

        private Vector3 _velocity = Vector3.zero;

        private NavMeshAgent _navMeshAgent;
        private AILookToEnemy _lookToEnemy;

        public override void Initialize(AIFSMAgent stateMachine)
        {
            if (OnStateEntered != null) OnStateEntered.Raise();
            
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            _lookToEnemy = stateMachine.GetComponent<AILookToEnemy>();
            
            if (_lookToEnemy)
                _lookToEnemy.FindTargetWithTag(_targetTag);
            
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

            var enemySightSensor = new AIInLineOfSight(stateMachine.Owner, _targetTag, _ignoreLayer);
            // FightDistanceCheck();
            _navMeshAgent.SetDestination(enemySightSensor.Target.position);

            // Verfolge das Ziel Viusel
            if (_lookToTarget)
            _lookToEnemy.LookAtInstance();    
            //_lookToEnemy.LookAt();
        }

        public override void Exit(AIFSMAgent stateMachine)
        {
            //if (_lookToEnemy && _lookToTarget)
            //    _lookToEnemy.ResetLookAt();
        }
        
        private Vector3 CalculateSeekBehaviour()
        {
            Vector3 desiredVelocity = (Target.position - Owner.position).normalized * _maxVelocity;
            Vector3 steering = desiredVelocity - _velocity;
            return steering * _seekForce;

        }
        
        private void FightDistanceCheck()
        {
            _navMeshAgent.isStopped = _navMeshAgent.remainingDistance <= _fightDistanceToTarget;

            if (_navMeshAgent.isStopped)
            {
                //_navMeshAgent.transform.forward = Target.position;
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


