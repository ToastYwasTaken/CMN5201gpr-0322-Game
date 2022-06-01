using UnityEngine;
using UnityEngine.AI;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : RandomPatrolAction.cs
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
    /// Determines the behavior random patrol
    /// </summary>
    [CreateAssetMenu(menuName = "AI FSM/Actions/Random Patrol")]
    public class RandomPatrolAction : AIStateAction
    {
        [SerializeField] private AIEvent OnHasReachedWaypoint;

        [Header("Settings")]
        [SerializeField] private float _distanceToWaypoint = 7f;
        [SerializeField] private float _waypointDistance = 10.0f;
        [SerializeField] private float _maxDistance = 2.0f;

        private NavMeshAgent _navMeshAgent = default;
        private AIRandomPatrol _aIRandomPatrol;

        /// <summary>
        /// Initialize state
        /// </summary>
        /// <param name="stateMachine"></param>
        public override void Initialize(AIFSMAgent stateMachine)
        {
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            _aIRandomPatrol = new AIRandomPatrol();
        }

        /// <summary>
        /// Execute state
        /// </summary>
        /// <param name="stateMachine"></param>
        public override void Execute(AIFSMAgent stateMachine)
        {  
            if (_navMeshAgent == null || !_navMeshAgent.isOnNavMesh) return;
            OnUpdateSettings();
 
            // Provides smoother movement between waypoints
            if (!_aIRandomPatrol.HasReachedByDistance(_navMeshAgent, _distanceToWaypoint)) return;

            // EVENT: Call event for has reached waypoint
            if (OnHasReachedWaypoint != null) OnHasReachedWaypoint.Raise();

            // Generates a new waypoint
            if (!_aIRandomPatrol.GetRandomWaypoint(_navMeshAgent.transform.position, 
                _waypointDistance, _maxDistance,out Vector3 point)) return;
            
            Debug.DrawRay(point, Vector3.up, Color.magenta, 3.0f);

            // Set agent destination 
            _=_navMeshAgent.SetDestination(point);
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