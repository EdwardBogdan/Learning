using Core.Data.ObservableProperty;
using Core.Data.PersistentProperty;
using System;
using UnityEngine;

namespace Core.Inventory
{ 
    [Serializable]
    public class TreasureClass
    {
        [SerializeField] private string _id;
    
        [SerializeField] private IntProperty _currentValue = new();
    
        [SerializeField] private IntPersistentProperty _savedValue;
    
        [SerializeField] private Sprite _icon;
    
        public string Id => _id;
        public Sprite Icon => _icon;
    
        public int CurrentValue => _currentValue.Value;
        public int SavedValue => _savedValue.Value;
    
        internal IntProperty CurrentValueModel => _currentValue;
        internal IntPersistentProperty SavedValueModel => _savedValue;
    
        internal TreasureClass(TreasureDef def)
        {
            string id = def.Id;
    
            _id = def.Id;
            _icon = def.Sprite;
    
            string key = id + EquipmentData.I.KeySaved;
    
            _savedValue = new(0, key);
        }
    
        public void SubscribeCurrentValue(IntProperty.OnPropertyChanged call, bool subscribe)
        {
            if (subscribe)
            {
                _currentValue.OnChanged += call;
            }
            else
            {
                _currentValue.OnChanged -= call;
            }
        }
        public void SubscribeSavedtValue(IntPersistentProperty.OnPropertyChanged call, bool subscribe)
        {
            if (subscribe)
            {
                _savedValue.OnChanged += call;
            }
            else
            {
                _savedValue.OnChanged -= call;
            }
        }
    
        internal void Validate()
        {
            int currentValue = _currentValue.Value;
            int savedValue = _savedValue.Value;
    
            if (currentValue < 0)
            {
                _currentValue.Value = 0;
            }
            else
            {
                _currentValue.Validate();
            }
    
            if (savedValue < 0)
            {
                _savedValue.Value = 0;
            }
            else
            {
                _savedValue.Validate();
            }
        }
        internal void Resave()
        {
            _savedValue.Value = _currentValue.Value;
        }
        internal void Reload()
        {
            _currentValue.Value = _savedValue.Value;
        }
    }
}
