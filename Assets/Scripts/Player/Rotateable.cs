﻿/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : Rotateable.cs
* Date   : 17.04.22
* Author : Jan Apsel (JA)
*
* These coded instructions, statements, and computer programs contain
* proprietary information of the author and are protected by Federal
* copyright law. They may not be disclosed to third parties or copied
* or duplicated in any form, in whole or in part, without the prior
* written consent of the author.
*
* History:
*   17.04.22    JA	Created
******************************************************************************/

using System;
using AngleExtension;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [CustomEditor(typeof(Rotateable))]
    public class RotateableEditor : Editor
    {
        private Rotateable _rotateable; 
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            _rotateable = (Rotateable)target;

            _rotateable.RotationCurve = EditorGUILayout.CurveField("Rotation/ x:Dis y:Spd", _rotateable.RotationCurve, Color.red, new Rect(0, 0.05f, 1, 1));
            EditorGUILayout.MinMaxSlider("Constrain: " + ((int)_rotateable.ConstrStart).ToString() + "/" + ((int)_rotateable.ConstrEnd).ToString(), ref _rotateable.ConstrStart, ref _rotateable.ConstrEnd, 0, 360);

            if (GUI.changed)
                UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
            if (GUILayout.Button("Center"))
            {
                _rotateable.CenterConstrains();
                UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
            }
        }
    }
    [RequireComponent(typeof(Rigidbody2D))]
    internal class Rotateable : MonoBehaviour
    {
        [SerializeField] private Transform _parentT;
        [SerializeField] private float _turnSpeed;
        [SerializeField] private bool _isConstrained;

        [HideInInspector] public float ConstrStart;
        [HideInInspector] public float ConstrEnd;
        [HideInInspector] public AnimationCurve RotationCurve;
        private Rigidbody2D _rBody;

        [SerializeField][Range(0, 360)] private float _parentOffset;
        private float _constrS, _constrE, _ownAngle, _parentAngle;
        private bool _isWideConstrain;


        private void OnValidate()
        {
            UpdateAngles();
        }
        private void Awake()
        {
            _rBody = GetComponent<Rigidbody2D>();

            UpdateAngles();
            //ownAngle = constrE;
            //_rBody.rotation = constrE;
        }

        public void UpdateAngles()
        {
            UpdateOwnAngle();
            UpdateParentAngle();
            UpdateConstrains();
        }
        public void CenterConstrains()
        {
            float range = Mathf.Abs(ConstrStart - ConstrEnd) / 2;
            ConstrStart = AngleWrap(180 - range);
            ConstrEnd = AngleWrap(180 + range);
        }

        private void UpdateOwnAngle()
        {
            _ownAngle = transform.localEulerAngles.z;
        }

        private void UpdateParentAngle()
        {
            _parentAngle = AngleWrap(_parentT.localEulerAngles.z + 180 + _parentOffset);
        }

        private void UpdateConstrains()
        {
            _constrS = AngleWrap(ConstrStart + _parentAngle);
            _constrE = AngleWrap(ConstrEnd + _parentAngle);

            _isWideConstrain = IsWideConstrain();
        }

        private bool isIncludeZero;

        private float ConstrainAngle(float cStart, float cEnd, float angle)
        {
            if (cStart < cEnd) // no 0
            {
                isIncludeZero = false;
                if (angle > cStart && angle < cEnd)
                    return angle;
                return ClosestAngle(angle, cStart, cEnd);
            }
            if (angle < cStart && angle > cEnd) //include 0
            {
                isIncludeZero = true;
                return ClosestAngle(angle, cStart, cEnd);
            }
            return angle;
        }

        private float _currTargetAngle;
        public void RotateTowardsTarget(Transform target)
        {
            UpdateOwnAngle();
            if (_isConstrained)
            {
                UpdateParentAngle();
                UpdateConstrains();
            }

            Vector2 targetDir = target.ToVector2() - transform.position.ToVector2();
            float targetAngle = AngleWrap(Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f);
            if (_isConstrained)
                targetAngle = ConstrainAngle(_constrS, _constrE, targetAngle);
            float currAngle = 0;

            _currTargetAngle = targetAngle;
            //if (targetAngle != currTargetAngle)
            //    currTargetAngle = AngleWrap(LerpAngle(currTargetAngle, targetAngle));

            float angleDiff = Mathf.Abs(Mathf.DeltaAngle(_currTargetAngle, _ownAngle));

            if (!_isWideConstrain || !_isConstrained)
            {
                currAngle = Mathf.LerpAngle(_ownAngle, _currTargetAngle, LerpDist(angleDiff, 180, _turnSpeed, RotationCurve));
            }
            else
            {
                float tempTargetAngle = _currTargetAngle;
                //float tempOwnAngle = _ownAngle;
                if (isIncludeZero)
                {
                    if (_ownAngle <= ConstrEnd) _ownAngle += 360;
                    if (tempTargetAngle <= ConstrEnd) tempTargetAngle += 360;
                }
                currAngle = AngleWrap(Mathf.Lerp(_ownAngle, tempTargetAngle, LerpDist(Mathf.Clamp(angleDiff, 0, 180), 180, _turnSpeed, RotationCurve)));
            }
            _rBody.rotation = currAngle;
        }

        private float LerpAngle(float currTarget, float newTarget)
        {
            float angleDiff = Mathf.Abs(Mathf.DeltaAngle(currTarget, newTarget));

            return Mathf.LerpAngle(currTarget, newTarget, LerpDist(angleDiff, 180, _turnSpeed, RotationCurve)); /////
        }

        private float LerpDist(float diff, float ratio, float speed, AnimationCurve curve)
        {
            diff = Mathf.Clamp(Mathf.Abs(diff), 0.01f, ratio); ////---
            float distUnified = (ratio / diff) / ratio;
            return Mathf.Clamp01(curve.Evaluate(diff / ratio) * distUnified * speed);
        }

        private float ClosestAngle(float angle, float targetA, float targetB)
        {
            return Mathf.Abs(Mathf.DeltaAngle(targetA, angle)) < Mathf.Abs(Mathf.DeltaAngle(targetB, angle)) ? targetA : targetB;
        }

        private bool IsWideConstrain()
        {
            if (Mathf.Abs(ConstrStart - ConstrEnd) >= 180f)
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

        private float HalfAngle(float angle)
        {
            if (angle > 180) return angle - 360;
            return angle;
        }

        private float AngleWrap(float _angle)
        {
            return _angle < 0 ? 360 + _angle : _angle > 360 ? _angle - 360 : _angle;
        }
        private void OnDrawGizmosSelected()
        {
            if (_isConstrained)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, DegreeToV3Relative(ConstrStart + _parentAngle));
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, DegreeToV3Relative(ConstrEnd + _parentAngle));
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, DegreeToV3Relative(_parentAngle));

                Handles.color = Color.Lerp(Color.red, Color.clear, 0.7f);
                Handles.DrawSolidArc(transform.position, Vector3.forward, DegreeToV3Relative(ConstrStart + _parentAngle) - transform.position, Mathf.Abs(ConstrStart - ConstrEnd), 2.5f);
            }
        }

        private Vector3 DegreeToV3Relative(float degree)
        {
            return transform.position + degToV2(degree + 90f).ToVector3().normalized * 2.5f;
        }

        private Vector2 degToV2(float degree)
        {
            return new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad));
        }
    }
}