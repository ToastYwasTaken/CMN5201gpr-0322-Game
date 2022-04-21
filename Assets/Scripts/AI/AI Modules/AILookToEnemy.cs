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
        private Vector3 OwnerPosition => _rotatingObject.transform.position;
        private Vector3 TargetPosition => Target.transform.position;

        [SerializeField] private GameObject _rotatingObject;
        [SerializeField] private Vector3 _defaultRotation = Vector3.zero;
        [SerializeField] private float _lerpSpeed = 0.5f;
        [SerializeField, Range(-180f, 180f)] private float _offset = 0f;

        public bool LookToTarget = false;
        public GameObject Target;

        private bool _setDefault = false;
        private float _lerpTime = 0.0f;
        private Quaternion _originQuaternion;
        
        private void Update()
        {
            if (_setDefault)
            {
                float refValue = Math.Abs(_originQuaternion.z - Quaternion.Euler(_defaultRotation).z);
                _setDefault = refValue < 1.9f;
                _rotatingObject.transform.rotation = LerpAngle();
            }

            if (!LookToTarget) return;

            if (!_rotatingObject)
            {
                Debug.LogWarning($"{this.name}: Set rotating object!");
                return;
            }

            if (!Target)
            {
                Debug.LogError($"{this.name}: No Target found!");
                return;
            }

            LookAt();
        }

        public void FindTargetWithTag(string targetTag) => Target = GameObject.FindWithTag(targetTag);

        public void SetDefaultRotation()
        {
            _lerpTime = 0.0f;
            _originQuaternion = _rotatingObject.transform.rotation;
            LookToTarget = false;
            _setDefault = true;
        }

        public void LookAt()
        {
            _setDefault = false;
            _rotatingObject.transform.rotation = CalculateRotationToTarget();
        }

        private Quaternion LerpAngle()
        {
            _lerpTime = _lerpTime + Time.deltaTime;
            return Quaternion.Lerp(_originQuaternion, Quaternion.Euler(_defaultRotation), _lerpTime * _lerpSpeed);
        }

        private Quaternion CalculateRotationToTarget()
        {
            Vector3 direction = TargetPosition - OwnerPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle + _offset, Vector3.forward);
        }
    }
}


