using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using CustomUnityUtils.Attributes;

namespace CustomUnityUtils.Editors.Attributes
{
    [CustomPropertyDrawer(typeof(TagAttribute))]
    public class TagAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string[] tags = UnityEditorInternal.InternalEditorUtility.tags;

            List<string> list = new();

            foreach (string tag in tags)
            {
                list.Add(tag);
            }

            int index = Mathf.Max(list.IndexOf(property.stringValue), 0);
            index = EditorGUI.Popup(position, property.displayName, index, list.ToArray());

            property.stringValue = list[index];
        }
    }
}
