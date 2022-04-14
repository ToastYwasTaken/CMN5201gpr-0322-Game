using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Actions/Attack")]
    public class AttackAction : AIStateAction
    {
        [Header("Settings")]
        [SerializeField] private float attackDistance = 10.0f;

        // [Header("AI Events")]
        // [SerializeField] private AIEvent OnStateEntered;
        // [SerializeField] private AIEvent OnPlayerIsInReach;
        // [SerializeField] private AIEvent OnPlayerAttack;
        


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


