using UnityEngine;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Actions/Idle")]
    public class IdleAction : AIStateAction
    {
       // [Header("AI Events")]
       // [SerializeField] private AIEvent OnStateEntered;

        public override void Initialize(AIFSMAgent stateMachine)
        {
            if (OnStateEntered != null) OnStateEntered.Raise();
        }

        public override void Execute(AIFSMAgent stateMachine)
        {
            throw new System.NotImplementedException();
        }
    }
}

