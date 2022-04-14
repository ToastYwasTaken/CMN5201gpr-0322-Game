using System.Collections;
using System.Collections.Generic;
using AISystem;
using UnityEngine;

namespace AISystem
{
   public abstract class AIStateAction : ScriptableObject
   {
      [SerializeField] private AIConfiguration aiConfiguration;
      [HideInInspector] public bool aiConfigFoldout = true;
       
      [Header("AI Events")]
      public AIEvent OnStateEntered;
      public AIEvent OnHasReachedWaypoint;
      public AIEvent OnAgentMoveForward;
      public AIEvent OnAgentMoveBack;
      public AIEvent OnAgentTurnLeft;
      public AIEvent OnAgentTurnRight;
      public AIEvent OnAgentStopped;
      
      public abstract void Initialize(AIFSMAgent stateMachine);
      public abstract void Execute(AIFSMAgent stateMachine);

      public virtual void Exit() {}
      
      public virtual void OnUpdateSettings() {}
      
      
      /// <summary>
      /// Configuration for AI
      /// </summary>
      public AIConfiguration AIConifg
      {
         get => aiConfiguration;
         set => aiConfiguration = value;
      }
   }
}

