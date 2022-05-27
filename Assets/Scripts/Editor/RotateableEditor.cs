/*****************************************************************************
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

            //_rotateable.RotationCurve = EditorGUILayout.CurveField
            //    ("Rotation/ x:Dis y:Spd", _rotateable.RotationCurve, 
            //    Color.red, new Rect(0, 0.05f, 1, 1));

            EditorGUILayout.MinMaxSlider("Constrain: " + 
                ((int)_rotateable.ConstrStart).ToString() + "/" + 
                ((int)_rotateable.ConstrEnd).ToString(), ref _rotateable.ConstrStart, 
                ref _rotateable.ConstrEnd, 0, 360);

            if (GUI.changed)
                UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
            if (GUILayout.Button("Center"))
            {
                _rotateable.CenterConstrains();
                UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
            }
        }
    }
}

//float tempTargetAngle = _currTargetAngle;
////float tempOwnAngle = _ownAngle;
//float tempOwnAngle = _ownAngle;
//if (isIncludeZero)
//{
//    if (_ownAngle <= _constrE && _ownAngle >= 0f) _ownAngle += 360;
//    if (tempTargetAngle <= _constrE && tempTargetAngle >= 0f) tempTargetAngle += 360;
//    if (tempOwnAngle <= _constrE) tempOwnAngle += 360;
//    if (tempTargetAngle <= _constrE) tempTargetAngle += 360;
//}
//currAngle = AngleWrap(Mathf.Lerp(_ownAngle, tempTargetAngle, LerpDist(Mathf.Clamp(angleDiff, 0.01f, 180), 180, _turnSpeed, _rotationCurve)));
//currAngle = AngleWrap(Mathf.Lerp(tempOwnAngle, tempTargetAngle, LerpDist(Mathf.Clamp(angleDiff, 0.01f, 180), 180, _turnSpeed, _rotationCurve)));
