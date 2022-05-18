using UnityEngine;

namespace AISystem
{

    [CreateAssetMenu(fileName = "FieldOfView_Decision",menuName = "AI FSM/Decisions/In Field of View")]
    public class AIInFieldOfViewDecision : AIDecision
    {
        private AIFieldOfView _fieldOfView;

        public override bool Decide(AIFSMAgent stateMachine)
        {
            Debug.Log($"AI: {stateMachine.name} | Decision: {this.name}");
            _fieldOfView = stateMachine.GetComponent<AIFieldOfView>();
        
            if (_fieldOfView) return _fieldOfView.InFieldOfView();
            Debug.LogError($"The Component \"AIFieldOfView\" is not found! " +
                           $"Please add this to the GameObject: {stateMachine.name}");
            return false;
            
        }
    }
}

