using UnityEngine;

namespace MyProject.Data.Defenitions
{
    [CreateAssetMenu(menuName = "Data/SaveData", fileName = "SaveData")]
    public class SaveData : ScriptableObject
    {
        private PlayerData _playerData = new();
        private InventoryData _inventoryData = new();

        public PlayerData LoadPlayedData => _playerData.Clone();
        public InventoryData LoadInventaryData => _inventoryData.Clone();

        public void SavePlayedData(PlayerData data)
        {
            _playerData = data;
        }
        public void SaveInventaryData(InventoryData data)
        {
            _inventoryData = data;
        }
    }
}