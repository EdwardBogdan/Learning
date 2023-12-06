using Inventory.Attribute;
using UnityEditor;
using UnityEngine;

namespace Inventory
{
    [CustomPropertyDrawer(typeof(ItemsAttribute))]
    public class ItemsAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string[] ids = InventoryDefs.I.GetEquipmentIds();

            int index = Mathf.Max(System.Array.IndexOf(ids, property.stringValue), 0);
            index = EditorGUI.Popup(position, property.displayName, index, ids);

            property.stringValue = ids[index];
        }
    }
}
