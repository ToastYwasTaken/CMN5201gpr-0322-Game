using UnityEngine;

namespace AISystem
{
    public class AIBaseState : ScriptableObject
    {
        public virtual void Initialize(AIFSMAgent stateMachine) {}
        public virtual void Execute(AIFSMAgent stateMachine) {}
        public virtual void Exit(AIFSMAgent stateMachine) {}
        
    }
}

