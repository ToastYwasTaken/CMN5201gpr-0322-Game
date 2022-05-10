using System.Collections;
using System.Collections.Generic;
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
