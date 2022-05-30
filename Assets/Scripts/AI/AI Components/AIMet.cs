using UnityEngine;

namespace AISystem
{
    public class AIMet : MonoBehaviour
    {
        public LayerMask BulletMask = 0;
        public float DetectionRadius = 1f;

        public bool AmMet()
        {
            Vector3 position = transform.position;
            var center = new Vector2(position.x, position.y);
            Collider2D colliders = Physics2D.OverlapCircle(center, DetectionRadius, BulletMask);
            return colliders != null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, DetectionRadius);
        }
    }
}