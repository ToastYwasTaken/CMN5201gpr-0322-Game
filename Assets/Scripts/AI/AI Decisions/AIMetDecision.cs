using UnityEngine;

namespace AISystem
{
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

            if (_met) return _met.AmMet();
            
            Debug.LogError($"The Component \"AIMet\" is not found! " +
                           $"Please add this to the GameObject: {stateMachine.name}");
            return false;
        }
    } 
}

