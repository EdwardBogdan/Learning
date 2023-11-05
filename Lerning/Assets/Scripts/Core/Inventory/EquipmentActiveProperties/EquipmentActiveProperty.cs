using Core.Inventory.Attribute;
using UnityEngine;

namespace Core.Inventory.Items
{
    internal abstract class EquipmentActiveProperty : ScriptableObject
    {
        [SerializeField][Items]
        protected string _equipmentId;
        [SerializeField] protected int _removeFromUse = 1;

        public abstract void ActiveProperty();
    }
}
