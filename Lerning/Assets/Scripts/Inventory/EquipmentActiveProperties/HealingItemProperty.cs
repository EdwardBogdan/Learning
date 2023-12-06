using UnityEngine;

namespace Inventory.ItemProperty
{
    [CreateAssetMenu(menuName = "Data/Inventory/ItemPropertys/HealingItemProperty", fileName = "HealingItemProperty")]
    internal class HealingItemProperty : EquipmentActiveProperty
    {
        internal override void ActiveProperty()
        {
            bool isPossible = EquipmentData.I.ModifyItem(_equipmentId, _removeFromUse, false);

            if (isPossible)
            {
                Debug.Log($"{_equipmentId} is used");
            }
            else
            {
                Debug.Log($"{_equipmentId} is 0");
            }
        }
    }
}
