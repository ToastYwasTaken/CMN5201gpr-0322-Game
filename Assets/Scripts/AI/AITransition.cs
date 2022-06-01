using UnityEngine;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AITransition.cs
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
    /// Define transitions
    /// </summary>
    [CreateAssetMenu(fileName = "AI_Transition", menuName = "AI FSM/Transition")]
    public sealed class AITransition : ScriptableObject
    {
        public AIDecision Decision;
        public AIBaseState IsTrue;
        public AIBaseState IsFalse;
        public bool RemainInState = false;

        /// <summary>
        /// Checking decision
        /// </summary>
        /// <param name="stateMachine"></param>
        public void Execute(AIFSMAgent stateMachine)
        {
            if (Decision.Decide(stateMachine) && RemainInState is not true)
            {
                stateMachine.CurrentState = IsTrue;
            }
            else if (RemainInState is not true)
            {
                stateMachine.CurrentState = IsFalse;
            }
        }
    }

}
