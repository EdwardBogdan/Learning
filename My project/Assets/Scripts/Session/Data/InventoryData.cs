using MyProject.Data.Defenitions;
using System;
using System.Collections.Generic;

namespace MyProject.Data
{ 
    [Serializable]
    public class InventoryData
    {
        private List<InventoryItemData> _inventory = new();

        public List<InventoryItemData> ItemsCopy => Clone()._inventory;

        public void Add(string id, int Value)
        {
            if (Value <= 0) return;

            var itemDef = DataFacade.I.ItemDefs.GetItemDef(id);
            if (itemDef.IsVoid) return;

            var item = GetItem(id);

            if (item == null)
            {
                item = new InventoryItemData(id);
                
                _inventory.Add(item);
            }

            item.Value += Value;
        }

        public void Remove(string id, int Value)
        {
            if (Value <= 0) return;

            var itemDef = DataFacade.I.ItemDefs.GetItemDef(id);
            if (itemDef.IsVoid) return;

            var item = GetItem(id);

            if (item == null) return;

            item.Value -= Value;

            if (item.Value <= 0)
            {
                _inventory.Remove(item);
            }
        }
        public int GetCount(string id)
        {
            var itemDef = DataFacade.I.ItemDefs.GetItemDef(id);
            if (itemDef.IsVoid) return 0;

            var item = GetItem(id);

            if (item == null)
            {
                return 0;
            }
            return item.Value;
        }
        public int GetMaxCount(string id)
        {
            var itemDef = DataFacade.I.ItemDefs.GetItemDef(id);
            if (itemDef.IsVoid) return 0;

            var item = GetItem(id);

            if (item == null)
            {
                return 0;
            }
            return item.MaxValue;
        }
        private InventoryItemData GetItem(string id)
        {
            foreach (var item in _inventory)
            {
                if (item.ID == id)
                {
                    return item;
                }
            }
            return null;
        }
        public InventoryData Clone()
        {
            var list = new List<InventoryItemData>();
            foreach (var item in _inventory)
            {
                list.Add(new InventoryItemData(item));
            }
            var data = new InventoryData();
            data._inventory = list;
            return data;
        }
    }

    public class InventoryItemData
    {
        public string ID;
        public int Value;
        public int MaxValue;

        public InventoryItemData(string id)
        {
            ID = id;
            //Value = value;
            //MaxValue = maxValue;
        }
        public InventoryItemData(InventoryItemData data)
        {
            ID = data.ID;
            Value = data.Value;
            MaxValue = data.MaxValue;
        }
    }
}
