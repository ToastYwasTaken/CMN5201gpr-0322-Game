using UnityEngine;

namespace AISystem
{
    public class AILookToEnemy : MonoBehaviour
    {
        public GameObject Target;

        [Header("Look At Settings")] [SerializeField]
        private GameObject _rotatingObject;

        [SerializeField] private float _lerpSpeed = 0.5f;

        // Look At
        private Quaternion LookRotate => Quaternion.LookRotation(gameObject.transform.up);
        private Vector3 LookPosition => gameObject.transform.forward;
        private GameObject _targetObject;
        private float _lerpTimeA = 0.0f;
        private float _lerpTimeB = 0.0f;
        private Quaternion _originQuaternion;

        private AIFieldOfView _fieldOfView;

        #region Propertys

        private Vector3 OwnerPosition => gameObject.transform.position;
        private Vector3 TargetPosition => Target.transform.position;
        public GameObject TargetObject { get; private set; }

        #endregion

        public void FindTargetWithTag(string targetTag)
        {
            _targetObject = GameObject.FindWithTag(targetTag);
            Target = _targetObject;
        }

        public void ResetLookAt()
        {
            _rotatingObject.transform.rotation = LerpRotating(LookRotate);
        }

        public void LookAt(GameObject target)
        {
            _lerpTimeB = 0.0f;
            _lerpTimeA = 0.0f;
            // Look At
            Quaternion rotation = CalculateRotationToTarget(target.transform.position);

            _rotatingObject.transform.rotation = rotation;
            _originQuaternion = _rotatingObject.transform.rotation;
        }

        public void LookAtInstance()
        {
            Quaternion rotation = CalculateRotationToTarget(TargetPosition);

            _rotatingObject.transform.rotation = rotation;
            _originQuaternion = _rotatingObject.transform.rotation;
        }

        private Quaternion LerpRotating(Quaternion targetRotating)
        {
            _lerpTimeA += Time.deltaTime;
            return Quaternion.Lerp(_rotatingObject.transform.rotation, targetRotating, _lerpTimeA * _lerpSpeed);
        }

        private Quaternion LerpAngleToPosition(Vector3 targetPosition)
        {
            _lerpTimeB += Time.deltaTime;
            return Quaternion.Lerp(_originQuaternion, Quaternion.Euler(targetPosition), _lerpTimeB * _lerpSpeed);
        }

        private Quaternion CalculateRotationToTarget(Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - OwnerPosition;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(-angle, Vector3.forward);
        }
    }
}