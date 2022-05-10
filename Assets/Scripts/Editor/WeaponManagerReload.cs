using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponManager))]
public class WeaponManagerReload : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WeaponManager weaponManager = (WeaponManager)target;
        if(GUILayout.Button("Reload Weapons"))
        {
            weaponManager.ReloadWeapons();
        }
    }
}
