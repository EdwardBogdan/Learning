using UnityEditor;
using UnityEngine;
using System.Linq;
using System.IO;
using System;
using CustomUnityUtils.GUI;


namespace PhysicModuleSystem
{
    [CustomEditor(typeof(PhysicModuleController))]
    public class PhysicModuleControllerEditor : Editor
    {
        private string _coreName;

        private int _currentCoreIndex;
        private int _currentModuleIndex;

        private Action OnDraw;

        private PhysicModuleController _controller;

        private MonoScript[] _availableModules;

        private static readonly string _pathCore = "PhysicModuleSystem/Core";
        private static readonly string _pathModule = "PhysicModuleSystem/Module";
        private static readonly string _pathScriptModules = "Assets/Scripts/PhysicModuleSystem/Modules";

        private static PhysicModuleLogicCore[] _logicMas;

        private static string[] _coreNames;

        private static string[] _moduleNames;

        private PhysicModuleLogicCore CurrentCore => _controller.Core;

        public override void OnInspectorGUI()
        {
            OnDraw?.Invoke();
        }

        private void DrawChooseOrCreateCore()
        {
            EditorGUILayout.LabelField("Create new core");

            EditorGUILayout.BeginHorizontal();

            _coreName = EditorGUILayout.TextField(_coreName);


            if (GUILayout.Button("Create Core", GUILayout.Width(120)))
            {
                if (_coreName == null || _coreName == string.Empty)
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

                PhysicModuleLogicCore core = (PhysicModuleLogicCore)CreateInstance(typeof(PhysicModuleLogicCore));
                string fullPath = "Assets/Resources/"+  _pathCore + "/" + _coreName + ".asset";
                AssetDatabase.CreateAsset(core, fullPath);
                AssetDatabase.SaveAssets();

                core = Resources.Load<PhysicModuleLogicCore>(_pathCore + "/" + _coreName);
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

            DrawDefaultInspector();

            CustomGUILayout.DrawHorizontalLine(3);

            ModuleProperties();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Install module");

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            _currentModuleIndex = EditorGUILayout.Popup(_currentModuleIndex, _moduleNames);

            if (GUILayout.Button("Install"))
            {
                InstallModule();
            }

            EditorGUILayout.EndHorizontal();
        }
        private void ModuleProperties()
        {
            var mods = CurrentCore.List;
            foreach (var mod in mods)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("PM: " + mod.Module.ModuleName);

                if (GUILayout.Button("Remove"))
                {
                    RemoveModule(mod.Module);
                    return;
                }

                EditorGUILayout.EndHorizontal();
                mod.OwnEditor.DrawDefaultInspector();
                EditorGUILayout.Space(2);
                CustomGUILayout.DrawHorizontalLine(2);
            }
        }
        private void ReloadLogicMas()
        {
            _logicMas = Resources.LoadAll<PhysicModuleLogicCore>(_pathCore);

            int count = _logicMas.Length;

            _coreNames = new string[count];

            for (int x = 0; x < count; x++)
            {
                _coreNames[x] = _logicMas[x].name;
            }

            _coreName = null;
        }
        private void LoadModules()
        {
            string[] guids = AssetDatabase.FindAssets("t:MonoScript", new[] { _pathScriptModules });

            int count = guids.Length;

            _availableModules = new MonoScript[count];

            _moduleNames = new string[count];

            for (int x = 0; x < count; x++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[x]);

                MonoScript obj = (MonoScript)AssetDatabase.LoadAssetAtPath(path, typeof(MonoScript));

                if (obj != null)
                {
                    _availableModules[x] = obj;
                    _moduleNames[x] = _availableModules[x].name;
                }
                
            }
        }
        private void InstallModule()
        {
            Type type = _availableModules[_currentModuleIndex].GetClass();

            PhysicModule module = (PhysicModule)CreateInstance(type);

            string fullPath = "Assets/Resources/" + _pathModule + "/" + CurrentCore.name + " - " + module.ModuleName + ".asset";

            var asset = AssetDatabase.LoadAssetAtPath(fullPath, typeof(UnityEngine.Object));

            if (asset != null)
            {
                Debug.Log("Module is exist");
                DestroyImmediate(module);
                return;
            }

            AssetDatabase.CreateAsset(module, fullPath);
            
            CurrentCore.AddModule(module);

            EditorUtility.SetDirty(CurrentCore);

            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();
        }
        private void RemoveModule(PhysicModule module)
        {
            CurrentCore.RemoveModule(module);

            string assetPath = AssetDatabase.GetAssetPath(module);

            AssetDatabase.DeleteAsset(assetPath);

            EditorUtility.SetDirty(CurrentCore);

            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();
        }

        private void StartDrawCore()
        {
            OnDraw = DrawCore;
            LoadModules();
            CurrentCore.AwakeModules();
        }
        private void StartDrawChoose()
        {
            ReloadLogicMas();
            OnDraw = DrawChooseOrCreateCore;
        }


        private void OnEnable()
        {
            _controller = (PhysicModuleController)target;

            string resourcesFolder = Application.dataPath + "/Resources";

            string fullPath = resourcesFolder + "/" + _pathCore;

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            fullPath = resourcesFolder + "/" + _pathModule;

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
    }
}
