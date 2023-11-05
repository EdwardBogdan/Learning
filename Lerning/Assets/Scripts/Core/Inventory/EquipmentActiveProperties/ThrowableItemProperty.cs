using UnityEngine;

namespace Core.Inventory.Items
{
    [CreateAssetMenu(menuName = "Data/Inventory/ItemPropertys/ThrowableItemProperty", fileName = "ThrowableItemProperty")]
    internal class ThrowableItemProperty : EquipmentActiveProperty
    {
        public override void ActiveProperty()
        {
            if (PlayerCore.PlayerEquipmentFunctional.ModifyItem(_equipmentId, _removeFromUse, false))
            {
                Debug.Log($"{_equipmentId} is Used");
            }
            else
            {
                Debug.Log($"{_equipmentId} not left");
            }
        }
    }
}
