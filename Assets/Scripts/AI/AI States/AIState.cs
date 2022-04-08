using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/State")]
    public sealed class AIState : AIBaseState
    {
        public List<AIStateAction> AIActions = new();
        public List<AITransition> AITransitions = new();

        public override void Initialize(AIFSMAgent stateMachine)
        {
           AIActions[0].Initialize(stateMachine);
        }

        public override void Execute(AIFSMAgent stateMachine)
        {
            foreach (var action in AIActions)
            {
                action.Execute(stateMachine);
            }

            foreach (var transition in AITransitions)
            {
                transition.Execute(stateMachine);
            }
        }
    }
}

