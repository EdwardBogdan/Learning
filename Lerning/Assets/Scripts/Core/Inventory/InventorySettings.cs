using UnityEngine;

namespace Core.Inventory
{
    [CreateAssetMenu(menuName = "Data/Settings/Inventory Settings", fileName = "InventorySettings")]
    public class InventorySettings : ScriptableObject
    {
        [SerializeField] private EquipmentDefs equipmentDefs;
        [SerializeField] private TreasureDefs treasureDefs;
        internal EquipmentDefs EquipmentDefs => equipmentDefs;
        internal TreasureDefs TreasureDefs => treasureDefs;

        private static InventorySettings _instance;
        internal static InventorySettings I => _instance == null ? LoadSingleton() : _instance;

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
            var items = equipmentDefs.Items;
            int count = items.Count;

            string[] ids = new string[count];

            for (int x = 0; x < count; x++)
            {
                ids[x] = items[x].Id;
            }
            return ids;
        }

        private static InventorySettings LoadSingleton()
        {
            return _instance = Resources.Load<InventorySettings>("Settings/InventorySettings");
        }
    }
}
