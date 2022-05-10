using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AISystem
{
    [CustomEditor(typeof(FlockingAction))]
   public class AIFlockingEditor : AIEditor
   {
       private FlockingAction _aiStateAction;
       private Editor _aiConfigurationEditor;

       public override void OnInspectorGUI()
       {
           _aiStateAction = (FlockingAction)target;
            
           DrawSettings(_aiStateAction.AIConifg, 
               _aiStateAction.OnUpdateSettings, 
               ref _aiStateAction.AiConfigFoldout, 
               ref _aiConfigurationEditor);

           base.OnInspectorGUI();
       }     
   } 
}


