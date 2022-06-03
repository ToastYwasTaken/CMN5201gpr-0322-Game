/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : OverdriveManagerReload.cs
* Date   : 03.06.2022
* Author : Alexander Sigmund (AS)
*
* These coded instructions, statements, and computer programs contain
* proprietary information of the author and are protected by Federal
* copyright law. They may not be disclosed to third parties or copied
* or duplicated in any form, in whole or in part, without the prior
* written consent of the author.
*
* History:
*
******************************************************************************/
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OverdriveManager))]
public class OverdriveManagerReload : Editor

{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        OverdriveManager overdriveManager = (OverdriveManager)target;
        if (GUILayout.Button("Reload Overdrive Chips"))
        {
            overdriveManager.ReloadOverdriveChips();
        }
    }
}
