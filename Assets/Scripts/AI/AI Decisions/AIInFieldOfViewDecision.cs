using UnityEngine;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AIInFieldOfViewDecision.cs
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
    /// Checking the decision: In field of view
    /// </summary>
    [CreateAssetMenu(fileName = "FieldOfView_Decision", menuName = "AI FSM/Decisions/In Field of View")]
    public class AIInFieldOfViewDecision : AIDecision
    {
        private AIFieldOfView _fieldOfView;
        [SerializeField] private bool _showDebugLogs = false;
        public override bool Decide(AIFSMAgent stateMachine)
        {
            if (_showDebugLogs)
                Debug.Log($"AI: {stateMachine.name} | Decision: {this.name}");
            _fieldOfView = stateMachine.GetComponent<AIFieldOfView>();

            if (_fieldOfView) return _fieldOfView.InFieldOfView() || _fieldOfView.HitDetected();
            Debug.LogError($"The Component \"AIFieldOfView\" is not found! " +
                           $"Please add this to the GameObject: {stateMachine.name}");
            return false;

        }
    }
}

