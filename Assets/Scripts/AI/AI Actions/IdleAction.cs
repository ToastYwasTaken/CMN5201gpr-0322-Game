using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Actions/Idle")]
    public class IdleAction : AIStateAction
    {
        private NavMeshAgent _navMeshAgent;

        public override void Initialize(AIFSMAgent stateMachine)
        {
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        }

        public override void Execute(AIFSMAgent stateMachine)
        { 
            if (_navMeshAgent == null) return;
            OnUpdateSettings();
            
            _navMeshAgent.isStopped = true;
        }

        public override void Exit(AIFSMAgent stateMachine)
        {
            _navMeshAgent.isStopped = false;
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

