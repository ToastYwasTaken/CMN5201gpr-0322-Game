using UnityEngine;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AIBaseState.cs
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
    /// Base for AI state
    /// </summary>
    public class AIBaseState : ScriptableObject
    {
        public virtual void Initialize(AIFSMAgent stateMachine) {}
        public virtual void Execute(AIFSMAgent stateMachine) {}
        public virtual void Exit(AIFSMAgent stateMachine) {}
        
    }
}

