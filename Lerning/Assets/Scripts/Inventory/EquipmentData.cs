using System;
using UnityEngine;
using System.Collections.Generic;

namespace Inventory
{
    [CreateAssetMenu(menuName = "Data/Inventory/Equipment Data", fileName = "Data_Equipment")]
    public class EquipmentData : ScriptableObject
    {
        [SerializeField] private EquipmentClass[] _itemsData;
        [SerializeField] private List<EquipmentClass> _currentItems = new();

        private EquipmentClass _choosedItem;
        public EquipmentClass ChoosedItem => _choosedItem;
        public List<EquipmentClass> CurrentItems => _currentItems;

        public delegate void OnChanged(EquipmentClass item);

        public event OnChanged OnChoosed;
        public event OnChanged OnRemoved;

        public event Action ItemSpecicalAbylity;
        public event Action OnResorted;

        
        public void ItemSpecialAbylityInvoke()
        {
            ItemSpecicalAbylity?.Invoke();
        }
        public void UseChoosedItem()
        {
            if (_choosedItem == null)
            {
                Debug.Log("item == null");
                return;
            }
            _choosedItem.Property.ActiveProperty();
        }
        public bool TryGetItemData(string name, out EquipmentClass item)
        { 
            foreach(EquipmentClass equipmentObj in _itemsData)
            {
                if (equipmentObj.Id == name)
                {
                    item = equipmentObj;
                    return true;
                }
            }
            Debug.Log($"{name}: not exist");
            item = null;
            return false;
        }

        #region Inventory Modification
        public void ChooseItem(int index)
        {
            int count = _currentItems.Count;

            if (count == 0)
            {
                OnRemoved?.Invoke(_choosedItem);
                _choosedItem = null;
                return;
            }

            if (count < index + 1) return;

            count = 0;

            EquipmentClass item = _currentItems[index];

            if (item == _choosedItem) return;

            OnRemoved?.Invoke(_choosedItem);

            _choosedItem = item;

            item.Property.OnSelected();

            OnChoosed?.Invoke(item);
        }
        internal void EquipItem(EquipmentClass item, bool doEquip)
        {
            if (doEquip)
            {
                if (!_currentItems.Contains(item))
                {
                    _currentItems.Add(item);

                    _currentItems.Sort((a, b) =>
                        Array.IndexOf(InventoryDefs.I.GetEquipmentIds(), a.Id)
                        .CompareTo(Array.IndexOf(InventoryDefs.I.GetEquipmentIds(), b.Id)));

                    OnResorted?.Invoke();
                }
            }
            else
            {
                _currentItems.RemoveAll(existingItem => existingItem == item);
                OnResorted?.Invoke();
            }
        }
        internal bool ModifyItem(string id, int value, bool isAddOperation)
        {
            if (!TryGetItemData(id, out var item)) return false;

            int currentValue = item.CurrentValueModel.Value;

            if (isAddOperation)
            {
                int maxValue = item.CurrentMaxValueModel.Value;

                int newCount = currentValue + value;

                if (newCount > maxValue)
                {
                    return false;
                }

                item.CurrentValueModel.Value = newCount;
            }
            else
            {
                if (currentValue < value)
                {
                    return false;
                }

                item.CurrentValueModel.Value -= value;
            }

            return true;
        }
        internal void AddItemMax(string id, int value)
        {
            if (!TryGetItemData(id, out var item)) return;

            item.CurrentMaxValueModel.Value += value;
        }
        private void OnAutoRechooseItem(int newValue, int oldValue)
        {
            if (newValue <= 0 || oldValue <= 0)
            {
                ChooseItem(0);
            }
        }
        #endregion

        #region Inventory Global Functional

        [ContextMenu("Recreate Equipment Data")]
        internal void Recreate()
        {
            var defList = InventoryDefs.I.EquipmentDefs.Items;

            _currentItems.Clear();

            int count = defList.Count;

            _itemsData = new EquipmentClass[count];

            for (int x = 0; x < count; x++)
            {
                var item = defList[x];

                EquipmentClass data = new EquipmentClass(item);
                data.SavedValueModel.Value = 0;
                data.SavedMaxValueModel.Value = item.MaxValue;

                _itemsData[x] = data;
            }
        }
        [ContextMenu("Reload Equipment Data")]
        internal void Reload()
        {
            var defList = InventoryDefs.I.EquipmentDefs.Items;

            int count = defList.Count;

            _itemsData = new EquipmentClass[count];

            _currentItems.Clear();

            for (int x = 0; x < count; x++)
            {
                var item = defList[x];

                EquipmentClass data = new(item);

                _itemsData[x] = data;

                data.SubscribeCurrentValue(OnAutoRechooseItem, true);

                if (data.CurrentValue > 0) EquipItem(data, true);
            }

            ChooseItem(0);
        }

        [ContextMenu("Resave Equipment Data")]
        internal void Resave()
        {
            foreach (var item in _itemsData)
            {
                item.Resave();
            }
        }
#if UNITY_EDITOR
        public EquipmentClass[] ItemList => _itemsData;
        public void EditRecreate()
        {
            Recreate();
        }
        public void EditReload()
        {
            Reload();
        }
        public void EditResave()
        {
            Resave();
        }
#endif
        #endregion

        #region Singlton
        private static EquipmentData _instance;
        internal static EquipmentData I => _instance == null ? LoadSingleton() : _instance;
        private static EquipmentData LoadSingleton()
        {
            return _instance = Resources.Load<EquipmentData>("InventoryData/Data_Equipment");
        }
        #endregion

        private void OnValidate()
        {
            foreach (var item in _itemsData)
            {
                item.Validate();
            }
            foreach (var item in _currentItems)
            {
                item.Validate();
            }
        }
    }
}
