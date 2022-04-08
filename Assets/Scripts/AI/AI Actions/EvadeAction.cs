using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Actions/Evade")]
    public class EvadeAction : AIStateAction
    {
        [Header("AI Events")]
        [SerializeField] private AIEvent OnStateEntered;

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

