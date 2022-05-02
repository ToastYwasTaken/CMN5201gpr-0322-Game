using System;
using System.Collections;
using UnityEngine;

namespace AISystem
{
    public class AILookToEnemy : MonoBehaviour
    {
        private Vector3 OwnerPosition => gameObject.transform.position;
        private Vector3 TargetPosition => Target.transform.position;

        public GameObject VisibleObject => _targetObject;

        [Header("Lool At Settings")]
        [SerializeField] private GameObject _rotatingObject;
        [SerializeField] private float _lerpSpeed = 0.5f;
        [SerializeField, Range(-180f, 180f)] private float _offset = 0f;
        public GameObject Target;


        [Header("Scan Settings")]
        [SerializeField] private LayerMask _targetLayerForScan = 0;
        [SerializeField] private QueryTriggerInteraction _queryTriggerForScan = QueryTriggerInteraction.Ignore;

        [Header("FieldOfView Settings")]
        public float ViewDistance = 10f;
        [Range(0, 360)] public float ViewAngle = 120f;
        [SerializeField] private LayerMask _targetLayerForView = 0;
        [SerializeField] private QueryTriggerInteraction _queryTriggerForView = QueryTriggerInteraction.Ignore;

        [Header("Aura Settings")]
        public bool UseAura = true;
        public float AuraRadius = 5f;

        // FoV 
        private GameObject _targetObject = null;
        private bool _targetIsVisible = false;

        // Look At
        private Quaternion LookRotate => Quaternion.LookRotation(gameObject.transform.forward);
        private Vector3 LookPosition => gameObject.transform.forward;

        private float _lerpTimeA = 0.0f;
        private float _lerpTimeB = 0.0f;
        private Quaternion _originQuaternion;

        private AIFieldOfView fov = new();

        public void FindTargetWithTag(string targetTag) => Target = GameObject.FindWithTag(targetTag);

        public void ResetLookAt()
        {
            _rotatingObject.transform.rotation = LerpRotating(LookRotate);
        }

        public void LookAt()
        {
            GameObject targetObject = fov.GetTarget(OwnerPosition, ViewDistance, Target.tag, _targetLayerForScan, _queryTriggerForScan);

            _targetIsVisible = TargetIsVisible(targetObject);

            _targetObject = _targetIsVisible ? targetObject : null;

            Debug.LogWarning($"Target is Visible: {_targetIsVisible}");
    
            //if (_targetIsVisible)
            //{
                _lerpTimeB = 0.0f;
                _lerpTimeA = 0.0f;
                // Look At
                Quaternion rotation = CalculateRotationToTarget(TargetPosition);

                _rotatingObject.transform.rotation = rotation;
                _originQuaternion = _rotatingObject.transform.rotation;
            //}
            //else
            //{
            //    Quaternion lookRotating = LerpAngleToPosition(LookPosition);
            //    _rotatingObject.transform.rotation = lookRotating; // Quaternion.Euler(lookRotating.x - 180, lookRotating.y, lookRotating.z + 180);
            //}
            
        }

        private bool TargetIsVisible(GameObject target)
        {
            return target != null && fov.InFieldOfView(gameObject.transform,
               target.transform, Target.tag,
               ViewDistance, ViewAngle,
               _targetLayerForView, _queryTriggerForView,
               AuraRadius, UseAura);
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
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle + _offset, Vector3.forward);
        }

        public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGobal)
        {
            if (!angleIsGobal)
                angleInDegrees += transform.eulerAngles.y;

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0f, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}


