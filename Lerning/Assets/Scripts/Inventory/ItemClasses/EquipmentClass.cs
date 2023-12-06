using Core.Data.ObservableProperty;
using Core.Data.PersistentProperty;
using Inventory.ItemProperty;
using UnityEngine;
using System;
using Core.Settings;

namespace Inventory
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

        #region Open Propertyes
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

#if UNITY_EDITOR
        public IntProperty EditCurrentValueModel => _currentValue;
        public IntProperty EditCurrentMaxValueModel => _currentMaxValue;
        public IntPersistentProperty EditSavedValueModel => _savedValue;
        public IntPersistentProperty EditSavedMaxValueModel => _savedMaxValue;
#endif
        #endregion

        internal static readonly string KeySaved = "saved";
        internal static readonly string KeyMax = "max";

        internal EquipmentClass(ItemDef def)
        {
            _id = def.Id;
            _icon = def.Sprite;
            _property = def.Property;

            _currentValue.OnChanged += OnEquipItem;

            string key = _id + KeySaved + SaveSlotSettings.SlotID;

            _savedValue = new(0, key);

            key = _id + KeyMax + SaveSlotSettings.SlotID;

            _savedMaxValue = new(def.MaxValue, key);

            _currentValue.Value = _savedValue.Value;
            _currentMaxValue.Value = _savedMaxValue.Value;
        }

        internal void Resave()
        {
            _savedValue.Value = _currentValue.Value;
            _savedMaxValue.Value = _currentMaxValue.Value;
        }
        internal void Validate()
        {
            _currentValue.Validate();
            _currentMaxValue.Validate();
            _savedValue.Validate();
            _savedMaxValue.Validate();
        }
        private void OnEquipItem(int newValue, int oldValue)
        {
            if (newValue > 0 && oldValue <= 0)
            {
                EquipmentData.I.EquipItem(this, true);
            }
            else if (newValue <= 0 && oldValue > 0)
            {
                EquipmentData.I.EquipItem(this, false);
            }
        }

        #region SubscribeInsideModels
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
        #endregion
    }
}
