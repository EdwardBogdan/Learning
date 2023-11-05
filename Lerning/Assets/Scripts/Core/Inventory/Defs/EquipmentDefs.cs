using Core.Inventory.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Inventory
{
    [CreateAssetMenu(menuName = "Data/Inventory/Equipment Defs", fileName = "EquipmentDefs")]
    internal class EquipmentDefs : ScriptableObject
    {
        [SerializeField] private List<ItemDef> _items;
        internal List<ItemDef> Items => _items;
    }

    [Serializable]
    internal struct ItemDef
    {
        [SerializeField] private string _id;
        [SerializeField] private int _maxValue;

        [SerializeField] private Sprite _sprite;
        [SerializeField] private EquipmentActiveProperty _property;
        internal string Id => _id;
        internal int MaxValue => _maxValue;
        internal Sprite Sprite => _sprite;
        internal EquipmentActiveProperty Property => _property;

        internal bool IsVoid => string.IsNullOrEmpty(_id); //Do I need it?

    }
}
