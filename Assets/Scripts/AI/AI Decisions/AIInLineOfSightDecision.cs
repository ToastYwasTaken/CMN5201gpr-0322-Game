using UnityEngine;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Decisions/In Line of Sight")]
    public class AIInLineOfSightDecision : AIDecision
    {
        [SerializeField] private string _targetTag = "Player";
        
        public override bool Decide(AIFSMAgent stateMachine)
        {
            var enemyInLineOfSight = new AIInLineOfSight();
            Debug.Log($"AI Decision: {this.name}");
            return enemyInLineOfSight.Ping(_targetTag);
        }
    }
}

