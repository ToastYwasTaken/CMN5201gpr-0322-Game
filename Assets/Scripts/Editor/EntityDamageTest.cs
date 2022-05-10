using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EntityStats))]
public class EntityDamageTest : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();


        

        EntityStats entityStats = (EntityStats)target;
        if (GUILayout.Button("Test Damage"))
        {
            entityStats.TestDealDamage();
        }
    }
}
