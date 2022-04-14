using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    public class AIInLineOfSight 
    {
        public Transform Target { get; set; }
        public Transform Owner { get; set; }

        public LayerMask IgnoreLayer { get; set; }

        private Ray _ray;

        public bool Ping(string targetTag)
        {
            if (Target == null || Owner == null) return false;

            var transform = Owner.transform;
            var position = transform.position;
            _ray = new Ray(position, Target.position - position);

            var dir = new Vector3(_ray.direction.x, 0, _ray.direction.z);
            var angle = Vector3.Angle(dir, transform.forward);

            if (angle > 60) return false;

            return Physics.Raycast(_ray, out var hit, 100, ~IgnoreLayer) && hit.collider.CompareTag(targetTag);
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawLine(_ray.origin, _ray.origin + _ray.direction * 100);
        //     Gizmos.color = Color.blue;
        //     Gizmos.DrawLine(transform.position, transform.position + transform.forward * 100 );
        // }
    }
}

