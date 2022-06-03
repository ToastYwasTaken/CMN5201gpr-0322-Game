/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : EntityDamageTest.cs
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
