using UnityEditor;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AIIdleEditor.cs
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
    [CustomEditor(typeof(IdleAction))]
    public class AIIdleEditor : AIEditor
    {
        private IdleAction _aiStateAction;
        private Editor _aiConfigurationEditor;

        public override void OnInspectorGUI()
        {
            _aiStateAction = (IdleAction)target;
            
            DrawSettings(_aiStateAction.AIConifg, 
                _aiStateAction.OnUpdateSettings, 
                ref _aiStateAction.AiConfigFoldout, 
                ref _aiConfigurationEditor);

            base.OnInspectorGUI();
        }
    }
}


