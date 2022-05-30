using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Actions/Evade")]
    public class EvadeAction : AIStateAction
    {
        private NavMeshAgent _navMeshAgent;
        private GameObject _owner;
        
        [SerializeField] private LayerMask _bulletMask = 0;
        [SerializeField] private float _detectionRadius = 1f;
        [SerializeField] private float _maxVelocity = 3f;
        [SerializeField] private float _seekForce = 0.005f;

        private Vector3 _velocity = Vector3.zero;
        
        public override void Initialize(AIFSMAgent stateMachine)
        {
            _navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            _owner = stateMachine.Owner;
        }

        public override void Execute(AIFSMAgent stateMachine)
        {

            if (_navMeshAgent == null || !_navMeshAgent.isOnNavMesh) return;
            OnUpdateSettings();

            Vector3 point = Evade();
            
            if (point == Vector3.zero) return;

            _navMeshAgent.SetDestination(point);
        }
 
        public Vector3 Evade()
        {
            Vector3 position = _owner.transform.position;
            var center = new Vector2(position.x, position.y);
            Collider2D collider2D = Physics2D.OverlapCircle(center, _detectionRadius, _bulletMask);
            
            return collider2D == null ? Vector3.zero : CalculateEvadeBehaviour(collider2D);
        }
        
        private Vector3 CalculateEvadeBehaviour(Collider2D col)
        {
            Vector3 desiredVelocity = (col.transform.position - _owner.transform.position).normalized * _maxVelocity;
            Vector3 steering = desiredVelocity - _velocity;
            return steering * _seekForce;

        }
        public override void OnUpdateSettings()
        {
             if (_navMeshAgent == null) return;
            _navMeshAgent.speed = AIConifg.speed;
            _navMeshAgent.angularSpeed = AIConifg.angularSpeed;
            _navMeshAgent.acceleration = AIConifg.acceleration;
            _navMeshAgent.stoppingDistance = AIConifg.stoppingDistance;
            _navMeshAgent.autoBraking = AIConifg.autoBraking;

        }
        
       

      
    }
}

