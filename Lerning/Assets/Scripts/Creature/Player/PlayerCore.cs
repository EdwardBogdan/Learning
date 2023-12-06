using AnimatorController;
using Core.Characters;
using Inventory;
using SpawnEquipment;
using UnityEngine;

namespace PlayerController
{
    public static class PlayerCore 
    {
        public static GameObject Player;
        public static CreatureAnimatorController PlayerAnimator { get; set; }
        public static SpawnManager SpawnManager { get; set; }
        public static PlayerCharacteristics PlayerCharacteristics => PlayerCharacteristics.I;
        public static EquipmentData Equipment => EquipmentData.I;
        public static TreasureData Treasure => TreasureData.I;
        public static InventoryDefs ItemDefs => InventoryDefs.I;

    }
}
