using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [CreateAssetMenu(menuName = "Data/Save/StoreID", fileName = "StoreID")]
    public class StoreID : ScriptableObject
    {
        [SerializeField] private List<string> _ids;

        private static readonly string mas = "abcdefghijklmnopqrstuvwxyz1234567890";
        public string CreateID()
        {
            string id = "";
            for (int x = 0; x < 5; x++)
            {
                int index = Random.Range(0, 35);
                id += mas[index];
            }

            if (!ExistID(id))
            {
                Add(id);
                return id;
            } 

            return CreateID();
        }
        public bool ExistID(string id)
        {
            return _ids.Contains(id);
        }
        public void Add(string id)
        {
            _ids.Add(id);
        }
        public void Remove(string id)
        {
            _ids.Remove(id);
        }

        #region Singlton
        private static StoreID _instance;
        internal static StoreID I => _instance == null ? LoadSingleton() : _instance;
        private static StoreID LoadSingleton()
        {
            return _instance = Resources.Load<StoreID>("SaveData/StoreID");
        }
        #endregion
    }
}
