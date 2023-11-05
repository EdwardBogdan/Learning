using UnityEngine;

namespace Core.Inventory
{
    [CreateAssetMenu(menuName = "Data/Inventory/Equipment Data", fileName = "EquipmentData")]
    public class EquipmentData : ScriptableObject
    {
        [SerializeField] private string _keySaved = "saved";
        [SerializeField] private string _keyMax = "max";

        [SerializeField] private EquipmentClass[] _items;

        internal EquipmentClass[] Items => _items;
        internal string KeySaved => _keySaved;
        internal string KeyMax => _keyMax;

        private static EquipmentClass _choosedItem;
        private static EquipmentData _instance;
        internal static EquipmentData I => _instance == null ? LoadSingleton() : _instance;
        private static EquipmentData LoadSingleton()
        {
            return _instance = Resources.Load<EquipmentData>("InventoryData/EquipmentData");
        }

        public void ChooseItem(int index)
        {
            int count = _items.Length;

            if (count < index) return;

            index--;

            count = 0;

            foreach (var item in _items)
            {
                if (item.CurrentValue > 0)
                {
                    if (count == index)
                    {
                        _choosedItem = item;
                        return;
                    }
                    else
                    {
                        count++;
                    }
                }
            }
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

        internal bool ModifyItem(string id, int value, bool isAddOperation)
        {
            var item = Getitem(id);
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
            var item = Getitem(id);
            item.CurrentMaxValueModel.Value += value;
        }
        internal EquipmentClass Getitem(string id)
        {
            var inventory = _items;

            foreach (var item in inventory)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }


        [ContextMenu("Recreate EquipmentData Data")]
        internal void Recreate()
        {
            var defList = InventorySettings.I.EquipmentDefs.Items;

            int count = defList.Count;

            I._items = new EquipmentClass[count];

            for (int x = 0; x < count; x++)
            {
                var item = defList[x];

                EquipmentClass data = new(item);
                data.SavedValueModel.Value = 0;
                data.SavedMaxValueModel.Value = item.MaxValue;

                I._items[x] = data;
            }
        }

        [ContextMenu("Reload EquipmentData Data")]
        internal void Reload()
        {
            foreach (var item in _items)
            {
                item.Reload();
            }
        }

        [ContextMenu("Resave EquipmentData Data")]
        internal void Resave()
        {
            foreach (var item in _items)
            {
                item.Resave();
            }
        }

        private void OnValidate()
        {
            foreach (var item in _items)
            {
                item.Validate();
            }
        }

    }
}
