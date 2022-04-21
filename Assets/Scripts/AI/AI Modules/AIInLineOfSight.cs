using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    public class AIInLineOfSight 
    {
        public Transform Target { get; private set; }
        public Transform Owner { get; private set; }

        public LayerMask IgnoreLayer {  get; private set; }

        public AIInLineOfSight(GameObject owner, string targetTag, LayerMask ignoreLayer)
        {
            Owner = owner.transform;

            IgnoreLayer = ignoreLayer;

            Target = GameObject.FindWithTag(targetTag).transform;
        }
        
        public AIInLineOfSight(GameObject owner, GameObject target, LayerMask ignoreLayer)
        {
            Owner = owner.transform;

            IgnoreLayer = ignoreLayer;

            Target = target.transform;
        }
        
        
        public bool IsInLine(string targetTag)
        {
            if (Target == null || Owner == null) return false;

            var transform = Owner.transform;
            var position = transform.position;
            Ray _ray = new Ray(position, Target.position - position);

            var dir = new Vector3(_ray.direction.x, 0, _ray.direction.z);
            var angle = Vector3.Angle(dir, transform.forward);

            if (angle > 60) return false;

            return Physics.Raycast(_ray, out var hit, 100, ~IgnoreLayer) && hit.collider.CompareTag(targetTag);
        }

       
    }
}

