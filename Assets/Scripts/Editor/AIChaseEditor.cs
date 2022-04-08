using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AISystem
{
    [CustomEditor(typeof(ChaseAction))]
    public class AIChaseEditor : AIEditor
    {
        private ChaseAction _aiStateAction;
        private Editor _aiConfigurationEditor;

        public override void OnInspectorGUI()
        {
            _aiStateAction = (ChaseAction)target;
            
            DrawSettings(_aiStateAction.AIConifg, 
                _aiStateAction.OnUpdateSettings, 
                ref _aiStateAction.aiConfigFoldout, 
                ref _aiConfigurationEditor);

            base.OnInspectorGUI();
        }
    }
}


