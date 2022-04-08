using UnityEditor;

namespace AISystem
{
    [CustomEditor(typeof(PatrolAction))]
    public class AIPatrolEditor : AIEditor
    {
        private PatrolAction _aiStateAction;
        private Editor _aiConfigurationEditor;

        public override void OnInspectorGUI()
        {
            _aiStateAction = (PatrolAction)target;
            
            DrawSettings(_aiStateAction.AIConifg, 
                _aiStateAction.OnUpdateSettings, 
                ref _aiStateAction.aiConfigFoldout, 
                ref _aiConfigurationEditor);

            base.OnInspectorGUI();
        }
    }
}


