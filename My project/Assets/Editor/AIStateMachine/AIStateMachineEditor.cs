using MyProject.AIStateMachineSpace;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.IO;
using System;
using MyProject.Utils.GUI;
using System.Reflection;

namespace MyProject.CustomEditors
{
    [CustomEditor(typeof(AIStateMachine))]
    public class AIStateMachineEditor : Editor
    {
        public AIStateMachine machine;

        private string folderPath;
        private bool folderExist;
        private int selectedCreateStateIndex = -1;

        private List<MonoScript> availableStates = new();
        private string[] availableStateNames;

        private GUIStyle titleStyle;
        private GUIStyle dedicatedStyle;

        internal protected List<CreatedStateDataPack> createdStates= new();
        internal protected List<string> createdStateNames = new();

        internal protected bool reloadTrigger = false;


        private void OnEnable()
        {
            machine = (AIStateMachine)target;
            reloadTrigger = true;
        }
        public override void OnInspectorGUI()
        {
            if (reloadTrigger) Reload();

            EditorGUI.BeginChangeCheck();

            machine.folderName = EditorGUILayout.TextField("AI Logic", machine.folderName);

            if (EditorGUI.EndChangeCheck())
            {
                reloadTrigger = true;
            }

            if (string.IsNullOrEmpty(machine.folderName))
            {
                return;
            }

            if (!folderExist)
            {
                EditorGUILayout.LabelField("Setting not found");

                EditorGUILayout.Space();

                if (GUILayout.Button("Create New Logic"))
                {
                    AssetDatabase.CreateFolder("Assets/Resources/AI Settings", machine.folderName);
                    AssetDatabase.Refresh();
                }
                return;
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("States exist:");
            EditorGUILayout.LabelField(createdStates.Count.ToString(), titleStyle);
            EditorGUILayout.EndHorizontal();

            CustomGUILayout.DrawHorizontalLine(3);

            DrawDefaultInspector();

            EditorGUILayout.Space();

            foreach (CreatedStateDataPack state in createdStates)
            {
                state?.stateEditor?.DrawInspectorForMachine();
            }

            CustomGUILayout.DrawHorizontalLine(3);

            EditorGUI.BeginChangeCheck();
            selectedCreateStateIndex = EditorGUILayout.Popup("State to Create: ", selectedCreateStateIndex, availableStateNames);

            if (EditorGUI.EndChangeCheck())
            {
                Type type = availableStates[selectedCreateStateIndex].GetClass();
                CreateState(folderPath, type);

                selectedCreateStateIndex = -1;

                reloadTrigger = true;
            }
            EditorGUILayout.Space(3);
        }
        private void Reload()
        {
            folderPath = "Assets/Resources/AI Settings/" + machine.folderName;

            folderExist = AssetDatabase.IsValidFolder(folderPath);

            if (!folderExist) return;

            CreateStyle();

            ReloadStates();

            ReloadExitNames();

            reloadTrigger = false;
        }
        internal protected void ReloadExitNames()
        {
            createdStateNames.Clear();
            foreach(CreatedStateDataPack data in createdStates)
            {
                createdStateNames.Add(data.state.stateName);
            }
        }
        private void ReloadStates()
        {
            createdStates.Clear();
            availableStates.Clear();

            string[] scriptableObjectFiles = Directory.GetFiles(folderPath)
                .Where(file => file.EndsWith(".asset"))
                .ToArray();

            List<AIState> loadedStates = scriptableObjectFiles
                .Select(filePath => AssetDatabase.LoadAssetAtPath<AIState>(filePath))
                .Where(obj => obj != null)
                .ToList();

            foreach (AIState stateObj in loadedStates)
            {
                CreatedStateDataPack data = new()
                {
                    state = stateObj,
                    stateEditor = (AIStateEditor)CreateEditor(stateObj, typeof(AIStateEditor)),
                };

                createdStates.Add(data);
            }

            foreach (CreatedStateDataPack data in createdStates)
            {
                data.stateEditor.Reload(this);
            }

            string[] guids = AssetDatabase.FindAssets("t:MonoScript", new[] { "Assets/Scripts/AI State Machine/States" });

            availableStates = guids
                .Select(guid => AssetDatabase.LoadAssetAtPath<MonoScript>(AssetDatabase.GUIDToAssetPath(guid)))
                .Where(obj => obj != null)
                .ToList();

            availableStateNames = availableStates
                .Select(script => script.name)
                .ToArray();
        }
        private void CreateState(string folderPath, Type type)
        {
            AIState newState = (AIState)CreateInstance(type);

            string assetPath = folderPath + "/" + newState.GetType().ToString() + ".asset";

            AssetDatabase.CreateAsset(newState, assetPath);
            AssetDatabase.RenameAsset(assetPath, newState.StateLogicName + " (Nameless)");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            reloadTrigger = true;
        }
        internal protected void RemoveState(AIState state)
        {
            string assetPath = AssetDatabase.GetAssetPath(state);
            AssetDatabase.DeleteAsset(assetPath);
            AssetDatabase.Refresh();

            reloadTrigger = true;
        }
        private void CreateStyle()
        {
            titleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold
            };

            dedicatedStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 12,
                fontStyle = FontStyle.Bold
            };
        }

        static internal protected int GetCurrentExitIndex(AIState state, List<CreatedStateDataPack> createdStates)
        {
            int count = createdStates.Count;
            for (int x = 0; x < count; x++)
            {
                if (createdStates[x].state == state)
                {
                    return x;
                }
            }
            return -1;
        }
        internal protected class CreatedStateDataPack
        {
            internal protected AIState state;
            internal protected AIStateEditor stateEditor;
        }
    }
}