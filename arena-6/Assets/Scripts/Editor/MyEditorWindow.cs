using UnityEditor;
using UnityEngine;
using System.Linq;

namespace NoSuchStudio.ExtendingEditor.Editor {

    public class MyEditorWindow : EditorWindow {

        [MenuItem("Window/My Menu/My Window", priority = 10)]
        public static void OpenFromWindow() {
            if (HasOpenInstances<MyEditorWindow>()) {
                GetWindow<MyEditorWindow>().Close();
            } else {
                var myWindow = GetWindow<MyEditorWindow>();
                myWindow.titleContent = new GUIContent("My Window");
            }
        }

        private string validationStr;
        private bool foldState;

        private void OnEnable() {
            validationStr = "-";
            foldState = true;
        }

        private void OnGUI() {
            // EditorGUILayout.LabelField("Label", "TODO GUI");
            if (GUILayout.Button("Validate Scene")) {
                validationStr = MyMenus.ValidateSceneNoDialog();
                if (string.IsNullOrEmpty(validationStr)) validationStr = "All Set!";
            }
            EditorGUILayout.LabelField("Result", validationStr);

            var arena = GameObject.FindGameObjectWithTag("Arena");
            float area = arena == null ? 0f : arena.transform.localScale.x * arena.transform.localScale.z * Mathf.PI / 4f;
            var targets = FindObjectsOfType<TargetController>().ToList();
            int totalScore = targets.Sum(tc => tc.Score);
            int count = targets.Count;
            foldState = EditorGUILayout.Foldout(foldState, "Level Stats");
            if (foldState) {
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField("Arena Size", $"{area:0.00}");
                EditorGUILayout.LabelField("Target Count", $"{count}");
                EditorGUILayout.LabelField("Total Score", $"{totalScore}");
                EditorGUI.indentLevel--;
            }
        }
    }
}