using UnityEditor;
using UnityEngine;

namespace AISystem
{
    [CustomEditor(typeof(AILookToEnemy))]
    [CanEditMultipleObjects]
    public class AILookAtTargetEditor : Editor
    {
        private AILookToEnemy _lookAtTarget;

        public override void OnInspectorGUI()
        {
            _lookAtTarget = (AILookToEnemy)target;

            if (GUILayout.Button("Set default rotation"))
            {
                _lookAtTarget.ResetLookAt();
            }

            base.OnInspectorGUI();
        }
        private void OnSceneGUI()
        {
            var fieldOfView = (AILookToEnemy)target;

            if (fieldOfView.UseAura)
            {
                Handles.color = Color.cyan;
                Handles.DrawWireArc(fieldOfView.transform.position, Vector3.up, Vector3.forward, 360, fieldOfView.AuraRadius);
            }
         
            Handles.color = Color.green;
            Handles.DrawWireArc(fieldOfView.transform.localPosition, Vector3.up, Vector3.forward, 360, fieldOfView.ViewDistance);

            Vector3 viewAngleA = fieldOfView.DirectionFromAngle(-fieldOfView.ViewAngle / 2, false);
            Vector3 viewAngleB = fieldOfView.DirectionFromAngle(fieldOfView.ViewAngle / 2, false);
            Handles.DrawLine(fieldOfView.transform.position, fieldOfView.transform.position + (viewAngleA * fieldOfView.ViewDistance));
            Handles.DrawLine(fieldOfView.transform.position, fieldOfView.transform.position + (viewAngleB * fieldOfView.ViewDistance));

            Handles.color = Color.red;
            if (fieldOfView.VisibleObject != null)
                Handles.DrawLine(fieldOfView.transform.position, fieldOfView.VisibleObject.transform.position);
        }
    }
}


