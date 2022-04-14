using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    public class AISwarm
    {
        public GameObject Target { get; set; }
        public float StopDistanceToPlayer { get; set; } = 2f;
        public Vector3 Velocity { get; set; } = Vector3.zero;
        public float MaxVelocity { get; set; } = 3f;
        public float MaxSeparation { get; set; } = 2f;
        public float SeparationDistance { get; set; } = 2f;
  
        private GameObject _owner;
        private readonly List<GameObject> _neighbors = new();
        private Vector3 _targetVelocity = Vector3.zero;
        private Vector3 _targetPrevPos;

        private Vector3 OwnerPosition
        {
            get  => _owner.transform.position;
            set => _owner.transform.position = value;
        }
      
        private Vector3 TargetPosition => Target.transform.position;
        public Vector3 TargetVelocity
        {
            get
            {
                _targetVelocity = (TargetPosition - _targetPrevPos) / Time.deltaTime;
                _targetPrevPos = TargetPosition;
                return _targetVelocity;
            }
        }

        public AISwarm(GameObject owner) => _owner = owner;
        
        public void SetNeighbors(GameObject neighbor)
        {
            if (neighbor == _owner) return;

            if (!_neighbors.Contains(neighbor))
                _neighbors.Add(neighbor);
        }

        public void ResetNeighbors()
        {
            _neighbors.Clear();
        }

        public Vector3 GetPosition()
        {
            return Vector3.zero;

        }

        public void InitSwarmBehaviour(GameObject owner, GameObject target)
        {
            _owner = owner;
            Target = target;
        }

        public Vector3 GetSwarmPosition()
        {
            if (_owner == null || Target == null) return Vector3.zero;

            Velocity += CalculateFollowTargetBehaviour();
            return OwnerPosition += Velocity * Time.deltaTime;
        }



        private Vector3 CalculateFollowTargetBehaviour()
        {
            Vector3 steering = Vector3.zero;

            Vector3 targetPosition = (TargetVelocity.normalized * -1f * StopDistanceToPlayer) + TargetPosition;

            if (Vector3.Distance(TargetPosition, OwnerPosition) <= StopDistanceToPlayer)
            {
                steering += CalculateEvadePosition();
                steering *= 0.5f;
            }
            else
            {
                steering += CalculateArrivalBehaviour(targetPosition, StopDistanceToPlayer);
            }

            steering += CaluclateSeparationBehaviour();
            return steering;
        }


        private Vector3 CaluclateSeparationBehaviour()
        {
            Vector3 steering = Vector3.zero;
            int neighbors = 0;

            for (int i = 0; i < _neighbors.Count; i++)
            {
                Vector3 neighborPosition = _neighbors[i].transform.position;
                if (Vector3.Distance(neighborPosition, OwnerPosition) <= SeparationDistance)
                {
                    steering += neighborPosition - OwnerPosition;
                    neighbors++;
                }
            }
            if (neighbors > 0)
            {
                steering /= neighbors;
                steering *= -1f;
            }

            steering = steering.normalized * MaxSeparation;
            return steering;

        }

        private Vector3 CalculateArrivalBehaviour(Vector3 target, float slowingRadius = 3.5f)
        {
            Vector3 desiredVelocity = (target - OwnerPosition).normalized * MaxVelocity;

            float distance = Vector3.Distance(target, OwnerPosition);
            if (distance <= slowingRadius)
            {
                desiredVelocity *= (distance / slowingRadius);
            }

            Vector3 steering = desiredVelocity - Velocity;
            return steering;
        }

        private Vector3 CalculateFleeBehaviour(Vector3 target)
        {
            Vector3 desiredVelocity = (OwnerPosition - target).normalized * MaxVelocity;
            Vector3 steering = desiredVelocity - Velocity;
            return steering;
        }

        private Vector3 CalculateEvadePosition()
        {
            float distance = Vector3.Distance(OwnerPosition, TargetPosition) / MaxVelocity;
            Vector3 targetFuturePosition = TargetPosition + (TargetVelocity * distance);
            return CalculateFleeBehaviour(targetFuturePosition);
        }

    }
}


