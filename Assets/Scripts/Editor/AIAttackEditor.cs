using UnityEditor;

namespace AISystem
{
    [CustomEditor(typeof(AttackAction))]
   public class AIAttackEditor : AIEditor
   {
       private AttackAction _aiStateAction;
       private Editor _aiConfigurationEditor;

       public override void OnInspectorGUI()
       {
           _aiStateAction = (AttackAction)target;
            
           DrawSettings(_aiStateAction.AIConifg, 
               _aiStateAction.OnUpdateSettings, 
               ref _aiStateAction.AiConfigFoldout, 
               ref _aiConfigurationEditor);

           base.OnInspectorGUI();
       }
   } 
}

