using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/Actions/Chase")]
    public class ChaseAction : AIStateAction
    {
        [Header("Settings")]
        [SerializeField] private float _velocityOffset = 0f;

        [Header("AI Events")]
        [SerializeField] private AIEvent OnStateEntered;
        [SerializeField] private AIEvent OnHasReachedWaypoint;
        [SerializeField] private AIEvent OnAgentMoveForward;
        [SerializeField] private AIEvent OnAgentMoveBack;
        [SerializeField] private AIEvent OnAgentTurnLeft;
        [SerializeField] private AIEvent OnAgentTurnRight;
        [SerializeField] private AIEvent OnAgentStopped;

        private NavMeshAgent navMeshAgent;

        public override void Initialize(AIFSMAgent stateMachine)
        {
            if (OnStateEntered != null) OnStateEntered.Raise();
            navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        }

        public override void Execute(AIFSMAgent stateMachine)
        {
            OnUpdateSettings();

            if (navMeshAgent.velocity.sqrMagnitude >= _velocityOffset)
            {
                OnAgentMoveForward.Raise();
            }
            else
            {
                OnAgentStopped.Raise();
            }


            var enemySightSensor = stateMachine.GetComponent<AIInLineOfSight>();

            navMeshAgent.SetDestination(enemySightSensor.Player.position);
        }

        public override void OnUpdateSettings()
        {
            navMeshAgent.speed = AIConifg.speed;
            navMeshAgent.angularSpeed = AIConifg.angularSpeed;
            navMeshAgent.acceleration = AIConifg.acceleration;
            navMeshAgent.stoppingDistance = AIConifg.stoppingDistance;
            navMeshAgent.autoBraking = AIConifg.autoBraking;

        }
    }
}

