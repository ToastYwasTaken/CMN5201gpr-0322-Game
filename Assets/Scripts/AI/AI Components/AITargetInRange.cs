using UnityEngine;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AITargetInRange.cs
* Date : 29.05.2022
* Author : René Kraus (RK)
*
* These coded instructions, statements, and computer programs contain
* proprietary information of the author and are protected by Federal
* copyright law. They may not be disclosed to third parties or copied
* or duplicated in any form, in whole or in part, without the prior
* written consent of the author.
******************************************************************************/
namespace AISystem
{
    /// <summary>
    /// aAllows checking if the target is in range
    /// </summary>
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

        /// <summary>
        /// Search searches for a gameobject with the given tag
        /// </summary>
        /// <param name="targetTag"></param>
        public void FindGameObject(string targetTag = "Player")
        {
            Target = GameObject.FindGameObjectWithTag(targetTag);
            TargetTag = targetTag;
        }

        /// <summary>
        /// Checking distance with vector data
        /// </summary>
        /// <param name="range"></param>
        /// <returns>bool</returns>
        public bool InRangeByDistance(float range = -1f)
        {
            if (range <= -1f) range = Range;
            if (Target == null || Owner == null) return false; 
            float distance = Vector3.Distance(Target.transform.position, Owner.transform.position);
            // Debug.Log($"Distance: {distance}");
            return distance <= range;
        }
        
        /// <summary>
        /// Checking distance with Physics Raycast
        /// </summary>
        /// <returns>bool</returns>
        public bool InRangeByRayCast()
        {
            if (Target == null) return false;

            Vector3 dir = Target.transform.position - Owner.transform.position;
            var ray = new Ray(Owner.transform.position, dir.normalized);
        
            if (!Physics.Raycast(ray, out RaycastHit hit, 
                    float.PositiveInfinity, TargetMask, Interaction)) 
                    return false;
        
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
