using UnityEditor;
using UnityEngine;

namespace AISystem
{
    [CustomEditor(typeof(AIFieldOfView))]
    public class AIFieldOfViewEditor : Editor
    {
        private AIFieldOfView _fieldOfView;
        
        private void OnSceneGUI()
        { 
            _fieldOfView = (AIFieldOfView)target;
    
            if (_fieldOfView.UseAura)
            {
                Handles.color = Color.cyan;
                Handles.DrawWireArc(_fieldOfView.transform.position, Vector3.forward, 
                    _fieldOfView.DirectionFromAngle(_fieldOfView.AuraRadius, false), 360, _fieldOfView.AuraRadius);
            }
    
            Handles.color = Color.green;
            Handles.DrawWireArc(_fieldOfView.transform.position, Vector3.forward, 
                _fieldOfView.DirectionFromAngle(_fieldOfView.ViewRadius, false), 360, _fieldOfView.ViewRadius);
    
            Vector3 viewAngleA = _fieldOfView.DirectionFromAngle(-_fieldOfView.ViewAngle / 2, false);
            Vector3 viewAngleB = _fieldOfView.DirectionFromAngle(_fieldOfView.ViewAngle / 2, false);
            Handles.DrawLine(_fieldOfView.transform.position, _fieldOfView.transform.position + (viewAngleA * _fieldOfView.ViewRadius));
            Handles.DrawLine(_fieldOfView.transform.position, _fieldOfView.transform.position + (viewAngleB * _fieldOfView.ViewRadius));
    
            Handles.color = Color.red;
            if (_fieldOfView.Target != null)
                Handles.DrawLine(_fieldOfView.transform.position, _fieldOfView.Target.transform.position);
        }
    }
}

