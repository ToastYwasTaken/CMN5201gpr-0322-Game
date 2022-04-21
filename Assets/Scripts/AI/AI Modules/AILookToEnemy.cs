using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AISystem
{
    public class AILookToEnemy : MonoBehaviour
    {
        private Vector3 OwnerPosition => gameObject.transform.position;
        private Vector3 TargetPosition => Target.transform.position;

        [SerializeField] private Vector3 _defaultRotation = Vector3.zero;
        [SerializeField, Range(-180f, 180f)] private float _offset = 0f;

        public bool LookToTarget = false;

        public GameObject Target = default;

        public void FindTargetWithTag(string targetTag) => Target = GameObject.FindWithTag(targetTag);

        public void SetDefaultRotation()
        {
            gameObject.transform.rotation = Quaternion.Euler(_defaultRotation);
           
        }

        private void Update()
        {
            if (!Target)
                Debug.LogError($"{this.name}: No Target found!");
            
            if (!LookToTarget) return;
            
            gameObject.transform.rotation = CalculateRotationToTarget();
           
        }


        private Quaternion CalculateRotationToTarget()
        {
            Vector3 direction = TargetPosition - OwnerPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle + _offset, Vector3.forward);
        }
    }

}


