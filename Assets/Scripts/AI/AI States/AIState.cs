using System.Collections.Generic;
using UnityEngine;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AIState.cs
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
    /// Define the AI state
    /// </summary>
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

