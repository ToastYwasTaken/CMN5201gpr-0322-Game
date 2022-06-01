using UnityEditor;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AIEditor.cs
* Date : 09.04.2022
* Author : René Kraus (RK)
*
* These coded instructions, statements, and computer programs contain
* proprietary information of the author and are protected by Federal
* copyright law. They may not be disclosed to third parties or copied
* or duplicated in any form, in whole or in part, without the prior
* written consent of the author.
******************************************************************************/
namespace AISystem
{
    /// <summary>
    /// Showing the AI configuration in  the inspector
    /// </summary>
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