using UnityEngine;

namespace AISystem
{
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

