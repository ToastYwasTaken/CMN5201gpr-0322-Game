using AngleExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    class Rotateable : MonoBehaviour
    {
        [SerializeField] Transform _parent;
        [SerializeField] float _turnSpeed;
        float _constrainL 
        { 
            get { return _constrainL; } 
            set { _constrainL = value; _isWideConstrain = IsWideConstrain(); } 
        }
        float _constrainR
        { 
            get { return _constrainR; } 
            set { _constrainR = value; _isWideConstrain = IsWideConstrain(); } 
        }
        [SerializeField] AnimationCurve mRotationCurve;

        Rigidbody2D _rBody;

        float lastDir, constrL, constrR, ownAngle, parentAngle, targetLerp;
        [SerializeField] bool isConstrain; 
        bool _isWideConstrain;

        private void Awake()
        {
            _rBody = GetComponent<Rigidbody2D>();
        }

        public void RotateToTarget2(Transform target)
        {
            Vector2 targetV2 = target.ToVector2() - _rBody.position;
            float targetAngle = Mathf.Atan2(targetV2.y, targetV2.x) * Mathf.Rad2Deg - 90f;
            if(targetAngle != targetLerp)
                targetLerp = LerpAngle(targetLerp, targetAngle);

            float angleDiff = Mathf.DeltaAngle(targetLerp, lastDir);
            if (angleDiff == 0) return;

            //float currAngle = AngleWrap(Mathf.LerpAngle(lastDir, targetLerp, LerpDist(angleDiff)));

            //_rBody.rotation = currAngle;
            //lastDir = currAngle;
        }

        public void RotateToTarget()
        {
            Transform target = null;
            Vector2 targetV2 = target.ToVector2() - _rBody.position;

            float targetAngle = Mathf.Atan2(targetV2.y, targetV2.x) * Mathf.Rad2Deg - 90f;


        }

        // wideconstrain? -> -+ Angle -> lerp ->AngleWrap

        //constrain >180 / lerp to <180 ? / ..


        void UpdateOwnAngle()
        {
            ownAngle = transform.rotation.z;
            if (ownAngle < 0) ownAngle = ownAngle + 360f;
        }
        void UpdateParentAngle()
        {
            parentAngle = _parent.ToVector2().GetAngle();
        }

        void UpdateConstrains(float forwardAngle)
        {
            constrL = _constrainL + forwardAngle;
            constrR = _constrainR + forwardAngle;
        }
        //bool IsInsideConstrain(float targetAngle)
        //{

        //    return targetAngle < _constrainL || targetAngle > _constrainR;
        //}
        bool isIncludeZero, isStartClosest;
        float ConstrainAngle(float cStart, float cEnd, float angle)
        {
            if(cStart < cEnd) // no 0
            {
                isIncludeZero = false;
                if (angle > cStart && angle < cEnd)
                    return angle;
                return ClosestAngle(angle, cStart, cEnd);
            }
            if(angle < cStart && angle > cEnd) //include 0
            {
                isIncludeZero = true;
                return ClosestAngle(angle, cStart, cEnd);
            }
            return angle;
        }
        float currTargetAngle;
        public void RotateTowardsTarget(Transform target)
        {
            UpdateOwnAngle();
            if(isConstrain)
            {
                UpdateParentAngle();
                UpdateConstrains(parentAngle);
            }

            Vector2 targetDir = target.ToVector2() - transform.position.ToVector2();
            float targetAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;

            if(isConstrain)
                targetAngle = ConstrainAngle(constrL, constrR, targetAngle);
            float currAngle = 0;

            if (targetAngle != currTargetAngle)
                currTargetAngle = AngleWrap(LerpAngle(currTargetAngle, targetAngle));
            else return;

            float angleDiff = Mathf.DeltaAngle(currTargetAngle, ownAngle);

            if (!_isWideConstrain || !isConstrain)
            {
                currAngle = Mathf.LerpAngle(ownAngle, currTargetAngle, LerpDist(angleDiff, 180, _turnSpeed, mRotationCurve));
            }
            else
            {
                if(isIncludeZero)
                {
                    if (ownAngle < currTargetAngle) ownAngle += 360;
                    else targetAngle += 360;
                }
                currAngle = AngleWrap(Mathf.Lerp(ownAngle, currTargetAngle, LerpDist(Mathf.Clamp(angleDiff, 0, 180), 180, _turnSpeed, mRotationCurve)));
            }
            _rBody.rotation = currAngle;
        }

        //constr < 180 = lerpangle
        //constr > 180 
        //if not include 0 lerp float
        //if include 0 lerp distance raw + Wrapangle after
        //lerpdist > 180 constant
        float LerpAngle(float currTarget, float newTarget)
        {
            float angleDiff = Mathf.Abs(Mathf.DeltaAngle(currTarget, newTarget));

            return Mathf.LerpAngle(currTarget, newTarget, LerpDist(angleDiff, 180, _turnSpeed, mRotationCurve)); /////
        }

        float LerpDist(float _diff, float _ratio, float _speed, AnimationCurve _curve)
        {
            _diff = Mathf.Abs(_diff);
            float distUnified = (_ratio / _diff) / _ratio;
            return Mathf.Clamp01(_curve.Evaluate(_diff / _ratio) * distUnified * _speed);
        }

        

        float ClosestAngle(float angle, float targetA, float targetB)
        {
            return Mathf.DeltaAngle(targetA, angle) < Mathf.DeltaAngle(targetB, angle) ? targetA : targetB;
        }

        bool IsWideConstrain()
        {
            if (Mathf.Abs(_constrainL) + Mathf.Abs(_constrainL) >= 180f)
                return true;
            return false;
        }

        public float AngleDifferenceToTarget(Transform target, bool isAbsolut)
        {
            float ownAngle = transform.up.ToVector2().GetAngle();
            float angleToTarget = (target.ToVector2() - transform.position.ToVector2()).GetAngle();
            float angleDiff = ownAngle - angleToTarget;
            if (angleDiff > 180) angleDiff = -180 + angleDiff % 180;

            return isAbsolut ? Mathf.Abs(angleDiff) : angleDiff;
        }
        float HalfAngle(float angle)
        {
            if (angle > 180) return angle -360;
            return angle;
        }

        float AngleWrap(float _angle)
        {
            return _angle < 0 ? 360 + _angle : _angle > 360 ? 0 : _angle;
        }
    }
}
