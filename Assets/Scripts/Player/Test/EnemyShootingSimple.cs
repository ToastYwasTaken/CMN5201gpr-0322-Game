using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AngleExtension;
namespace Assets.Scripts.Player 
{
    public class EnemyShootingSimple : MonoBehaviour
    {
        [SerializeField] Transform _parent;
        private float _ownAngle;
        [SerializeField] float _range, _aimRange;
        Transform _target;

        [HideInInspector]IWeapon _weapon;

        private void Awake()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            _weapon = GetComponent<IWeapon>();
        }

        //[SerializeField] bool isRange, isNotObst, IsAimAt;
        private void FixedUpdate()
        {
            //isRange = IsInRange(_target);
            //isNotObst = IsNotObstructed(_target);
            //IsAimAt = IsAimingAt(_target);
            if ( IsInRange(_target) && IsNotObstructed(_target))
            {
                _weapon.Fire();
            }
        }
        bool IsInRange(Transform target)
        {
            float sqrMag = (_parent.position.ToVector2() - target.position.ToVector2()).sqrMagnitude;
            return sqrMag < _range;
        }
        bool IsNotObstructed(Transform target)
        {
            RaycastHit hit;
            Physics.Raycast(_parent.position, target.position -_parent.position, out hit);
            if (hit.collider != null)
            {
                IEntity iEtt = hit.transform.gameObject.GetComponent<IEntity>();
                if (iEtt != null)
                {
                    if (iEtt.EType() == eEntityType.PLAYER)
                        return true;
                }
            }
            return false;
        }
        bool IsAimingAt(Transform target)
        {
            if (AngleDifferenceToTarget(target, true) < _aimRange)
                return true;
            return false;
        }
        private void UpdateOwnAngle()
        {
            _ownAngle = _parent.localEulerAngles.z;
        }

        public float AngleDifferenceToTarget(Transform target, bool isAbsolut)
        {
            float ownAngle = _parent.up.ToVector2().GetAngle();
            float angleToTarget = (target.ToVector2() - _parent.position.ToVector2()).GetAngle();
            float angleDiff = ownAngle - angleToTarget;
            if (angleDiff > 180) angleDiff = -180 + angleDiff % 180;

            return isAbsolut ? Mathf.Abs(angleDiff) : angleDiff;
        }

        //private void OnDrawGizmosSelected()
        //{
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawLine(_parent.position, _target.position);
        //}
    }
}