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
        [SerializeField] private float _closeAttackDistance = 10.0f;
        [SerializeField] private bool _useFarAttack = false;
        [SerializeField] private float _farAttackDistance = 5.0f;

        private NavMeshAgent _navMeshAgent;
        private GameObject _owner;
        private AITargetInRange _targetDistance;
        private AILookToEnemy _lookToEnemy;
 

        public override void Initialize(AIFSMAgent stateMachine)
        {
         
            _owner = stateMachine.Owner;
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            _lookToEnemy = stateMachine.GetComponent<AILookToEnemy>();
            _targetDistance = stateMachine.GetComponent<AITargetInRange>();

            if (_lookToEnemy)
                _lookToEnemy.FindTargetWithTag(_targetTag);
        }


        public override void Execute(AIFSMAgent stateMachine)
        {
            
            if (_navMeshAgent == null || !_navMeshAgent.isOnNavMesh) return;
            OnUpdateSettings();
            
            // Attack
            Attack();
  
            // Verfolge das Ziel Viusel
            if (_lookToTarget)
            {
                //_navMeshAgent.updateRotation = false;
                //_lookToEnemy.LookAt();
                _lookToEnemy.LookAtTarget();    
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
            if (_targetDistance.InRangeByDistance(_farAttackDistance) && _useFarAttack)
            {
                if (OnFarAttack != null)
                    OnFarAttack.Raise(); 
                Debug.Log($"{_owner.name}: execute far attack");
            }
            // Nahbereich
            else if (_targetDistance.InRangeByDistance(_closeAttackDistance))
            {
                if (OnCloseAttack != null)
                    OnCloseAttack.Raise();
                Debug.Log($"{_owner.name}: execute close attack");
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

