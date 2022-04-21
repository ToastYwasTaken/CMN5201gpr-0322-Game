using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Decisions/Target in Range")]
   public class AITargetInRangeDecision : AIDecision
   {
       [SerializeField] private string _targetTag = "Player";
       [SerializeField] private float _range = 5f;

       private GameObject _owner;
       
       public override bool Decide(AIFSMAgent stateMachine)
       {
           Debug.Log($"AI Decision: {this.name}");
           
           _owner = stateMachine.Owner;
           
           var inRange = new AITargetInRange(_owner, _targetTag);
           return inRange.TargetInRange(_range);
       }
   } 
}

