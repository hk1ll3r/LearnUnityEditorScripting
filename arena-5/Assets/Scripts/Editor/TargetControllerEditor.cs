using System.Linq;
using UnityEngine;
using UnityEditor;

namespace NoSuchStudio.ExtendingEditor.Editor {
    [CustomEditor(typeof(TargetController))]
    [CanEditMultipleObjects]
    public class TargetControllerEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            SerializedProperty scoreProp = serializedObject.FindProperty("score");
            string valStr = scoreProp.intValue.ToString();
            if (scoreProp.hasMultipleDifferentValues) {
                valStr = EditorGUILayout.TextField(scoreProp.displayName, "<multiple values>");
                if (valStr != "<multiple values>") {
                    int valInt;
                    int.TryParse(valStr, out valInt);
                    scoreProp.intValue = valInt;
                }
            } else {
                valStr = EditorGUILayout.TextField(scoreProp.displayName, valStr);
                int valInt;
                int.TryParse(valStr, out valInt);
                scoreProp.intValue = valInt;
            }
            serializedObject.ApplyModifiedProperties();

            // DrawDefaultInspector();
        }
    }
}