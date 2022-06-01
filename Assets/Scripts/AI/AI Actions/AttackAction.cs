using UnityEngine;
using UnityEngine.AI;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AttackAction.cs
* Date : 09.04.2022
* Author : René Kraus (RK)
*
* These coded instructions, statements, and computer programs contain
* proprietary information of the author and are protected by Federal
* copyright law. They may not be disclosed to third parties or copied
* or duplicated in any form, in whole or in part, without the prior
* written consent of the author.
******************************************************************************/
namespace AISystem
{
    /// <summary>
    /// Determines the action behavior attacking
    /// </summary>
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

        /// <summary>
        /// Initialize state
        /// </summary>
        /// <param name="stateMachine"></param>
        public override void Initialize(AIFSMAgent stateMachine)
        {
            _owner = stateMachine.Owner;
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            _lookToEnemy = stateMachine.GetComponent<AILookToEnemy>();
            _targetDistance = stateMachine.GetComponent<AITargetInRange>();

            // Search for GameObject passed tag
            if (_lookToEnemy)
                _lookToEnemy.FindTargetWithTag(_targetTag);
        }

        /// <summary>
        /// Execute state
        /// </summary>
        /// <param name="stateMachine"></param>
        public override void Execute(AIFSMAgent stateMachine)
        {
            if (_navMeshAgent == null || !_navMeshAgent.isOnNavMesh) return;
            OnUpdateSettings();

            // call attack check
            Attack();

            // Track the target viusel
            if (_lookToTarget)
                _lookToEnemy.LookAtTarget();
        }

        /// <summary>
        /// Leave state
        /// </summary>
        /// <param name="stateMachine"></param>
        public override void Exit(AIFSMAgent stateMachine) { }

        /// <summary>
        /// Determines attack behavior between near and far combat
        /// </summary>
        private void Attack()
        {
            // far range
            if (_targetDistance.InRangeByDistance(_farAttackDistance) && _useFarAttack)
            {
                // EVENT: Call event for far attack
                if (OnFarAttack != null) OnFarAttack.Raise();
                    
                Debug.Log($"{_owner.name}: execute far attack");
            }
            // close range
            else if (_targetDistance.InRangeByDistance(_closeAttackDistance))
            {
                // EVENT: Call event for close attack
                if (OnCloseAttack != null) OnCloseAttack.Raise();
                    
                Debug.Log($"{_owner.name}: execute close attack");
            }
        }

        public override void OnUpdateSettings()
        {
            if (_navMeshAgent == null) return;
            _navMeshAgent.speed = AIConifg.Speed;
            _navMeshAgent.angularSpeed = AIConifg.AngularSpeed;
            _navMeshAgent.acceleration = AIConifg.Acceleration;
            _navMeshAgent.stoppingDistance = AIConifg.StoppingDistance;
            _navMeshAgent.autoBraking = AIConifg.AutoBraking;

        }
    }
}

