using UnityEngine;

namespace AISystem
{
    public abstract class AIDecision : ScriptableObject
    {
        public abstract bool Decide(AIFSMAgent stateMachine);
    }

}
