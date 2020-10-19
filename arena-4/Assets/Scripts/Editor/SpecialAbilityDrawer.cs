using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NoSuchStudio.ExtendingEditor.Editor {
    //[CustomPropertyDrawer(typeof(SpecialAbility))]
    public class SpecialAbilityPropertyDrawer : PropertyDrawer {

        private int GetEnumValueIndex(SpecialAbilityType sat) {
            return Array.IndexOf(Enum.GetValues(typeof(SpecialAbilityType)), sat);
        }

        private SpecialAbilityType GetEnumValueFromIndex(int index) {
            return (SpecialAbilityType)Enum.GetValues(typeof(SpecialAbilityType)).GetValue(index);
        }

        private void ShowTypeField(Rect position, SerializedProperty property) {
            int oldInt = property.enumValueIndex;
            SpecialAbilityType oldType = GetEnumValueFromIndex(property.enumValueIndex);
            SpecialAbilityType newType = oldType;
            float toggleWidth = position.width / 3f;
            // Debug.Log($"toggle width: {toggleWidth} height: {position.height}");
            Rect rectDash = new Rect(position.min.x, position.min.y, toggleWidth, position.height);
            if (EditorGUI.ToggleLeft(rectDash, "Dash", newType == SpecialAbilityType.Dash)) {
                newType = SpecialAbilityType.Dash;
            } else if (newType == SpecialAbilityType.Dash) {
                newType = SpecialAbilityType.None;
            }
            Rect rectBounce = new Rect(position.min.x + toggleWidth, position.min.y, toggleWidth, position.height);
            if (EditorGUI.ToggleLeft(rectBounce, "Bounce", newType == SpecialAbilityType.Bounce)) {
                newType = SpecialAbilityType.Bounce;
            } else if (newType == SpecialAbilityType.Bounce) {
                newType = SpecialAbilityType.None;
            }
            Rect rectInvis = new Rect(position.min.x + 2 * toggleWidth, position.min.y, toggleWidth, position.height);
            if (EditorGUI.ToggleLeft(rectInvis, "Invis", newType == SpecialAbilityType.Invisibility)) {
                newType = SpecialAbilityType.Invisibility;
            } else if (newType == SpecialAbilityType.Invisibility) {
                newType = SpecialAbilityType.None;
            }
            // Debug.Log($"old type: {oldInt}:{oldType} newtype: {newType}\n property {property.displayName}");
            property.enumValueIndex = GetEnumValueIndex(newType);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            //EditorGUI.LabelField(position, label, new GUIContent("TODO"));

            /*EditorGUI.BeginProperty(position, label, property);
            Rect rectFoldout = new Rect(position.min.x, position.min.y, position.size.x, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.Foldout(rectFoldout, property.isExpanded, label);
            int lines = 1;
            if (property.isExpanded) {
                Rect rectType = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rectType, property.FindPropertyRelative("type"));
                Rect rectDuration = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rectDuration, property.FindPropertyRelative("duration"));
                Rect rectCooldown = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rectCooldown, property.FindPropertyRelative("cooldown"));
                Rect rectPower = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rectPower, property.FindPropertyRelative("power"));
            }
            Rect rectHelpBox = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
            EditorGUI.HelpBox(rectHelpBox, "This is our property drawer", MessageType.Info);
            EditorGUI.EndProperty();*/

            EditorGUI.BeginProperty(position, label, property);
            Rect rectFoldout = new Rect(position.min.x, position.min.y, position.size.x, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.Foldout(rectFoldout, property.isExpanded, label);
            if (property.isExpanded) {
                // Debug.Log($"type position size: {position.width}x{position.height}");
                Rect rectType = new Rect(position.min.x + EditorGUIUtility.labelWidth, position.min.y + EditorGUIUtility.singleLineHeight, position.size.x - EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);
                SerializedProperty propType = property.FindPropertyRelative("type");
                ShowTypeField(rectType, propType);
                var curProp = GetEnumValueFromIndex(propType.enumValueIndex);
                if (curProp == SpecialAbilityType.None) {
                    // show no fields
                } else {
                    Rect rectCooldown = new Rect(position.min.x, position.min.y + 2 * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                    EditorGUI.PropertyField(rectCooldown, property.FindPropertyRelative("cooldown"));
                    if (curProp == SpecialAbilityType.Invisibility) {
                        Rect rectDuration = new Rect(position.min.x, position.min.y + 3 * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                        EditorGUI.PropertyField(rectDuration, property.FindPropertyRelative("duration"));
                    } else {
                        Rect rectPower = new Rect(position.min.x, position.min.y + 3 * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                        EditorGUI.PropertyField(rectPower, property.FindPropertyRelative("power"));
                    }
                }
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            int totalLines = 1;

            /*if (property.isExpanded) {
                totalLines += 4;
            }*/

            if (property.isExpanded) {
                totalLines++; // for type field
                SerializedProperty propType = property.FindPropertyRelative("type");
                switch (GetEnumValueFromIndex(propType.enumValueIndex)) {
                    case SpecialAbilityType.None:
                        break;
                    case SpecialAbilityType.Dash:
                    case SpecialAbilityType.Bounce:
                    case SpecialAbilityType.Invisibility:
                        totalLines += 2;
                        break;
                }
            }

            return EditorGUIUtility.singleLineHeight * totalLines + EditorGUIUtility.standardVerticalSpacing * (totalLines - 1);
        }
    }
}