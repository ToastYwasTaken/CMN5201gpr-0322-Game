using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{

    [CreateAssetMenu(menuName = "AI FSM/Actions/Patrol")]
    public class PatrolAction : AIStateAction
    {
        [SerializeField] private AIEvent OnHasReachedWaypoint;
        
        [Header("Settings")] 
        [SerializeField] private Transform[] _patrolPoints = default;
        [SerializeField] private float _velocityOffset = 0f;

 
        private NavMeshAgent _navMeshAgent;

        public override void Initialize(AIFSMAgent stateMachine)
        {
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        }

        public override void Execute(AIFSMAgent stateMachine)
        {
            if (_navMeshAgent == null) return;
            OnUpdateSettings();
            
            //var navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            var patrol = new AIPatrolPoints
            {
                PatrolPoints = _patrolPoints
            };

            if (!patrol.HasReached(_navMeshAgent)) return;
            if (OnHasReachedWaypoint != null) OnHasReachedWaypoint.Raise();

            _navMeshAgent.SetDestination(patrol.GetNext().position);


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

