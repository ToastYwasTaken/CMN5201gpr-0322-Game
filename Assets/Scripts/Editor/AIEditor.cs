using UnityEditor;

namespace AISystem
{
    public class AIEditor : Editor
    {
        protected void DrawSettings(UnityEngine.Object settings, System.Action onSettingsUpdated, 
            ref bool foldout, ref Editor editor)
        {
            if (settings == null) return;
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);

            using var check = new EditorGUI.ChangeCheckScope();
            if (!foldout) return;

            CreateCachedEditor(settings, null, ref editor);
            editor.OnInspectorGUI();

            if (!check.changed) return;
            onSettingsUpdated?.Invoke();
        }

    }
}