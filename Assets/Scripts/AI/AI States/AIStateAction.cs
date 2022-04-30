using UnityEngine;

namespace AISystem
{
    public abstract class AIStateAction : ScriptableObject
    {
        [SerializeField] private AIConfiguration _aiConfiguration;
        [HideInInspector] public bool AiConfigFoldout = true;

        [Header("AI Events")]
        public AIEvent OnStateEntered;
        public AIEvent OnAgentMoving;
        public AIEvent OnAgentStopped;
        public AIEvent OnStateExit;

        public abstract void Initialize(AIFSMAgent stateMachine);
        public abstract void Execute(AIFSMAgent stateMachine);

        public virtual void Exit(AIFSMAgent stateMachine) 
        {
            if (OnStateExit != null)
                OnStateExit.Raise();
        }

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

