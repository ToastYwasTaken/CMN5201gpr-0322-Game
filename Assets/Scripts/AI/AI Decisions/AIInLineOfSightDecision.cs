using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Decisions/In Line of Sight")]
    public class AIInLineOfSightDecision : AIDecision
    {
        [SerializeField] private string _targetTag = "Player";
        [SerializeField] private LayerMask _ignoreLayer = 0;

        private GameObject _owner;
        
        public override bool Decide(AIFSMAgent stateMachine)
        {
            Debug.Log($"AI Decision: {this.name}");

            _owner = stateMachine.Owner;
            
            var enemyInLineOfSight = new AIInLineOfSight(_owner, _targetTag, _ignoreLayer);
            return enemyInLineOfSight.Ping(_targetTag);
        }
    }
}

