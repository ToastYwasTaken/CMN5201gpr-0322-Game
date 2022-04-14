using System.Linq;
using UnityEngine;

namespace AISystem
{
    public class AIFieldOfView 
    {
        private Ray _ray;

        public Collider[] LookAroundForColliders(Vector3 origin, float radius, LayerMask ignoreLayer, QueryTriggerInteraction queryTrigger = QueryTriggerInteraction.Ignore)
        {
            return Physics.OverlapSphere(origin, radius, ~ignoreLayer, queryTrigger);
        }

        public GameObject LookForGameObject(Collider[] colliders, string objectTag)
        {
            return (from collider in colliders where collider.CompareTag(objectTag) select collider.gameObject).FirstOrDefault();
        }

        public bool InFieldOfView(Transform origin, Transform destination,
            string tag, float viewDistance, float viewAngle, LayerMask ignoreLayer,
            QueryTriggerInteraction queryTrigger = QueryTriggerInteraction.Ignore,
            float auraDistance = 5f, bool useAura = false)
        {
            Vector3 dir = destination.position - origin.position;
            _ray = new Ray(origin.position, dir.normalized);
            bool isVisible = false;

            if (!Physics.Raycast(_ray, out RaycastHit hit, viewDistance, ~ignoreLayer, queryTrigger)) return isVisible;
            if (!hit.collider.CompareTag(tag)) return isVisible;
            Vector3 rayDirection = hit.transform.position - origin.position;
            float angle = Vector3.Angle(rayDirection, origin.forward);

            if (angle < viewAngle * 0.5f)
            {
                isVisible = true;
                Debug.DrawRay(origin.position, dir, Color.green);
            }
            else
            {
                isVisible = false;
            }

            if (!(hit.distance <= auraDistance) || !useAura) return isVisible;
            isVisible = true;
            
            Debug.DrawRay(origin.position, dir, Color.red);
            return isVisible;
        }
    }
}