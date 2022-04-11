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
        [SerializeField] bool isConstrained;
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

        float lastDir, constrL, constrR, forwardAngle, targetLerp;
        bool _isWideConstrain;

        private void Awake()
        {
            _rBody = GetComponent<Rigidbody2D>();
        }

        public void RotateToTarget(Transform target)
        {
            Vector2 targetV2 = target.ToVector2() - _rBody.position;
            float targetAngle = Mathf.Atan2(targetV2.y, targetV2.x) * Mathf.Rad2Deg - 90f;
            if(targetAngle != targetLerp)
                targetLerp = LerpAngle(targetLerp, targetAngle);

            float angleDiff = Mathf.DeltaAngle(targetLerp, lastDir);
            if (angleDiff == 0) return;

            float currAngle = AngleWrap(Mathf.LerpAngle(lastDir, targetLerp, LerpDist(angleDiff)));

            _rBody.rotation = currAngle;
            lastDir = currAngle;
        }

        //constrain >180 / lerp to <180 ? / ..

        float LerpAngle(float currTarget, float newTarget)
        {
            float angleDiff = Mathf.Abs(Mathf.DeltaAngle(currTarget, newTarget));

            return Mathf.LerpAngle(currTarget, newTarget, LerpDist(angleDiff)); /////
        }

        float LerpDist(float _angleDiff)
        {
            _angleDiff = Mathf.Abs(_angleDiff);
            float distUnified = (180 / _angleDiff) / 180;
            return Mathf.Clamp01(mRotationCurve.Evaluate(_angleDiff / 180) * distUnified * _turnSpeed); //deltatime?
        }

        void UpdateForwardAngle()
        {
            forwardAngle = _parent.ToVector2().GetAngle();
        }

        void UpdateConstrains(float forwardAngle)
        {
            constrL = _constrainL + forwardAngle;
            constrR = _constrainR + forwardAngle;
        }
        bool IsInsideConstrain(float targetAngle)
        {
            return targetAngle < _constrainL || targetAngle > _constrainR;
        }
        bool IsWideConstrain()
        {
            if (Mathf.Abs(_constrainL) + Mathf.Abs(_constrainL) > 180f)
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

        float AngleWrap(float _angle)
        {
            return _angle < 0 ? 360 + _angle : _angle > 360 ? 0 : _angle;
        }
    }
}
