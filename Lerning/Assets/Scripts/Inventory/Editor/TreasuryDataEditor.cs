using Core.Data.ObservableProperty;
using Core.Data.PersistentProperty;
using CustomUnityUtils.GUI;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Inventory.Editors
{
    [CustomEditor(typeof(TreasureData))]
    public class TreasuryDataEditor : Editor
    {
        private static readonly List<ItemEditPropertyes> list = new();

        private static TreasureData obj;
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Player Treasury Data");

            CustomGUILayout.DrawHorizontalLine(3);

            obj.MaxValue = (int)Mathf.Clamp(obj.MaxValue, 0, Mathf.Infinity);

            obj.MaxValue = EditorGUILayout.IntField("Max Value", obj.MaxValue);

            foreach (var item in list)
            {
                item.foldout = EditorGUILayout.BeginFoldoutHeaderGroup(item.foldout, $"{item.id}");

                if (!item.foldout)
                {
                    EditorGUILayout.EndFoldoutHeaderGroup();
                    continue;
                }

                item.currentValue.Value = EditorGUILayout.IntSlider("Current Value", item.currentValue.Value, 0, obj.MaxValue);

                item.savedValue.Value = EditorGUILayout.IntSlider("Saved Value", item.savedValue.Value, 0, obj.MaxValue);

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

            obj = (TreasureData)target;

            TreasureClass[] mas = obj.EditTreasures;

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

            public IntPersistentProperty savedValue;

            public ItemEditPropertyes(TreasureClass obj)
            {
                id = obj.Id;

                currentValue = obj.EditCurrentValueModel;

                savedValue = obj.EditSavedValueModel;
            }
        }
    }
}
