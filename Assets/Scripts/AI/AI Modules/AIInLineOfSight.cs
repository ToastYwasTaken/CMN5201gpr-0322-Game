using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    public class AIInLineOfSight : MonoBehaviour
    {
        public Transform Player { get; private set; }

        [SerializeField] private LayerMask _ignoreLayer;

        private Ray _ray;

        private void Awake()
        {
            Player = GameObject.Find("Player").transform;
        }

        public bool Ping()
        {
            if (Player == null) return false;

            var position = this.transform.position;
            _ray = new Ray(transform.position, Player.position - transform.position);

            var dir = new Vector3(_ray.direction.x, 0, _ray.direction.z);
            var angle = Vector3.Angle(dir, transform.forward);

            if (angle > 60) return false;

            if (!Physics.Raycast(_ray, out var hit, 100, ~_ignoreLayer))
            {
                return false;
            }

            if (hit.collider.tag == "Player")
            {
                return true;
            }

            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_ray.origin, _ray.origin + _ray.direction * 100);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 100 );
        }
    }
}

