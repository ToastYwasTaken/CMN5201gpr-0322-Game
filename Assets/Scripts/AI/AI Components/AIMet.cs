using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    public class AIMet : MonoBehaviour
    {

        public LayerMask BulletMask = 0;
        public QueryTriggerInteraction Interaction = QueryTriggerInteraction.Ignore;
        public float DetectionRadius = 1f;
        
        public bool AmMet()
        {
            Vector3 position = transform.position;
            var center = new Vector2(position.x, position.y);
            Collider2D colliders = Physics2D.OverlapCircle(center, DetectionRadius, BulletMask);
            Debug.LogWarning($"Bullets hit me: {colliders}");
            return false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            throw new NotImplementedException();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, DetectionRadius);
        }
    }
}