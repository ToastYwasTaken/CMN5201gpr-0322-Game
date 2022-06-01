using UnityEngine;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AIMet.cs
* Date : 31.05.2022
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
    /// Determines if the agent is under fire
    /// </summary>
    public class AIMet : MonoBehaviour
    {
        public LayerMask BulletMask = 0;
        public float DetectionRadius = 1f;

        /// <summary>
        /// Returns whether a hit was detected
        /// </summary>
        /// <returns>bool</returns>
        public bool HitDetected()
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