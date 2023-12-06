using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.IO;
using System;
using CustomUnityUtils.GUI;

namespace AIStateSystem.Editors
{
    [CustomEditor(typeof(AIStateMachine))]
    public class AIStateMachineEditor : Editor
    {
        private string _coreName;

        private int _currentCoreIndex;
        private int _currentModuleIndex;

        private Action OnDraw;

        private AIStateMachine _controller;

        private static AILogicCore[] _logicMas;
        private static MonoScript[] _availableStates;
        private AILogicCore CurrentCore => _controller.Core;

        private static int _dublicateCount;

        private static string[] _coreNames;
        private static string[] _stateNames;

        private static readonly string _pathCore = "AIStateSystem/Core";
        private static readonly string _pathState = "AIStateSystem/States";
        private static readonly string _pathScriptStates = "Assets/Scripts/AIStateSystem/States";

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            OnDraw.Invoke();

            serializedObject.ApplyModifiedProperties();
        }

        private void StartDrawCore()
        {
            OnDraw = DrawCore;
            LoadStates();
            CurrentCore.AwakeStates();
            ReloadAttributes();
        }
        private void StartDrawChoose()
        {
            ReloadLogicMas();
            OnDraw = DrawChooseOrCreateCore;
        }

        private void DrawCore()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Core: " + CurrentCore.name);

            if (GUILayout.Button("Change Core"))
            {
                _controller.Core = null;
                EditorUtility.SetDirty(_controller);
                StartDrawChoose();
                return;
            }

            EditorGUILayout.EndHorizontal();

            CustomGUILayout.DrawHorizontalLine(3);

            EditorGUI.BeginChangeCheck();

            DrawDefaultInspector();

            if (EditorGUI.EndChangeCheck())
            {
                ReloadAttributes();
            }

            CustomGUILayout.DrawHorizontalLine(3);

            DrawProperties();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Install module");

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            _currentModuleIndex = EditorGUILayout.Popup(_currentModuleIndex, _stateNames);

            if (GUILayout.Button("Install"))
            {
                InstallModule();
            }

            EditorGUILayout.EndHorizontal();

            EditorUtility.SetDirty(_controller);
        }
        private void DrawChooseOrCreateCore()
        {
            EditorGUILayout.LabelField("Create new core");
            EditorGUILayout.BeginHorizontal();

            _coreName = EditorGUILayout.TextField(_coreName);


            if (GUILayout.Button("Create Core", GUILayout.Width(120)))
            {
                if (string.IsNullOrEmpty(_coreName))
                {
                    Debug.Log("Name is empty");
                    return;
                }

                bool exists = _coreNames.Contains(_coreName);
                if (exists)
                {
                    Debug.Log("Logic Core with this name is exists");
                    return;
                }

                AILogicCore core = (AILogicCore)CreateInstance(typeof(AILogicCore));
                string fullPath = "Assets/Resources/" + _pathCore + "/" + _coreName + ".asset";
                AssetDatabase.CreateAsset(core, fullPath);
                AssetDatabase.SaveAssets();

                core = Resources.Load<AILogicCore>(_pathCore + "/" + _coreName);
                _controller.Core = core;

                StartDrawCore();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Or select an existing one");

            EditorGUILayout.BeginHorizontal();

            _currentCoreIndex = EditorGUILayout.Popup(_currentCoreIndex, _coreNames);

            if (GUILayout.Button("Choose Core", GUILayout.Width(120)))
            {
                var Core = _logicMas[_currentCoreIndex];

                _controller.Core = Core;

                EditorUtility.SetDirty(_controller);

                StartDrawCore();
            }

            EditorGUILayout.EndHorizontal();
        }
        private void ReloadLogicMas()
        {
            _logicMas = Resources.LoadAll<AILogicCore>(_pathCore);

            int count = _logicMas.Length;

            _coreNames = new string[count];

            for (int x = 0; x < count; x++)
            {
                _coreNames[x] = _logicMas[x].name;
            }

            _coreName = null;
        }
        private void LoadStates()
        {
            string[] guids = AssetDatabase.FindAssets("t:MonoScript", new[] { _pathScriptStates });

            int count = guids.Length;

            _availableStates = new MonoScript[count];

            _stateNames = new string[count];

            for (int x = 0; x < count; x++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[x]);

                MonoScript obj = (MonoScript)AssetDatabase.LoadAssetAtPath(path, typeof(MonoScript));

                if (obj != null)
                {
                    _availableStates[x] = obj;
                    _stateNames[x] = _availableStates[x].name;
                }

            }
        }
        private void DrawProperties()
        {
            var mods = CurrentCore.List;
            foreach (var mod in mods)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUI.BeginChangeCheck();

                mod.State.stateName = EditorGUILayout.TextField(mod.State.stateName);

                if (EditorGUI.EndChangeCheck())
                {
                    ReloadAttributes();
                }

                if (GUILayout.Button("Remove"))
                {
                    RemoveModule(mod.State);
                    return;
                }

                EditorGUILayout.EndHorizontal();
                mod.OwnEditor.DrawDefaultInspector();
                EditorGUILayout.Space(2);
                CustomGUILayout.DrawHorizontalLine(2);
            }
        }
        private void InstallModule()
        {
            Type type = _availableStates[_currentModuleIndex].GetClass();

            AIState module = (AIState)CreateInstance(type);

            string fullPath = "Assets/Resources/" + _pathState + "/" + CurrentCore.name + " - " + module.StateLogicName + " " + _dublicateCount + ".asset";

            var asset = AssetDatabase.LoadAssetAtPath(fullPath, typeof(UnityEngine.Object));

            if (asset != null)
            {
                _dublicateCount++;
                InstallModule();
                return;
            }

            _dublicateCount = 0;

            AssetDatabase.CreateAsset(module, fullPath);

            module.stateName = module.StateLogicName;

            CurrentCore.AddModule(module);

            EditorUtility.SetDirty(CurrentCore);

            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();

            ReloadAttributes();
        }
        private void RemoveModule(AIState module)
        {
            CurrentCore.RemoveModule(module);

            string assetPath = AssetDatabase.GetAssetPath(module);

            AssetDatabase.DeleteAsset(assetPath);

            EditorUtility.SetDirty(CurrentCore);

            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();

            ReloadAttributes();
        }

        private void ReloadAttributes()
        {
            AIStateChooseAttributeDrawer.core = CurrentCore;
            AIStateChooseAttributeDrawer.names = ReloadExitNames();

            AIAreaChooseAttributeEditor.core = _controller;
            AIAreaChooseAttributeEditor.names = _controller.GetAreaNames();

            AIComponentRefAttributeDrawer.core = _controller;
            AIComponentRefAttributeDrawer.names = _controller.GetComponentRefNames();

        }
        private string[] ReloadExitNames()
        {
            List<string> createdStateNames = new();
            foreach (var item in CurrentCore.List)
            {
                createdStateNames.Add(item.State.stateName);
            }

            return createdStateNames.ToArray();
        }

        private void OnEnable()
        {
            _controller = (AIStateMachine)target;

            string resourcesFolder = Application.dataPath + "/Resources";

            string fullPath = resourcesFolder + "/" + _pathCore;

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            fullPath = resourcesFolder + "/" + _pathState;

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            if (CurrentCore != null)
            {
                StartDrawCore();
            }
            else
            {
                StartDrawChoose();
            }
        }
        private void OnDisable()
        {
            AIStateChooseAttributeDrawer.core = null;

            AIAreaChooseAttributeEditor.core = null;

            AIComponentRefAttributeDrawer.core = null;
        }
    }
}