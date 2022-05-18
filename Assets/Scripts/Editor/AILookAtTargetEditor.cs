using UnityEditor;
using UnityEngine;

namespace AISystem
{
    [CustomEditor(typeof(AILookToEnemy))]
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
    }
}
    

