using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AIStateSystem
{
    public class AILogicCore : ScriptableObject
    {
        [SerializeField] internal List<AIStateData> list = new();

#if UNITY_EDITOR
        public List<AIStateData> List => list;
        public void AddModule(AIState state)
        {
            AIStateData data = new(state);

            list.Add(data);
        }
        public void RemoveModule(AIState state)
        {
            foreach (var item in list)
            {
                if (item.State == state)
                {
                    list.Remove(item);
                    break;
                }
            }
        }
        public void AwakeStates()
        {
            foreach (AIStateData data in list)
            {
                if (data.OwnEditor == null)
                {
                    data.CreateModuleEditor(data.State);
                }
            }
        }
#endif
    }

    [Serializable]
    public class AIStateData
    {
        [SerializeField] private AIState _state;
        private Editor _editor;

#if UNITY_EDITOR
        public AIState State => _state;
        public Editor OwnEditor => _editor;
#endif
        internal AIStateData(AIState state)
        {
            _state = state;
            CreateModuleEditor(state);
        }
        internal void CreateModuleEditor(AIState module)
        {
            _editor = Editor.CreateEditor(module);
        }
    }
}

