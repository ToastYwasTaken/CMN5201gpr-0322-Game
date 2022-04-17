using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AISystem
{
    [CustomEditor(typeof(IdleAction))]
    public class AISwarmEditor : AIEditor
    {
        private SwarmAction _aiStateAction;
        private Editor _aiConfigurationEditor;

        public override void OnInspectorGUI()
        {
            _aiStateAction = (SwarmAction)target;
            
            DrawSettings(_aiStateAction.AIConifg, 
                _aiStateAction.OnUpdateSettings, 
                ref _aiStateAction.aiConfigFoldout, 
                ref _aiConfigurationEditor);

            base.OnInspectorGUI();
        }
    }

}
