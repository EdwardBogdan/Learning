using Inventory.Attribute;
using UnityEngine;

namespace Inventory.ItemProperty
{
    internal abstract class EquipmentActiveProperty : ScriptableObject
    {
        [SerializeField][Items]
        protected string _equipmentId;
        [SerializeField] protected int _removeFromUse = 1;

        internal virtual void OnSelected() { }
        internal abstract void ActiveProperty();
    }
}
