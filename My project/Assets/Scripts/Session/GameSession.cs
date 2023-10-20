using MyProject.Characters;
using MyProject.Data.Defenitions;
using UnityEngine;

namespace MyProject.Data
{
    public class GameSession : MonoBehaviour
    {
        public InventoryData Inventory  { get; private set; }
        public PlayerData PersonData { get; private set; }
        public GameObject Player { get; private set; }
        public static GameSession CurrentSession => _currentSession;
        private static GameSession _currentSession;
        private void Awake()
        {
            if (IsCurrentSession())
            {
                DontDestroyOnLoad(gameObject);
                _currentSession = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void LoadDataForSession()
        {
            Player = FindObjectOfType<PlayerDataLoader>()._player;
            PersonData = DataFacade.I.LastSave.LoadPlayedData;
            Inventory = DataFacade.I.LastSave.LoadInventaryData;
        }
        public void SaveDataFromSession()
        {
            DataFacade data = DataFacade.I;

            data.LastSave.SaveInventaryData(CurrentSession.Inventory);
            data.LastSave.SavePlayedData(CurrentSession.PersonData);
        }
        [ContextMenu("CheckByDebug")]
        public void CheckInventoryByDebug()
        {
            var items = Inventory.ItemsCopy;
            foreach (var item in items)
            {
                Debug.Log($"{item.ID} : {item.Value}");
            } 
        }
        private bool IsCurrentSession()
        {
            GameSession[] _sessions = FindObjectsOfType<GameSession>();
            foreach (GameSession _session in _sessions)
            {
                if (_session != this)
                {
                    _session.LoadDataForSession();
                    return false;
                } 
            }
            LoadDataForSession();
            return true;
        }
    }
}
