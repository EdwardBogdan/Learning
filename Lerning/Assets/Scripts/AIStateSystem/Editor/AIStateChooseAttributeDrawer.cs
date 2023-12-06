using UnityEditor;
using UnityEngine;
using Unity.VisualScripting;
using System;

namespace AIStateSystem.Editors
{
    [CustomPropertyDrawer(typeof(AIStateChooseAttribute))]
    public class AIStateChooseAttributeDrawer : PropertyDrawer
    {
        public static AILogicCore core;
        public static string[] names;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (core == null || core.List.Count <= 0)
            {
                EditorGUI.PropertyField(position, property);
                return;
            }

            int index = -1;

            AIState value = (AIState)property.GetUnderlyingValue();

            foreach (var item in core.List)
            {
                if (item.State == value)
                {
                    index = Array.IndexOf(core.List.ToArray(), item);
                    break;
                }
            }

            EditorGUI.BeginChangeCheck();

            index = EditorGUI.Popup(position, property.displayName, index, names);

            if (EditorGUI.EndChangeCheck())
            {
                property.SetUnderlyingValue(core.List[index].State);
            }
        }
    }
}
