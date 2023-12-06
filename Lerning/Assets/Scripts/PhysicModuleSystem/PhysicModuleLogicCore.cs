using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System;

namespace PhysicModuleSystem
{
    public class PhysicModuleLogicCore : ScriptableObject
    {
        [SerializeField] internal List<ModuleData> list = new();

#if UNITY_EDITOR
        public List<ModuleData> List => list;
        public void AddModule(PhysicModule module)
        {
            ModuleData data = new(module);

            list.Add(data);
        }
        public void RemoveModule(PhysicModule module)
        {
            foreach (var item in list)
            {
                if (item.Module == module)
                {
                    list.Remove(item);
                    break;
                }
            }
        }
        public void AwakeModules()
        {
            foreach (ModuleData data in list)
            {
                if (data.OwnEditor == null)
                {
                    data.CreateModuleEditor(data.Module);
                }
            }
        }
#endif
    }

    [Serializable]
    public class ModuleData
    {
        [SerializeField] private PhysicModule _module;
        private Editor _editor;

#if UNITY_EDITOR
        public PhysicModule Module => _module;
        public Editor OwnEditor => _editor;
#endif
        internal ModuleData(PhysicModule module)
        {
            _module = module;
            CreateModuleEditor(module);
        }
        internal void CreateModuleEditor(PhysicModule module)
        { 
            _editor = Editor.CreateEditor(module);
        }
    }
}
