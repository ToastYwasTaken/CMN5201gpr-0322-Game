using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    public class AITargetInRange
    {
        public Transform Target { get; private set; }
        public Transform Owner { get; private set; }

        public AITargetInRange(GameObject owner, string targetTag)
        {
            Owner = owner.transform;
            Target = GameObject.FindWithTag(targetTag).transform;
        }

        public AITargetInRange(GameObject owner, GameObject target)
        {
            Owner = owner.transform;
            Target = target.transform;
        }

        public bool TargetInRange(float range)
        {
            if (Target == null || Owner == null) return false; 
            float distance = Vector3.Distance(Target.position, Owner.position);
            // Debug.Log($"Distance: {distance}");
            return distance <= range;
        }

    }
}
