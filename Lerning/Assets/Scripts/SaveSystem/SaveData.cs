using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaveSystem
{
    public class SimpleSaveData : IDisposable
    {
        public bool isDead;
        protected bool saveIsDead;
        protected virtual void LoadData()
        {
            saveIsDead = isDead;
        }
        protected virtual void SaveData()
        {
            saveIsDead = isDead;
        }
        public SimpleSaveData()
        {
            CurrentSessionData.OnSave += SaveData;
            CurrentSessionData.OnLoad += LoadData;
        }
        public void Dispose()
        {
            CurrentSessionData.OnSave -= SaveData;
            CurrentSessionData.OnLoad -= LoadData;
        }
    }

    public class CreatureSaveData : SimpleSaveData
    {
        
        public Vector3 pos;
        public int hp;

        private int saveHP;
        private Vector3 savePos;

        protected override void SaveData()
        {
            base.SaveData();
            saveHP = hp;
            savePos = pos;
        }
        protected override void LoadData()
        {
            base.LoadData();
            hp = saveHP;
            pos = savePos;
        }
    }
    public class CheckPointSaveData : SimpleSaveData
    {
        public string SceneName { get; private set; }
        public string SpawnPosID { get; private set; }

        public CheckPointSaveData (string ID)
        {
            SceneName = SceneManager.GetActiveScene().name;
            SpawnPosID = ID;
        }
        protected override void SaveData()
        {
            base.SaveData();
        }
        protected override void LoadData()
        {
            base.LoadData();
        }
    }
}
