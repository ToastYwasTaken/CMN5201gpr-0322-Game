using UnityEngine;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Decisions/In Line of Sight")]
    public class AIInLineOfSightDecision : AIDecision
    {
        public override bool Decide(AIFSMAgent stateMachine)
        {
            var enemyInLineOfSight = ((Component)stateMachine).GetComponent<AIInLineOfSight>();
            Debug.Log($"AI Decision: {this.name}");
            return enemyInLineOfSight.Ping();
        }
    }
}

