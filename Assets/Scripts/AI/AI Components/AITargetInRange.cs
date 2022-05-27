using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    public class AITargetInRange : MonoBehaviour
    {
        public string TargetTag = "Player";
        public LayerMask TargetMask = 0;
        public QueryTriggerInteraction Interaction = QueryTriggerInteraction.Ignore;

        [Header("In Range settings")] 
        public float Range = 10f;

        #region Propertys

        public GameObject Owner => gameObject;
        public GameObject Target { get; private set; }

        #endregion
        
        private void Start()
        {
            if (string.IsNullOrEmpty(TargetTag)) return;
            FindGameObject(TargetTag);
        }

        public void FindGameObject(string targetTag = "Player")
        {
            Target = GameObject.FindGameObjectWithTag(targetTag);
            TargetTag = targetTag;
        }

        public bool InRangeByDistance(float range = -1f)
        {
            if (range <= -1f) range = Range;
            if (Target == null || Owner == null) return false; 
            float distance = Vector3.Distance(Target.transform.position, Owner.transform.position);
            // Debug.Log($"Distance: {distance}");
            return distance <= range;
        }
        
        public bool InRangeByRayCast()
        {
            if (Target == null) return false;

            Vector3 dir = Target.transform.position - Owner.transform.position;
            var ray = new Ray(Owner.transform.position, dir.normalized);
        
            if (!Physics.Raycast(ray, out RaycastHit hit, 
                    float.PositiveInfinity, TargetMask, Interaction)) return false;
        
            if (hit.distance <= Range)
            {
                Debug.DrawRay(Owner.transform.position, dir, Color.green);
                return true;
            }
            else
            {
                Debug.DrawRay(Owner.transform.position, dir, Color.red);
                return false;
            }
        }

    }
}
