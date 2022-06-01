using UnityEngine;
using UnityEngine.AI;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : ChaseAction.cs
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
    /// Determines the action behavior chasing
    /// </summary>
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

        private NavMeshAgent _navMeshAgent;
        private AILookToEnemy _lookToEnemy;
        private AITargetInRange _targetInRange;

        /// <summary>
        /// Initialize state
        /// </summary>
        /// <param name="stateMachine"></param>
        public override void Initialize(AIFSMAgent stateMachine)
        {
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            _lookToEnemy = stateMachine.GetComponent<AILookToEnemy>();
            _targetInRange = stateMachine.GetComponent<AITargetInRange>();
            Owner = stateMachine.Owner.transform;

            // Search for GameObject passed tag
            if (_lookToEnemy)
            {
                _lookToEnemy.FindTargetWithTag(_targetTag);
                Target = _lookToEnemy.Target.transform;
            }
        }

        /// <summary>
        /// Execute state
        /// </summary>
        /// <param name="stateMachine"></param>
        public override void Execute(AIFSMAgent stateMachine)
        {
            if (_navMeshAgent == null || !_navMeshAgent.isOnNavMesh) return;

            OnUpdateSettings();

            // Is target in range
            if (!_targetInRange.InRangeByDistance(_fightDistanceToTarget)) return;

            // Seek value
            Vector3 point = CalculateSeekBehaviour();
            // Debug.Log($"Point: {point}");

            // Set agent destination 
            _=_navMeshAgent.SetDestination(_targetInRange.Target.transform.position + point);

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
        /// Calculate Seek
        /// </summary>
        /// <returns></returns>
        private Vector3 CalculateSeekBehaviour()
        {
            Vector3 desiredVelocity = (Target.position - Owner.position).normalized * _maxVelocity;
            Vector3 steering = desiredVelocity - _navMeshAgent.velocity;
            return steering * _seekForce;
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


