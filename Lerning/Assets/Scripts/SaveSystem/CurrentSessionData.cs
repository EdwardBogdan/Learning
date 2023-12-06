using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaveSystem
{
    public static class CurrentSessionData
    {
        private static SceneSaveDataSet _currentSceneData;
        private static readonly Dictionary<string, SceneSaveDataSet> _dataCollection = new();

        public static string SpawnPosID { get; private set; }
        internal static CheckPointSaveData checkPoint { get; private set; }
        public static SceneSaveDataSet CurrentSceneData => _currentSceneData;

        public static void ClearSessionData()
        {
            _dataCollection.Clear();
            checkPoint = null;
        }
        internal static void LoadScene(string sceneName)
        {
            OnChangeScene?.Invoke();
            SceneManager.LoadScene(sceneName);
            InitSceneData(sceneName);
        }
        internal static void SetCheckPoint(CheckPointSaveData data) => checkPoint = data;
        internal static void ChangeSpawnPoint(string pos) => SpawnPosID = pos;
        private static void InitSceneData(string sceneName)
        {
            if (_dataCollection.TryGetValue(sceneName, out _currentSceneData))
            {
                return;
            }

            _currentSceneData = new SceneSaveDataSet();
            _dataCollection.Add(sceneName, _currentSceneData);
        }

        #region Events
        public static Action OnSave;
        public static Action OnLoad;
        public static Action OnChangeScene;
        public static void SaveData()
        {
            OnSave?.Invoke();
        }
        public static void LoadData()
        {
            OnLoad?.Invoke();
        }
        #endregion
        #region Editor
        #if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod]
        private static void InitTestScene()
        {
            var scene = SceneManager.GetActiveScene();
            InitSceneData(scene.name);
        }
        #endif
        #endregion
    }

    public class SceneSaveDataSet
    {
        private TypeSaveData<SimpleSaveData> _simpleData = new();
        private TypeSaveData<CreatureSaveData> _creatureData = new();

        public TypeSaveData<SimpleSaveData> SimpleData => _simpleData;
        public TypeSaveData<CreatureSaveData> CreatureData => _creatureData;
    }
    public class TypeSaveData<T>  where T : SimpleSaveData, new()
    {
        private Dictionary<string, T> _collection = new();
        public T CreateSaveData(string id)
        {
            T _new = new();
            _collection.Add(id, _new);
            return _new;
        }
        public bool TryGetSaveData(string id, out T value)
        {
            return _collection.TryGetValue(id, out value);
        }
    }
}