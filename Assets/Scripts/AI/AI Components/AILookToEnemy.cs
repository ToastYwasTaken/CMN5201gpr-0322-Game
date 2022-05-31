using UnityEngine;
using UnityEngine.PlayerLoop;

namespace AISystem
{
    public class AILookToEnemy : MonoBehaviour
    {
        public GameObject Target;

        [Header("Look At Settings")]
        [SerializeField]
        private GameObject _rotatingObject;

        [SerializeField] private float _lerpSpeed = 0.5f;

        // Look At

        private GameObject _targetObject;
        private float _lerpTimeA = 0.0f;

        #region Propertys

        private Vector3 OwnerPosition => gameObject.transform.position;
        private Vector3 TargetPosition => Target.transform.position;

        #endregion

        public void FindTargetWithTag(string targetTag)
        {
            _targetObject = GameObject.FindWithTag(targetTag);
            Target = _targetObject;
        }

        public void LookAtTarget()
        {
            if (Target == null) return;
            Quaternion rotation = CalculateRotationToTarget(TargetPosition);
            _rotatingObject.transform.rotation = LerpRotating(rotation);
        }

        private Quaternion LerpRotating(Quaternion targetRotating)
        {
            _lerpTimeA += Time.deltaTime;
            if (_lerpTimeA >= 1f) _lerpTimeA = 0.0f;
            return Quaternion.Lerp(_rotatingObject.transform.rotation, targetRotating, _lerpTimeA * _lerpSpeed);
        }

        private Quaternion CalculateRotationToTarget(Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - OwnerPosition;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(-angle, Vector3.forward);
        }
    }
}