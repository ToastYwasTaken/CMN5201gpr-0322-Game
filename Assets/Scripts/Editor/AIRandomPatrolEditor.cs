using UnityEditor;

namespace AISystem
{
    [CustomEditor(typeof(RandomPatrolAction))]
    public class AIRandomPatrolEditor : AIEditor
    {
        private RandomPatrolAction _aiStateAction;
        private Editor _aiConfigurationEditor;

        public override void OnInspectorGUI()
        {
            _aiStateAction = (RandomPatrolAction)target;

            DrawSettings(_aiStateAction.AIConifg,
                _aiStateAction.OnUpdateSettings,
                ref _aiStateAction.AiConfigFoldout,
                ref _aiConfigurationEditor);

            base.OnInspectorGUI();
        }
    }
}