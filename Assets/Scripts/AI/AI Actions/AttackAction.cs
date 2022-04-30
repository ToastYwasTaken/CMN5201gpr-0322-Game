using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Actions/Attack")]
    public class AttackAction : AIStateAction
    {
        [SerializeField] private AIEvent OnCloseAttack;
        [SerializeField] private AIEvent OnFarAttack;

        [Header("Settings")]
        [SerializeField] private bool _lookToTarget = false;
        [SerializeField] private string _targetTag = "Player";
        [SerializeField] private float _fightDistanceToTarget = 7f;
        [SerializeField] private float _closeAttackDistance = 10.0f;
        [SerializeField] private bool _useFarAttack = false;
        [SerializeField] private float _farAttackDistance = 5.0f;

        // [SerializeField] private bool _agentStopByAttack = false;
        [SerializeField] private float _velocityOffset = 0.2f;

        [Header("Scan Settings")]
        [SerializeField] private float _lookRadius = 5f;
        [SerializeField] private LayerMask _ignoreLayerForScan = 0;
        [SerializeField] private QueryTriggerInteraction _queryTriggerForScan = QueryTriggerInteraction.Ignore;

        [Header("FieldOfView Settings")]
        [SerializeField] private float _viewDistance = 10f;
        [SerializeField] private float _viewAngle = 120f;
        [SerializeField] private LayerMask _ignoreLayerForView = 0;
        [SerializeField] private QueryTriggerInteraction _queryTriggerForView = QueryTriggerInteraction.Ignore;

        [Header("Aura Settings")]
        [SerializeField] private bool _useAura = true;
        [SerializeField] private float _auraRadius = 5f;



        private bool _targetIsVisible = false;
        private Collider[] _colliders;
        private GameObject _gameObject;

        private NavMeshAgent _navMeshAgent;
        private GameObject _owner;
        private AITargetInRange _targetDistance;
        private AILookToEnemy _lookToEnemy;
        private AIFieldOfView _fieldOfView;

        public override void Initialize(AIFSMAgent stateMachine)
        {
            if (OnStateEntered != null) OnStateEntered.Raise();
            Debug.LogWarning("Attack");
            _owner = stateMachine.Owner;
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            _lookToEnemy = stateMachine.GetComponent<AILookToEnemy>();
            _targetDistance = new AITargetInRange(_owner, _targetTag);
            _fieldOfView = new AIFieldOfView();




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

            Attack();

            // Verfolge das Ziel Viusel
            if (_lookToTarget && TargetIsVisible(stateMachine))
            {
                _lookToEnemy.LookAt();
            }
            else
            {
                _lookToEnemy.ResetLookAt();
            }
        }

        public override void Exit(AIFSMAgent stateMachine)
        {
            if (_lookToEnemy && _lookToTarget)
                _lookToEnemy.ResetLookAt();
        }

        private void Attack()
        {
            // Fernbereich
            if (_targetDistance.TargetInRange(_farAttackDistance) && _useFarAttack)
            {
                if (OnFarAttack != null)
                    OnFarAttack.Raise();
            }
            // Nahbereich
            else if (_targetDistance.TargetInRange(_closeAttackDistance))
            {
                if (OnCloseAttack != null)
                    OnCloseAttack.Raise();
            }
        }

        private bool TargetIsVisible(AIFSMAgent stateMachine)
        {
            var fov = new AIFieldOfView();

            _colliders = fov.LookAroundForColliders(stateMachine.transform.position, _lookRadius, _ignoreLayerForScan, _queryTriggerForScan);

            _gameObject = fov.LookForGameObject(_colliders, stateMachine.PlayerTag);

            if (_gameObject == null) return false;

            return fov.InFieldOfView(stateMachine.transform,
               _gameObject.transform, stateMachine.PlayerTag,
               _viewDistance, _viewAngle,
               _ignoreLayerForView, _queryTriggerForView,
               _auraRadius, _useAura);
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

