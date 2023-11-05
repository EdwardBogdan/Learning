using Core.Inventory.Attribute;
using UnityEditor;
using UnityEngine;

namespace Core.Inventory.Editors
{
    [CustomPropertyDrawer(typeof(ItemsAttribute))]
    public class ItemsAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string[] ids = PlayerCore.InventorySettings.GetEquipmentIds();

            int index = Mathf.Max(System.Array.IndexOf(ids, property.stringValue), 0);
            index = EditorGUI.Popup(position, property.displayName, index, ids);

            property.stringValue = ids[index];
        }
    }
}
