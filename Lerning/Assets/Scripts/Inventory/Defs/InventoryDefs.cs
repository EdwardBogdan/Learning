using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(menuName = "Data/Settings/Inventory Defs", fileName = "InventoryDefs")]
    public class InventoryDefs : ScriptableObject
    {
        [SerializeField] private EquipmentDefs equipmentDefs;
        [SerializeField] private TreasureDefs treasureDefs;
        internal EquipmentDefs EquipmentDefs => equipmentDefs;
        internal TreasureDefs TreasureDefs => treasureDefs;
      
        public Sprite GetItemSprite(string Id)
        {
            foreach (var item in equipmentDefs.Items)
            {
                if (item.Id == Id) return item.Sprite;
            }
            return default;
        }
        public string[] GetEquipmentIds()
        {
            var items = equipmentDefs.Items;
            int count = items.Count;

            string[] ids = new string[count];

            for (int x = 0; x < count; x++)
            {
                ids[x] = items[x].Id;
            }
            return ids;
        }
        public string[] GetTreasureIds()
        {
            var items = treasureDefs.Treasures;
            int count = items.Count;

            string[] ids = new string[count];

            for (int x = 0; x < count; x++)
            {
                ids[x] = items[x].Id;
            }
            return ids;
        }
        public int GetEquipmentCount()
        {
            return equipmentDefs.Items.Count;
        }
        public int GetTreasureCount()
        {
            return treasureDefs.Treasures.Count;
        }

        #region Singleton
        private static InventoryDefs _instance;
        public static InventoryDefs I => _instance == null ? LoadSingleton() : _instance;
        private static InventoryDefs LoadSingleton()
        {
            return _instance = Resources.Load<InventoryDefs>("Settings/InventoryDefs");
        }
        #endregion
    }
}
