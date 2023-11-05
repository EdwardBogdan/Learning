using Core.Data.ObservableProperty;
using Core.Data.PersistentProperty;
using Core.Inventory.Items;
using UnityEngine;
using System;

namespace Core.Inventory
{
    [Serializable]
    public class EquipmentClass
    {
        [SerializeField] private string _id;

        [SerializeField] private IntProperty _currentValue = new();
        [SerializeField] private IntProperty _currentMaxValue = new();

        [SerializeField] private IntPersistentProperty _savedValue;
        [SerializeField] private IntPersistentProperty _savedMaxValue;

        [SerializeField] private Sprite _icon;

        [SerializeField] private EquipmentActiveProperty _property;

        public string Id => _id;
        public Sprite Icon => _icon;
        public int CurrentValue => _currentValue.Value;
        public int CurrentMaxValue => _currentMaxValue.Value;
        public int SavedValue => _savedValue.Value;
        public int SavedMaxValue => _savedMaxValue.Value;

        internal IntProperty CurrentValueModel => _currentValue;
        internal IntProperty CurrentMaxValueModel => _currentMaxValue;
        internal IntPersistentProperty SavedValueModel => _savedValue;
        internal IntPersistentProperty SavedMaxValueModel => _savedMaxValue;
        internal EquipmentActiveProperty Property => _property;

        internal EquipmentClass(ItemDef def)
        {
            string id = def.Id;

            _id = def.Id;
            _icon = def.Sprite;
            _property = def.Property;

            string key = id + EquipmentData.I.KeySaved;

            _savedValue = new(0, key);

            key = id + EquipmentData.I.KeyMax;

            _savedMaxValue = new(def.MaxValue, key);
        }

        public void SubscribeCurrentValue(IntProperty.OnPropertyChanged call, bool subscribe)
        {
            if (subscribe) _currentValue.OnChanged += call;

            else _currentValue.OnChanged -= call;
        }
        public void SubscribeCurrentMaxValue(IntProperty.OnPropertyChanged call, bool subscribe)
        {
            if (subscribe) _currentMaxValue.OnChanged += call;

            else _currentMaxValue.OnChanged -= call;
        }
        public void SubscribeSavedValue(IntPersistentProperty.OnPropertyChanged call, bool subscribe)
        {
            if (subscribe) _savedValue.OnChanged += call;

            else _savedValue.OnChanged -= call;
        }
        public void SubscribeSavedMaxtValue(IntPersistentProperty.OnPropertyChanged call, bool subscribe)
        {
            if (subscribe) _savedMaxValue.OnChanged += call;

            else _savedMaxValue.OnChanged -= call;
        }

        internal void Validate()
        {
            int currentValue = _currentValue.Value;
            int currentMaxValue = _currentMaxValue.Value;
            int savedValue = _savedValue.Value;
            int savedMaxValue = _savedMaxValue.Value;

            bool isCurrentValueOutLimit = currentMaxValue < currentValue;
            bool isCurrentMaxValueBelowZero = currentMaxValue < 0;
            bool isSavedValueOutLimit = savedMaxValue < savedValue;
            bool isSavedMaxValueBelowZero = savedMaxValue < 0;

            if (isCurrentMaxValueBelowZero) _currentMaxValue.Value = 0;
            else _currentMaxValue.Validate();

            if (isCurrentValueOutLimit) _currentValue.Value = currentMaxValue;
            else if (currentValue < 0) _currentValue.Value = 0;
            else _currentValue.Validate();

            if (isSavedMaxValueBelowZero) _savedMaxValue.Value = 0;
            else _savedMaxValue.Validate();

            if (isSavedValueOutLimit) _savedValue.Value = savedMaxValue;
            else if (savedValue < 0) _savedValue.Value = 0;
            else _savedValue.Validate();
        }
        internal void Resave()
        {
            _savedValue.Value = _currentValue.Value;
            _savedMaxValue.Value = _currentMaxValue.Value;
        }
        internal void Reload()
        {
            _currentValue.Value = _savedValue.Value;
            _currentMaxValue.Value = _savedMaxValue.Value;
        }
    }
}
