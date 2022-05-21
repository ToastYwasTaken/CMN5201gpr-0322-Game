using UnityEngine;

namespace AISystem
{
    [CreateAssetMenu(fileName = "AI_Transition", menuName = "AI FSM/Transition")]
  public sealed class AITransition : ScriptableObject
  {
    public AIDecision Decision;
    public AIBaseState IsTrue;
    public AIBaseState IsFalse;
    public bool RemainInState = false;
  
    public void Execute(AIFSMAgent stateMachine)
    {
      if (Decision.Decide(stateMachine) && RemainInState is not true)
      {
        stateMachine.CurrentState = IsTrue;
      }
      else if (RemainInState is not true)
      {
        stateMachine.CurrentState = IsFalse;
      }
    }
  }

}
