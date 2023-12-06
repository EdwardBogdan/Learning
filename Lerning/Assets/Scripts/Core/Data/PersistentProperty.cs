using System;
using UnityEngine;

namespace Core.Data.PersistentProperty
{
    public abstract class PersistentProperty<TPropertyType> : IDisposable
    {
        [SerializeField] private TPropertyType _value;

        private TPropertyType _stored;

        private readonly TPropertyType _defaultValue;

        public delegate void OnPropertyChanged(TPropertyType newValue, TPropertyType oldValue);

        public event OnPropertyChanged OnChanged;

        public PersistentProperty(TPropertyType defaultValue)
        {
            _defaultValue = defaultValue;
        }

        public TPropertyType Value
        {
            get => _stored;
            set
            {
                var isEquals = _stored.Equals(value);
                if (isEquals) return;

                var oldValue = _stored;
                Write(value);
                _stored = _value = value;

                OnChanged?.Invoke(value, oldValue);
            }
        }

        public void Init()
        {
            _stored = _value = Read(_defaultValue);
        }

        protected abstract void Write(TPropertyType value);
        protected abstract TPropertyType Read(TPropertyType defaulValue);

        public void Validate()
        {
            Value = _value;
        }

        public void Dispose()
        {
            OnChanged = null;
        }
    }
}
