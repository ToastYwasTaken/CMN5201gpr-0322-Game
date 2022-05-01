using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace AISystem
{
    public class AILookToEnemy : MonoBehaviour
    {
        private Vector3 OwnerPosition => gameObject.transform.position;
        private Vector3 TargetPosition => Target.transform.position;

        [Header("Lool At Settings")]
        [SerializeField] private GameObject _rotatingObject;
        [SerializeField] private float _lerpSpeed = 0.5f;
        [SerializeField, Range(-180f, 180f)] private float _offset = 0f;
        public GameObject Target;


        [Header("Scan Settings")]
        [SerializeField] private float _lookRadius = 5f;
        [SerializeField] private LayerMask _ignoreLayerForScan = 0;
        [SerializeField] private QueryTriggerInteraction _queryTriggerForScan = QueryTriggerInteraction.Ignore;

        [Header("FieldOfView Settings")]
        [SerializeField] private float _viewDistance = 10f;
        [SerializeField] private float _viewAngle = 120f;
        [SerializeField] private LayerMask _ignoreLayerForView = 0;
        [SerializeField] private QueryTriggerInteraction _queryTriggerForView = QueryTriggerInteraction.Ignore;

        [Header("Aura Settings")]
        [SerializeField] private bool _useAura = true;
        [SerializeField] private float _auraRadius = 5f;

        


        private bool _targetIsVisible = false;
        private Collider[] _colliders;
        private GameObject _gameObject;
        private Vector3 _refPosition = Vector3.zero;
        private bool _setDefault = false;
        private bool _lookToTarget = false;
        private float _lerpTime = 0.0f;
        private Quaternion _originQuaternion;

        public void FindTargetWithTag(string targetTag) => Target = GameObject.FindWithTag(targetTag);

        public void ResetLookAt()
        {
            _setDefault = true;
            _lookToTarget = false;
            //_rotatingObject.transform.rotation = CalculateRotationToTarget(_refPosition);
            _rotatingObject.transform.rotation = LerpAngleToTarget(_refPosition);
        }

        private IEnumerator ResetLook()
        {   
            _lerpTime = 0.0f;
            _originQuaternion = _rotatingObject.transform.rotation;

            while (true)
            {
                _rotatingObject.transform.rotation = LerpAngleToTarget(_refPosition);
                yield return new WaitForEndOfFrame();
            }
        }

        public void LookAt()
        {
            _setDefault = false;
            _lookToTarget = true;
            _lerpTime = 0.0f;
            _targetIsVisible = TargetIsVisible();
            Debug.LogWarning($"Target is Visible: {_targetIsVisible}");
            // Quaternion rotation = CalculateRotationToTarget(TargetPosition);
            
            // _rotatingObject.transform.rotation = rotation;
            // _originQuaternion = _rotatingObject.transform.rotation;
        }

        private bool TargetIsVisible()
        {
            var fov = new AIFieldOfView();

            _colliders = fov.LookAroundForColliders(_rotatingObject.transform.position, _lookRadius, _ignoreLayerForScan, _queryTriggerForScan);

            _gameObject = fov.LookForGameObject(_colliders, Target.tag);

            return _gameObject != null && fov.InFieldOfView(gameObject.transform,
               _gameObject.transform, Target.tag,
               _viewDistance, _viewAngle,
               _ignoreLayerForView, _queryTriggerForView,
               _auraRadius, _useAura);
        }

        private Quaternion LerpAngleToTarget(Vector3 targetPosition)
        {
            _lerpTime += Time.deltaTime;
            return Quaternion.Lerp(_originQuaternion, Quaternion.Euler(targetPosition), _lerpTime * _lerpSpeed);
        }

        private Quaternion CalculateRotationToTarget(Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - OwnerPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle + _offset, Vector3.forward * Time.deltaTime);
        }
    }
}


