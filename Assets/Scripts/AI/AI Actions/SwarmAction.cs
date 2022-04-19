using System.Collections;
using System.Collections.Generic;
using AISystem;
using UnityEngine;
using UnityEngine.AI;

namespace  AISystem
{
   [CreateAssetMenu(menuName = "AI FSM/Actions/Swarm")]
   public class SwarmAction : AIStateAction
   {

      [Header("Settings")] 
      [SerializeField] private GameObject _swarmleader;
      [SerializeField] private string _neighborTag = "Enemy";

      [SerializeField] private float _distanceToLeader = 2f;
      [SerializeField] private float _maxVelocity = 3f;
      [SerializeField] private float _maxSeparation = 2f;
      [SerializeField] private float _separationDistance = 2f;
      [SerializeField] private float _velocityOffset = 0.2f;
      
      private NavMeshAgent _navMeshAgent;
      private GameObject _owner;


      public override void Initialize(AIFSMAgent stateMachine)
      {
         if (OnStateEntered != null) OnStateEntered.Raise();

         _owner = stateMachine.Owner;
         _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();

      }

      public override void Execute(AIFSMAgent stateMachine)
      {
         OnUpdateSettings();  
         if (_navMeshAgent.velocity.sqrMagnitude >= _velocityOffset)
         {
            if (OnAgentMoveForward != null)
               OnAgentMoveForward.Raise();
         }
         else 
         {
            if (OnAgentStopped != null)
               OnAgentStopped.Raise();
         }

         var swarm = new AISwarm(_owner, _swarmleader)
         {
            Leader = _swarmleader,
            StopDistanceToPlayer = _distanceToLeader,
            MaxVelocity = _maxVelocity,
            MaxSeparation = _maxSeparation,
            SeparationDistance = _separationDistance
         };

         swarm.SetAllNeighborsWithTag(_neighborTag);

         _navMeshAgent.SetDestination(swarm.GetPositionInSwarm());

      }
      
      public override void OnUpdateSettings()
      {
         _navMeshAgent.speed = AIConifg.speed;
         _navMeshAgent.angularSpeed = AIConifg.angularSpeed;
         _navMeshAgent.acceleration = AIConifg.acceleration;
         _navMeshAgent.stoppingDistance = AIConifg.stoppingDistance;
         _navMeshAgent.autoBraking = AIConifg.autoBraking;

      }
   } 
}

