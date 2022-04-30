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

        [SerializeField] private GameObject _rotatingObject;
        [SerializeField] private float _lerpSpeed = 0.5f;
        [SerializeField, Range(-180f, 180f)] private float _offset = 0f;

        public GameObject Target;

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
            _rotatingObject.transform.rotation = CalculateRotationToTarget(TargetPosition);
            _originQuaternion = _rotatingObject.transform.rotation;
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
            return Quaternion.AngleAxis(angle + _offset, Vector3.forward);
        }
    }
}


