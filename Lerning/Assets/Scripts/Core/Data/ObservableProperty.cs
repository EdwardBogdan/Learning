using System;
using UnityEngine;

namespace Core.Data.ObservableProperty
{
    public abstract class ObservableProperty<TPropertyType>
    {
        [SerializeField] private TPropertyType _value;

        private TPropertyType _stored;

        public delegate void OnPropertyChanged(TPropertyType newValue, TPropertyType oldValue);

        public event OnPropertyChanged OnChanged;

        public TPropertyType Value
        {
            get => _value;
            set
            {
                var isSame = _stored.Equals(value);
                if (isSame)
                {
                    _stored = _value = value;
                    return;
                } 
                var oldValue = _stored;
                _stored = _value = value;

                OnChanged?.Invoke(_value, oldValue);
            }
        }

        public void Validate()
        {
            Value = _value;
        }

        public Delegate[] GetEvents()
        {
            return OnChanged.GetInvocationList();
        }
    }
}
