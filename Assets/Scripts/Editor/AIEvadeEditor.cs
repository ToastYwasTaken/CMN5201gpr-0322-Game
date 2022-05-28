using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AISystem
{
    [CustomEditor(typeof(EvadeAction))]
    public class AIEvadeEditor : AIEditor
    {
        private EvadeAction _aiStateAction;
        private Editor _aiConfigurationEditor;

        public override void OnInspectorGUI()
        {
            _aiStateAction = (EvadeAction)target;

            DrawSettings(_aiStateAction.AIConifg,
                _aiStateAction.OnUpdateSettings,
                ref _aiStateAction.AiConfigFoldout,
                ref _aiConfigurationEditor);

            base.OnInspectorGUI();
        }
    }
}

