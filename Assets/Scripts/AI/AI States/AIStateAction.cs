using UnityEngine;

namespace AISystem
{
    public abstract class AIStateAction : ScriptableObject
    {
        [SerializeField] private AIConfiguration _aiConfiguration;
        [HideInInspector] public bool AiConfigFoldout = true;

        [Header("AI Events")]
        public AIEvent OnStateEntered;
        public AIEvent OnAgentMoveForward;
        public AIEvent OnAgentMoveBack;
        public AIEvent OnAgentTurnLeft;
        public AIEvent OnAgentTurnRight;
        public AIEvent OnAgentStopped;

        public abstract void Initialize(AIFSMAgent stateMachine);
        public abstract void Execute(AIFSMAgent stateMachine);

        public virtual void Exit(AIFSMAgent stateMachine) { }

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

