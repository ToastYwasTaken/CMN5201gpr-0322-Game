using System.Collections;
using System.Collections.Generic;
using AISystem;
using UnityEngine;

namespace AISystem
{
  [CreateAssetMenu(menuName = "AI FSM/Transition")]
  public sealed class AITransition : ScriptableObject
  {
    public AIDecision Decision;
    public AIBaseState IsTrue;
    public AIBaseState IsFalse; 
  
    public void Execute(AIFSMAgent stateMachine)
    {
      if (Decision.Decide(stateMachine) && IsTrue is not AIRemainInState)
      {
        stateMachine.CurrentState = IsTrue;
      }
      else if (IsFalse is not AIRemainInState)
      {
        stateMachine.CurrentState = IsFalse;
      }
    }
  }

}
