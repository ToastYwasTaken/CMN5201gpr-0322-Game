using UnityEngine;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AILookToEnemy.cs
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
    /// Aligns the GameOject to the target
    /// </summary>
    public class AILookToEnemy : MonoBehaviour
    {

        [Header("Look At Settings")]
        [SerializeField] private GameObject _rotatingObject;
        [SerializeField] private float _lerpSpeed = 0.5f;

        private GameObject _targetObject;
        private float _lerpTimeA = 0.0f;

        public GameObject Target;

        #region Propertys

        private Vector3 OwnerPosition => gameObject.transform.position;
        private Vector3 TargetPosition => Target.transform.position;

        #endregion

        /// <summary>
        /// Search searches for a gameobject with the given tag
        /// </summary>
        /// <param name="targetTag"></param>
        public void FindTargetWithTag(string targetTag)
        {
            _targetObject = GameObject.FindWithTag(targetTag);
            Target = _targetObject;
        }

        /// <summary>
        /// Rotates the object to the target
        /// </summary>
        public void LookAtTarget()
        {
            if (Target == null) return;
            Quaternion rotation = CalculateRotationToTarget(TargetPosition);
            _rotatingObject.transform.rotation = LerpRotating(rotation);
        }

        /// <summary>
        /// Lerp the rotating
        /// </summary>
        /// <param name="targetRotating"></param>
        /// <returns>Quaternion</returns>
        private Quaternion LerpRotating(Quaternion targetRotating)
        {
            _lerpTimeA += Time.deltaTime;
            if (_lerpTimeA >= 1f) _lerpTimeA = 0.0f;
            return Quaternion.Lerp(_rotatingObject.transform.rotation, targetRotating, _lerpTimeA * _lerpSpeed);
        }

        /// <summary>
        /// Calculates the quaternion to the target
        /// </summary>
        /// <param name="targetPosition"></param>
        /// <returns>Quaternion</returns>
        private Quaternion CalculateRotationToTarget(Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - OwnerPosition;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(-angle, Vector3.forward);
        }
    }
}