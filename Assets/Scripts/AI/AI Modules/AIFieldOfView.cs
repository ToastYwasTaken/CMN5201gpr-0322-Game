using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace AISystem
{
    public class AIFieldOfView
    {
        public Collider[] GetCollidersAround(Vector3 origin, float radius, LayerMask targetLayer, QueryTriggerInteraction queryTrigger = QueryTriggerInteraction.Ignore)
        {
            return Physics.OverlapSphere(origin, radius, targetLayer, queryTrigger);
        }

        public GameObject GetGameobjectFromColliders(Collider[] colliders, string objectTag)
        {
            return (from collider in colliders where collider.CompareTag(objectTag) select collider.gameObject).FirstOrDefault();
        }

        public GameObject GetTarget(Vector3 ownerPosition, float viewRadius, string objectTag, LayerMask targetLayer, QueryTriggerInteraction query = QueryTriggerInteraction.Ignore)
        {
            Collider[] colliders = Physics.OverlapSphere(ownerPosition, viewRadius, targetLayer, query);

            return (from collider in colliders where collider.CompareTag(objectTag) select collider.gameObject).FirstOrDefault();
        } 


        public bool InFieldOfView(Transform ownerTransform, Transform targetTransform,
            string tag, float viewDistance, float viewAngle, LayerMask targetLayer,
            QueryTriggerInteraction queryTrigger = QueryTriggerInteraction.Ignore,
            float auraDistance = 5f, bool useAura = false)
        {
            Vector3 dir = targetTransform.position - ownerTransform.position;
            var ray = new Ray(ownerTransform.position, dir.normalized);

            if (!Physics.Raycast(ray, out RaycastHit hit, viewDistance,
                targetLayer, queryTrigger))
            {
                return false;
            }

            if (!hit.collider.CompareTag(tag)) return false;

            Vector3 rayDirection = hit.transform.position - ownerTransform.position;
            float angle = Vector3.Angle(rayDirection, ownerTransform.forward);
            Debug.DrawRay(ownerTransform.position, dir, Color.blue);
            //Debug.LogWarning($"Vector: {rayDirection} | Angle: {angle}");
            return angle < viewAngle * 0.5f || ((hit.distance <= auraDistance) && useAura);
        }

    }
}