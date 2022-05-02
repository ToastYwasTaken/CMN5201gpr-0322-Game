using TMPro;
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

            //Attack();


            // Verfolge das Ziel Viusel
            if (_lookToTarget)
            {
                //_navMeshAgent.updateRotation = false;
                _lookToEnemy.LookAt();
            }
            //else
            //{
            //    _lookToEnemy.ResetLookAt();
            //    Debug.Log("Reset");
            //}

        }

        public override void Exit(AIFSMAgent stateMachine)
        {
            //if (_lookToEnemy && _lookToTarget)
            //    _lookToEnemy.ResetLookAt();
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

