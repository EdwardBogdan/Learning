using CustomColorPack.Attributes;
using UnityEditor;
using UnityEngine;

namespace CustomColorPack.CustomEditors.Attributes
{
    [CustomPropertyDrawer(typeof(ColorChooseAttribute))]
    public class ColorChooseAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var names = CustomColorStorage.GetAllId();

            string currentName = CustomColorStorage.GetDataByColor(property.colorValue).Id;

            int index = Mathf.Max(names.IndexOf(currentName), 0);
            index = EditorGUI.Popup(position, property.displayName, index, names.ToArray());

            var pack = CustomColorStorage.I.ColorsBase[index];

            property.colorValue = pack.Color;
        }
    }
}
