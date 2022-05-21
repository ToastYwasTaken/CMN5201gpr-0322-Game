using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    [CreateAssetMenu(fileName= "AI_State", menuName = "AI FSM/State")]
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
            foreach (AIStateAction action in AIActions)
            {
                action.Execute(stateMachine);
            }

            foreach (AITransition transition in AITransitions)
            {
                transition.Execute(stateMachine);
            }
        }

        public override void Exit(AIFSMAgent stateMachine)
        {
            AIActions[0].Exit(stateMachine);
        }
    }
}

