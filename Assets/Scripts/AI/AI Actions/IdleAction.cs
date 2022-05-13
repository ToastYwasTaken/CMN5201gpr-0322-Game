using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Actions/Idle")]
    public class IdleAction : AIStateAction
    {
        [Header("Settings")]
        [SerializeField] private float _velocityOffset = 0.2f;
        
        private NavMeshAgent _navMeshAgent;
      
        
        public override void Initialize(AIFSMAgent stateMachine)
        {
            if (OnStateEntered != null) OnStateEntered.Raise();
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        }

        public override void Execute(AIFSMAgent stateMachine)
        {
            OnUpdateSettings();
             if (_navMeshAgent == null) return;
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

