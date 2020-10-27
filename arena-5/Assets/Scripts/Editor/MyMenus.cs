using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace NoSuchStudio.ExtendingEditor.Editor {

    public static class MyMenus {

        public static string ValidateSceneNoDialog() {
            string res;
            var obj = GameObject.FindGameObjectWithTag("GameController");
            var gameManager = obj?.GetComponent<GameManager>();
            if (!gameManager) {
                res = "No GameManager object in scene. Go ahead and add a GameManager component to a game object in your scene and set the tag of the object to 'GameController'";
            } else {
                Selection.objects = new Object[] { obj };
                res = gameManager.Validate();
            }
            return res;
        }

        public static void ValidateScene() {
            var obj = GameObject.FindGameObjectWithTag("GameController");
            var gameManager = obj?.GetComponent<GameManager>();
            if (!gameManager) {
                EditorUtility.DisplayDialog("No GameManager object in scene.", "Go ahead and add a GameManager component to a game object in your scene and set the tag of the object to 'GameController'", "OK");
                return;
            } else {
                Selection.objects = new Object[] { obj };
                string res = gameManager.Validate();
                if (string.IsNullOrEmpty(res)) {
                    EditorUtility.DisplayDialog("Scene Validation", "Scene is valid!", "Ok");
                } else {
                    EditorUtility.DisplayDialog("Scene Validation", "Scene is invalid!", "Errr");
                }
            }
        }

        [MenuItem("My Menu/Validate Scene")]
        public static void ValidateFromMyMenu() {
            ValidateScene();
        }

        [MenuItem("My Menu/Validate Scene", true)]
        public static bool ValidateValidateFromMyMenu() {
            return false;
        }

        [MenuItem("Assets/My Assets/Validate Scene", priority = 100)]
        public static void ValidateFromAssetsMenu() {
            ValidateScene();
        }

        [MenuItem("Assets/Create/My Assets/My Material", priority = 100)]
        public static void CreateNewMaterialAsset() {
            Material material = new Material(Shader.Find("Specular"));
            AssetDatabase.CreateAsset(material, "Assets/Materials/CreatedMaterial.mat");
        }

        [MenuItem("Component/My Components/Target", priority = 100)]
        public static void CreateNewComponent() {
            ValidateScene();
        }

        // code copied and modified from https://docs.unity3d.com/ScriptReference/MenuItem.html
        [MenuItem("GameObject/My Objects/Target", priority = 10)]
        public static void CreateNewTarget(MenuCommand menuCommand) {
            var go = new GameObject("New Target");
            go.AddComponent<TargetController>();
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }

        [MenuItem("Window/My Menu/ValidateScene", priority = 10)]
        public static void ValidateFromWindowMenu() {
            ValidateScene();
        }

        [MenuItem("Help/My Menu/ValidateScene", priority = 10)]
        public static void ValidateFromHelpMenu() {
            ValidateScene();
        }

        [MenuItem("CONTEXT/GameManager/Validate Scene")]
        static void ValidateSceneContext() {
            ValidateScene();
        }

        [MenuItem("CONTEXT/Text/Set Font Size")]
        static void SetTextFontSize(MenuCommand command) {
            Text txt = (Text)command.context;
            txt.fontSize = 36;
        }

    }
}