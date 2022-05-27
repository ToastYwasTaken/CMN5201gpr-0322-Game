
//TODO:
//Steuerung nach Richtung.

using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [CustomEditor(typeof(PlayerController))]
    //[CanEditMultipleObjects]
    public class PlayerControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            PlayerController playerController = (PlayerController)target;

            playerController.mCurve = EditorGUILayout.CurveField("Size", playerController.mCurve, Color.blue, new Rect(0, 0.05f, 1, 1));
        }
    }
}