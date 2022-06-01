using UnityEngine;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AIMetDecision.cs
* Date : 31.05.2022
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
    /// Checking the decision: am met
    /// </summary>
    [CreateAssetMenu(fileName = "Met_Decision", menuName = "AI FSM/Decisions/Met")]
    public class AIMetDecision : AIDecision
    {
        private AIMet _met;
        [SerializeField] private bool _showDebugLogs = false;
        public override bool Decide(AIFSMAgent stateMachine)
        {
            if (_showDebugLogs)
                Debug.Log($"AI: {stateMachine.name} | Decision: {this.name}");
            _met = stateMachine.GetComponent<AIMet>();


            Debug.LogWarning($"Hit: {_met.HitDetected()}");
            if (_met) return _met.HitDetected();
            Debug.LogError($"The Component \"AIMet\" is not found! " +
                           $"Please add this to the GameObject: {stateMachine.name}");
            return false;
        }
    }
}

