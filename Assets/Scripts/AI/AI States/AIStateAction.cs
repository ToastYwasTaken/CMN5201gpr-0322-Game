using UnityEngine;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AIStateAction.cs
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
    /// Define the AI state action
    /// </summary>
    public abstract class AIStateAction : ScriptableObject
    {
        [SerializeField] private AIConfiguration _aiConfiguration;
        [HideInInspector] public bool AiConfigFoldout = true;
        public abstract void Initialize(AIFSMAgent stateMachine);
        public abstract void Execute(AIFSMAgent stateMachine);

        public virtual void Exit(AIFSMAgent stateMachine) {}
        
        public virtual void OnUpdateSettings() { }


        /// <summary>
        /// Configuration for AI
        /// </summary>
        public AIConfiguration AIConifg
        {
            get => _aiConfiguration;
            set => _aiConfiguration = value;
        }
    }
}

