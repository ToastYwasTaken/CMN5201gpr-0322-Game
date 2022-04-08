using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    public class AIFieldOfView : MonoBehaviour
    {
        private Ray _ray;

        public Collider[] LookAroundForColliders(Vector3 origin, float radius, LayerMask ignoreLayer, QueryTriggerInteraction queryTrigger = QueryTriggerInteraction.Ignore)
        {
            return Physics.OverlapSphere(origin, radius, ~ignoreLayer, queryTrigger);
        }

        public GameObject LookForGameObject(Collider[] colliders, string objectTag)
        {
            foreach (var collider in colliders)
            {
                if (collider.CompareTag(objectTag))
                    return collider.gameObject;
            }
            return null;
        }

        public bool InFieldOfView(Transform origin, Transform destination, 
            string tag, float viewDistance, float viewAngle, LayerMask ignoreLayer,
            QueryTriggerInteraction queryTrigger = QueryTriggerInteraction.Ignore, 
            float auraDistance = 5f, bool useAura = false)
        {
            Vector3 dir = destination.position - origin.position;
            _ray = new Ray(origin.position, dir.normalized);
            bool isVisible = false;

            if (Physics.Raycast(_ray, out var hit, viewDistance, ~ignoreLayer, queryTrigger))
            {
                if (hit.collider.CompareTag(tag))
                {
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

                    if (hit.distance <= auraDistance && useAura)
                    {
                        isVisible = true;
                        Debug.DrawRay(origin.position, dir, Color.red);
                    }
                }
            }
            return isVisible;
        }

        private void OnDrawGizmos()
        {
            //Gizmos.color = Color.red;
            //Gizmos.DrawSphere(_ray.origin, 5f);

            //Gizmos.color = Color.blue;
            //Gizmos.DrawLine(transform.position, transform.position + transform.forward * 100);
        }

    }
}