using System.Collections;
using System.Collections.Generic;
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

        public override void Initialize(AIFSMAgent stateMachine)
        {
            if (OnStateEntered != null) OnStateEntered.Raise();
            Debug.LogWarning("Attack");
            _owner = stateMachine.Owner;
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            _targetDistance = new AITargetInRange(_owner, _targetTag);
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

            Attack();
            // FightDistanceCheck();
            
        }

        private void FightDistanceCheck()
        {
            _navMeshAgent.isStopped = _targetDistance.TargetInRange(_fightDistanceToTarget);
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

