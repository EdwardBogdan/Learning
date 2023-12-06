using System;
using UnityEditor;
using UnityEngine;

namespace AIStateSystem.Editors
{
    [CustomPropertyDrawer(typeof(AIAreaChooseAttribute))]
    public class AIAreaChooseAttributeEditor : PropertyDrawer
    {
        public static AIStateMachine core;
        public static string[] names;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (core == null)
            {
                EditorGUI.PropertyField(position, property);
                return;
            }

            int index = -1;

            string value = property.stringValue;

            foreach (var item in names)
            {
                if (item == value)
                {
                    index = Array.IndexOf(names, item);
                    break;
                }
            }

            EditorGUI.BeginChangeCheck();

            index = EditorGUI.Popup(position, property.displayName, index, names);

            if (EditorGUI.EndChangeCheck())
            {
                if (index >= names.Length)
                {
                    index = -1;
                    return;
                }
                property.stringValue = names[index];
            }
        }
    }
}
