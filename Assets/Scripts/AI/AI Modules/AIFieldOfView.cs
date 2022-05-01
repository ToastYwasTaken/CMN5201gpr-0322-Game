using System.Linq;
using UnityEngine;

namespace AISystem
{
    public class AIFieldOfView
    {
        public Collider[] LookAroundForColliders(Vector3 origin, float radius, LayerMask ignoreLayer, QueryTriggerInteraction queryTrigger = QueryTriggerInteraction.Ignore)
        {
            Debug.DrawLine(origin, new Vector3(origin.x + radius, origin.y, origin.z), Color.cyan);
            Debug.DrawLine(origin, new Vector3(origin.x, origin.y + radius, origin.z), Color.cyan);
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
            var ray = new Ray(origin.position, dir.normalized);

            if (!Physics.Raycast(ray, out RaycastHit hit, viewDistance,
                ~ignoreLayer, queryTrigger))
            {
                return false;
            }

            if (!hit.collider.CompareTag(tag)) return false;

            Vector3 rayDirection = hit.transform.position - origin.position;
            float angle = Vector3.Angle(rayDirection, origin.forward);

            //Debug.LogWarning($"Angle: {angle}");
            Debug.DrawRay(origin.position, dir, Color.red);

            return angle < viewAngle * 0.5f || ((hit.distance <= auraDistance) && useAura);
        }

    }
}