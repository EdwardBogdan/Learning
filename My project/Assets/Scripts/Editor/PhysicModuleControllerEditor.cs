using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using MyProject.Physic.Modules;
using System.Linq;
using System.IO;
using System;
using MyProject.Utils.GUI;
using Unity.VisualScripting;
using System.Runtime.InteropServices.ComTypes;

[CustomEditor(typeof(PhysicModuleController))]
public class PhysicModuleControllerEditor : Editor
{
    private bool folderExist;

    private string folderPath;

    private List<MonoScript> availableModules = new();

    private List<PhysicModule> modules = new();

    private string[] availableModuleNames;

    private int selectedModuleIndex = -1;

    ModuleData[] datas;

    private GUIStyle titleStyle;
    private GUIStyle dedicatedStyle;

    private PhysicModuleController controller;

    public override void OnInspectorGUI()
    {
        controller = (PhysicModuleController)target;

        if (datas == null) Reload();

        EditorGUI.BeginChangeCheck();
        controller.folderName = EditorGUILayout.TextField("Settings Name", controller.folderName);

        if (EditorGUI.EndChangeCheck())
        {
            Reload();
        }

        if (string.IsNullOrEmpty(controller.folderName))
        {
            return;
        }

        if (!folderExist)
        {
            EditorGUILayout.LabelField("Setting not found");

            if (GUILayout.Button("Create Moduls Storage"))
            {
                AssetDatabase.CreateFolder("Assets/Resources/Physic Moduls", controller.folderName);
                AssetDatabase.Refresh();
            }
            return;
        }

        CreateStyle();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Moduls instaled:");
        EditorGUILayout.LabelField(modules.Count.ToString(), titleStyle);
        EditorGUILayout.EndHorizontal();

        CustomGUILayout.DrawHorizontalLine();

        if (GUILayout.Button("Refresh Values On THIS Object (Only For RunTime)"))
        {
            controller.ActivateCurrentModuls();
        }

        CustomGUILayout.DrawHorizontalLine(3);

        DrawDefaultInspector();

        foreach (ModuleData module in datas)
        {
            DrawModule(module);
        }

        CustomGUILayout.DrawHorizontalLine(3);

        EditorGUI.BeginChangeCheck();
        selectedModuleIndex = EditorGUILayout.Popup("PM to Create: ", selectedModuleIndex, availableModuleNames);

        if (EditorGUI.EndChangeCheck())
        {
            Type type = availableModules[selectedModuleIndex].GetClass();
            InstallModule(folderPath, type);
            Reload();

            selectedModuleIndex = -1;
        }
    }

    private void LoadModules()
    {
        string[] scriptableObjectFiles = Directory.GetFiles(folderPath)
            .Where(file => file.EndsWith(".asset"))
            .ToArray();

        Type assetType = typeof(PhysicModule);

        modules.Clear();
        availableModules.Clear();

        foreach (string filePath in scriptableObjectFiles)
        {
            PhysicModule obj = (PhysicModule)AssetDatabase.LoadAssetAtPath(filePath, assetType);
            if (obj != null)
            {
                modules.Add(obj);
            }
        }

        string[] guids = AssetDatabase.FindAssets("t:MonoScript", new[] { "Assets/Scripts/PhysicModuleController/PhysicModules" });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            MonoScript obj = (MonoScript)AssetDatabase.LoadAssetAtPath(path, typeof(MonoScript));
            if (obj != null)
            {
                availableModules.Add(obj);
            }
        }

        availableModuleNames = new string[availableModules.Count];
        for (int i = 0; i < availableModules.Count; i++)
        {
            availableModuleNames[i] = availableModules[i].name;
        }
    }
    private void DrawModule(ModuleData data)
    {
        CustomGUILayout.DrawHorizontalLine(3);

        EditorGUILayout.BeginHorizontal();

        data.foldout = EditorGUILayout.BeginFoldoutHeaderGroup(data.foldout, $"Module: {data.module.ModuleName}");

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Remove Module", GUILayout.Width(120)))
        {
            string assetPath = AssetDatabase.GetAssetPath(data.module);
            AssetDatabase.DeleteAsset(assetPath);
            AssetDatabase.Refresh();
            Reload();
            return;
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(2);

        if (!data.foldout)
        {
            EditorGUILayout.EndFoldoutHeaderGroup();
            return;
        }

        PhysicModule module = data.module;

        Editor editor = CreateEditor(module);
        editor.DrawDefaultInspector();

        EditorGUILayout.Space();
    }
    private void CreateModuleDatas()
    {
        datas = new ModuleData[modules.Count()];

        for (int stateIndex = 0; stateIndex < modules.Count; stateIndex++)
        {
            datas[stateIndex] = new()
            {
                module = modules[stateIndex],
            };
        }
    }
    private void InstallModule(string folderPath, Type type)
    {
        string[] scriptableObjectFiles = Directory.GetFiles(folderPath)
            .Where(file => file.EndsWith(".asset"))
            .ToArray();

        foreach (string filePath in scriptableObjectFiles)
        {
            PhysicModule obj = (PhysicModule)AssetDatabase.LoadAssetAtPath(filePath, type);

            if (obj != null)
            {
                Debug.Log("Module is already installed");
                return;
            }
        }

        PhysicModule module = (PhysicModule)CreateInstance(type);
        AssetDatabase.CreateAsset(module, folderPath + "/" + module.GetType().ToString() + ".asset");
        AssetDatabase.SaveAssets();
    }
    private void CreateStyle()
    {
        titleStyle = new GUIStyle(GUI.skin.label);
        titleStyle.fontSize = 14;
        titleStyle.fontStyle = FontStyle.Bold;

        dedicatedStyle = new GUIStyle(GUI.skin.label);
        dedicatedStyle.fontSize = 12;
        dedicatedStyle.fontStyle = FontStyle.Bold;
    }
    private void Reload()
    {
        folderPath = "Assets/Resources/Physic Moduls/" + controller.folderName;

        folderExist = AssetDatabase.IsValidFolder(folderPath);

        if (!folderExist) return;

        CreateStyle();

        LoadModules();

        CreateModuleDatas();
    }

    class ModuleData
    {
        public PhysicModule module;

        public bool foldout = false;
    }
}