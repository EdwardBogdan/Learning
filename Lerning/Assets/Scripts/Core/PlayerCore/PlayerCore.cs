using Core.Inventory;
using Core.Characters;
using UnityEngine;

namespace Core
{
    public static class PlayerCore 
    {
        public static GameObject Player;

        public static EquipmentData PlayerEquipmentFunctional => EquipmentData.I;
        public static EquipmentClass[] PlayerItemsData => EquipmentData.I.Items;
        public static TreasuryData PlayerTreasuryFunctional => TreasuryData.I;
        public static TreasureClass[] PlayerTreasuryData => TreasuryData.I.Treasures;
        public static InventorySettings InventorySettings => InventorySettings.I;

        public static PlayerCharacteristics PlayerCharacteristics => PlayerCharacteristics.I;
    }
}
