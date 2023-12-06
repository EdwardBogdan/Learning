using Core.Data.ObservableProperty;
using Core.Data.PersistentProperty;
using System.Collections.Generic;
using UnityEditor;
using CustomUnityUtils.GUI;
using UnityEngine;

namespace Inventory.Editors
{
    [CustomEditor(typeof(EquipmentData))]
    public class EquipmentDataEditor : Editor
    {
        private static readonly List<ItemEditPropertyes> list = new();

        private static EquipmentData obj;
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Player Equipment Data");

            CustomGUILayout.DrawHorizontalLine(3);

            foreach (var item in list)
            {
                item.foldout = EditorGUILayout.BeginFoldoutHeaderGroup(item.foldout, $"{item.id}");

                if (!item.foldout)
                {
                    EditorGUILayout.EndFoldoutHeaderGroup();
                    continue;
                }

                item.currentValue.Value = EditorGUILayout.IntSlider("Current Value", item.currentValue.Value, 0, item.currentMaxValue.Value);

                item.currentMaxValue.Value = EditorGUILayout.IntSlider("Current Max", item.currentMaxValue.Value, 0, 99);

                item.savedValue.Value = EditorGUILayout.IntSlider("Saved Value", item.savedValue.Value, 0, item.savedMaxValue.Value);

                item.savedMaxValue.Value = EditorGUILayout.IntSlider("Saved Max", item.savedMaxValue.Value, 0, 99);

                EditorGUILayout.EndFoldoutHeaderGroup();
            }

            EditorGUILayout.Space(10);

            if (GUILayout.Button("Reload Equipment Data"))
            {
                obj.EditReload();
            }
            if (GUILayout.Button("Resave Equipment Data"))
            {
                obj.EditResave();
            }
            if (GUILayout.Button("Recreate Equipment Data"))
            {
                obj.EditRecreate();
            }

            EditorUtility.SetDirty(obj);
        }

        private void OnEnable()
        {
            list.Clear();

            obj = (EquipmentData)target;

            EquipmentClass[] mas = obj.ItemList;

            foreach (var item in mas)
            {
                ItemEditPropertyes itemData = new(item);

                list.Add(itemData);
            }
        }

        private class ItemEditPropertyes
        {
            public bool foldout = false;

            public string id;

            public IntProperty currentValue;
            public IntProperty currentMaxValue;
                   
            public IntPersistentProperty savedValue;
            public IntPersistentProperty savedMaxValue;

            public ItemEditPropertyes(EquipmentClass obj)
            {
                id = obj.Id;

                currentValue = obj.EditCurrentValueModel;
                currentMaxValue = obj.EditCurrentMaxValueModel;

                savedValue = obj.EditSavedValueModel;
                savedMaxValue = obj.EditSavedMaxValueModel;
            }
        }
    }
}
