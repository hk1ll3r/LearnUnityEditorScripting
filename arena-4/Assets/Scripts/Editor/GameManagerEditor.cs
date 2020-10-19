using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NoSuchStudio.ExtendingEditor.Editor {
    [CustomEditor(typeof(GameManager))]
    public class GameManagerEditor : UnityEditor.Editor {

        int[] levelOptionsInt;
        string[] levelOptionsStr;
        int level;
        string validStr;
        GameManager targetGameManager;

        protected GUIStyle styleValid;
        protected GUIStyle styleInvalid;

        private int GetLevel(string levelName) {
            int res;
            int.TryParse(levelName.Substring(6), out res);
            return res;
        }

        public void OnEnable() {
            levelOptionsInt = Enumerable.Range(1, 20).ToArray();
            levelOptionsStr = levelOptionsInt.Select(i => i.ToString()).ToArray();
            targetGameManager = (GameManager)target;

            styleValid = new GUIStyle();
            styleValid.normal.textColor = new Color(0f, 0.5f, 0f, 1f);

            styleInvalid = new GUIStyle();
            styleInvalid.normal.textColor = new Color(0.6f, 0f, 0f, 1f);

            validStr = targetGameManager.Validate();
        }

        public override void OnInspectorGUI() {
            // Validation Section
            if (GUILayout.Button("Validate Scene")) {
                validStr = targetGameManager.Validate();
            }
            bool isValid = string.IsNullOrEmpty(validStr);
            int numLines = 1;
            if (!isValid) {
                numLines += validStr.Split('\n').Length - 1;
            }
            EditorGUILayout.LabelField("Validation", isValid ? "All Set!" : validStr, isValid ? styleValid : styleInvalid, GUILayout.Height(numLines * 10));
            // EditorGUILayout.Separator(); // seems like Separator() doesn't work from 2019.4.x
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            // Level Name Section
            level = GetLevel(targetGameManager.LevelName);
            Undo.RecordObject(targetGameManager, "LevelName change from our Editor");
            int levelIndex = ArrayUtility.IndexOf(levelOptionsInt, level);
            levelIndex = EditorGUILayout.Popup("Level Index", levelIndex, levelOptionsStr);
            level = levelOptionsInt[levelIndex];
            targetGameManager.LevelName = $"Level {level}";
            EditorGUILayout.LabelField("Level Name", targetGameManager.LevelName);

            /*level = GetLevel(targetGameManager.LevelName);
            serializedObject.Update(); // Sync the serialized properties with the gameobject with GameManager component.
            int levelIndex = ArrayUtility.IndexOf(levelOptionsInt, level);
            levelIndex = EditorGUILayout.Popup("Level Index", levelIndex, levelOptionsStr);
            level = levelOptionsInt[levelIndex];
            serializedObject.FindProperty("levelName").stringValue = $"Level {level}";
            EditorGUILayout.LabelField("Level Name", targetGameManager.LevelName);
            serializedObject.ApplyModifiedProperties(); // write the potentially changed levelName property back to the gameobject with the GameManager component.            
                                                        // comment out this line and you won't be able to change level from inspector.
*/
            // Default Inspector Section
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            // EditorGUILayout.Separator(); // seems like Separator() doesn't work from 2019.4.x
            DrawDefaultInspector();
        }
    }
}

// Adding the Validate button
/*namespace NoSuchStudio.ExtendingEditor.Editor {
    [CustomEditor(typeof(GameManager))]
    public class GameManagerEditor : UnityEditor.Editor {

        string validStr;
        GameManager targetGameManager;

        protected GUIStyle styleValid;
        protected GUIStyle styleInvalid;

        public void OnEnable() {
            styleValid = new GUIStyle();
            styleValid.normal.textColor = new Color(0f, 0.5f, 0f, 1f);

            styleInvalid = new GUIStyle();
            styleInvalid.normal.textColor = new Color(0.6f, 0f, 0f, 1f);

            targetGameManager = (GameManager)target;
            validStr = targetGameManager.Validate();
        }

        public override void OnInspectorGUI() {
            if (GUILayout.Button("Validate Scene")) {
                validStr = targetGameManager.Validate();
            }
            bool isValid = string.IsNullOrEmpty(validStr);
            int numLines = 1;
            if (!isValid) {
                numLines += validStr.Split('\n').Length - 1;
            }
            EditorGUILayout.LabelField("Validation", isValid ? "All Set!" : validStr, isValid ? styleValid : styleInvalid, GUILayout.Height(numLines * 10));

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            // EditorGUILayout.Separator(); // seems like Separator() doesn't work from 2019.4.x
            DrawDefaultInspector();
        }
    }
}*/

// Replicate the default editor.
/*namespace NoSuchStudio.ExtendingEditor.Editor {
    [CustomEditor(typeof(GameManager))]
    public class GameManagerEditor : UnityEditor.Editor {

        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.HelpBox("This is our custom editor!", MessageType.Info);
        }
    }
}*/
