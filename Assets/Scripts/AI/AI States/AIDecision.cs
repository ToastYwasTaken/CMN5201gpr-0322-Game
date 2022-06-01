using UnityEngine;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AIDecision.cs
* Date : 29.05.2022
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
    /// Base for decision
    /// </summary>
    public abstract class AIDecision : ScriptableObject
    {
        public abstract bool Decide(AIFSMAgent stateMachine);
    }

}
