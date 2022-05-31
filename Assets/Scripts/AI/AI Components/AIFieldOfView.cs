using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace AISystem
{
    public class AIFieldOfView : MonoBehaviour
    {
        public string TargetTag = "Player";
        public LayerMask TargetMask = 0;
        public QueryTriggerInteraction Interaction = QueryTriggerInteraction.Ignore;

        [Header("Field of View settings")]
        public float ViewRadius = 10f;
        public float ViewAngle = 120f;
        public bool UseAura = false;
        public float AuraRadius = 2.5f;

        #region Propertys

        public Transform OwnerTransform => gameObject.transform;
        public GameObject Target { get; private set; }

        #endregion
        
        private GameObject GetTarget()
        {
            Collider[] colliders = Physics.OverlapSphere(OwnerTransform.position, ViewRadius, TargetMask, Interaction);

            return (from collider in colliders where collider.CompareTag(TargetTag) select collider.gameObject).FirstOrDefault();
        }

        public bool InFieldOfView()
        {
            GameObject target = GetTarget();
            if (target == null) return false;

            Target = target;

            Vector3 dir = target.transform.position - OwnerTransform.position;
            var ray = new Ray(OwnerTransform.position, dir.normalized);

            if (!Physics.Raycast(ray, out RaycastHit hit, ViewRadius,
                    TargetMask, Interaction))
            {
                return false;
            }

            if (!hit.collider.CompareTag(TargetTag)) return false;

            Vector3 rayDirection = hit.transform.position - OwnerTransform.position;
            //Debug.Log($"Aura: {hit.distance} - {AuraRadius}");
            float angle = Vector3.Angle(rayDirection, OwnerTransform.forward);
            Debug.DrawRay(OwnerTransform.position, dir, Color.blue);
            //Debug.LogWarning($"Vector: {rayDirection} | Angle: {angle}");
            return angle < ViewAngle * 0.5f || ((hit.distance <= AuraRadius) && UseAura);
        }
        
        public Vector3 DirectionFromAngle(float angleInDegress, bool angleIsGobal)
        {
            if (!angleIsGobal)
                angleInDegress += transform.eulerAngles.z;

            return new Vector3(Mathf.Sin(angleInDegress * Mathf.Deg2Rad), Mathf.Cos(angleInDegress * Mathf.Deg2Rad), 0f);
        }

    }
}