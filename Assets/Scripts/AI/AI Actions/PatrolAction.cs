using UnityEngine;
using UnityEngine.AI;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : PatrolAction.cs
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
    /// Determines the action behavior patrol
    /// </summary>
    [CreateAssetMenu(menuName = "AI FSM/Actions/Patrol")]
    public class PatrolAction : AIStateAction
    {
        [SerializeField] private AIEvent OnHasReachedWaypoint;
        
        [Header("Settings")] 
        [SerializeField] private Transform[] _patrolPoints = default;

        private NavMeshAgent _navMeshAgent;

        /// <summary>
        /// Initialize state
        /// </summary>
        /// <param name="stateMachine"></param>
        public override void Initialize(AIFSMAgent stateMachine)
        {
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        }

        /// <summary>
        /// Execute state
        /// </summary>
        /// <param name="stateMachine"></param>
        public override void Execute(AIFSMAgent stateMachine)
        {
            if (_navMeshAgent == null || !_navMeshAgent.isOnNavMesh) return;
            OnUpdateSettings();
            
            // Get patrol points
            var patrol = new AIPatrolPoints
            {
                PatrolPoints = _patrolPoints
            };

            //  Check is point has reached
            if (!patrol.HasReached(_navMeshAgent)) return;
            // EVENT: Call event for has reached waypoint
            if (OnHasReachedWaypoint != null) OnHasReachedWaypoint.Raise();

            // Set agent destination 
            _=_navMeshAgent.SetDestination(patrol.GetNext().position);
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

