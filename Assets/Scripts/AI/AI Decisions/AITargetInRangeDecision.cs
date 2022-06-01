using UnityEngine;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AITargetInRangeDecision.cs
* Date : 19.04.2022
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
    /// Checking the decision: target in range
    /// </summary>
    [CreateAssetMenu(fileName = "InRange_Decision", menuName = "AI FSM/Decisions/Target in Range")]
    public class AITargetInRangeDecision : AIDecision
    {
        private AITargetInRange _inRange;
        [SerializeField] private bool _showDebugLogs = false;
        public override bool Decide(AIFSMAgent stateMachine)
        {
            if (_showDebugLogs)
                Debug.Log($"AI Decision: {this.name}");

            _inRange = stateMachine.GetComponent<AITargetInRange>();
            if (_inRange) return _inRange.InRangeByRayCast();

            Debug.LogError($"The Component \"AITargetInRange\" is not found! " +
                           $"Please add this to the GameObject: {stateMachine.name}");
            return false;
        }
    }
}

