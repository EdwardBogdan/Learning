using UnityEditor;
using MyProject.AIStateMachineSpace;
using static MyProject.CustomEditors.AIStateMachineEditor;
using MyProject.Utils.GUI;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

namespace MyProject.CustomEditors
{
    [CustomEditor(typeof(AIState), true)]
    public class AIStateEditor : Editor
    {
        private AIStateMachineEditor machineEditor;

        private AIState state;

        private readonly List<ExitFieldData> exitFields = new();

        private bool foldout = false;

        internal protected void Reload(AIStateMachineEditor machine)
        {
            state = (AIState)target;

            machineEditor = machine;

            exitFields.Clear();

            FieldInfo[] allFields = state.GetType().GetFields();

            foreach (FieldInfo field in allFields)
            {
                if (field.FieldType == typeof(AIState))
                {
                    ExitFieldData data = new()
                    {
                        info = field,
                        index = GetCurrentExitIndex((AIState)field.GetValue(state), machineEditor.createdStates)
                    };
                    exitFields.Add(data);
                }
            }
        }
        internal protected void DrawInspectorForMachine()
        {
            CustomGUILayout.DrawHorizontalLine(3);
            EditorGUILayout.BeginHorizontal();

            foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, $"AI Logic: {state.StateLogicName}");
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Remove State", GUILayout.Width(100)))
            {
                machineEditor.RemoveState(state);
                return;
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.EndHorizontal();
            EditorGUI.BeginChangeCheck();

            state.stateName = EditorGUILayout.TextField("State:", state.stateName);

            if (EditorGUI.EndChangeCheck())
            {
                RenameByName(state);
                machineEditor.ReloadExitNames();
                EditorUtility.SetDirty(state);
            }

            if (!foldout)
            {
                EditorGUILayout.EndFoldoutHeaderGroup();

                EditorGUILayout.Space(2);
                return;
            }

            DrawDefaultInspector();

            EditorGUILayout.Space();

            foreach (ExitFieldData aiStateField in exitFields)
            {
                CustomGUILayout.DrawHorizontalLine();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Exit: ");
                EditorGUILayout.LabelField(ObjectNames.NicifyVariableName(aiStateField.info.Name));//, dedicatedStyle);
                EditorGUILayout.EndHorizontal();

                EditorGUI.BeginChangeCheck();
                aiStateField.index = EditorGUILayout.Popup("is: ", aiStateField.index, machineEditor.createdStateNames.ToArray());

                if (EditorGUI.EndChangeCheck())
                {
                    aiStateField.info.SetValue(state, machineEditor.createdStates[aiStateField.index].state);
                    EditorUtility.SetDirty(state);
                }

                EditorGUILayout.Space();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        static internal protected void RenameByName(AIState state)
        {
            string assetPath = AssetDatabase.GetAssetPath(state);
            AssetDatabase.RenameAsset(assetPath, state.StateLogicName + $" ({state.stateName})");
        }
        class ExitFieldData
        {
            public FieldInfo info;
            public int index;
        }
    }
}
