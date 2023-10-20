using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Data.Defenitions
{
    [CreateAssetMenu(menuName = "Data/Defs/InventoryItems", fileName = "ItemsDef")]
    public class InventoryItemsDef : ScriptableObject
    {
        [SerializeField] private List<ItemDef> _items;

        public ItemDef GetItemDef(string id)
        {
            foreach (var item in _items)
            {
                if (item.Id == id) return item;
            }

            Debug.Log($"Item: {id} does not exist");
            return default;
        }

        #if UNITY_EDITOR
        public List<ItemDef> ItemsForEditor => _items;

        # endif
    }

    [Serializable]
    public struct ItemDef
    {
        [SerializeField] private string _id;
        [SerializeField] private int _maxValue;
        public string Id => _id;
        public int MaxValue => _maxValue;

        public bool IsVoid => string.IsNullOrEmpty(_id);
    }
}
