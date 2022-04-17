using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Actions/Evade")]
    public class EvadeAction : AIStateAction
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
        }
    }
}

