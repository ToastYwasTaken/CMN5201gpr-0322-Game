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
        [SerializeField] private bool _lookToTarget = true;
        [SerializeField] private string _targetTag = "Player";
        [SerializeField] private float _fightDistanceToTarget = 7f;
        [SerializeField] private float _maxVelocity = 3f;
        [SerializeField] private float _seekForce = 0.005f;

        private Vector3 _velocity = Vector3.zero;

        private NavMeshAgent _navMeshAgent;
        private AILookToEnemy _lookToEnemy;
        private AITargetInRange _targetInRange;

        public override void Initialize(AIFSMAgent stateMachine)
        {
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            _lookToEnemy = stateMachine.GetComponent<AILookToEnemy>();
            _targetInRange = stateMachine.GetComponent<AITargetInRange>();
            
            if (_lookToEnemy)
                _lookToEnemy.FindTargetWithTag(_targetTag);
            
        }

        public override void Execute(AIFSMAgent stateMachine)
        {  
            if (_navMeshAgent == null) return;
            OnUpdateSettings();
            
            if (!_targetInRange.InRangeByDistance(_fightDistanceToTarget)) return;
      
            // FightDistanceCheck();
            _navMeshAgent.SetDestination(_targetInRange.Target.transform.position);

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
            if (_navMeshAgent == null) return;
            _navMeshAgent.speed = AIConifg.speed;
            _navMeshAgent.angularSpeed = AIConifg.angularSpeed;
            _navMeshAgent.acceleration = AIConifg.acceleration;
            _navMeshAgent.stoppingDistance = AIConifg.stoppingDistance;
            _navMeshAgent.autoBraking = AIConifg.autoBraking;

        }
    }
}


