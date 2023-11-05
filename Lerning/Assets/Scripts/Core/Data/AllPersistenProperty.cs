using System;
using UnityEngine;

namespace Core.Data.PersistentProperty
{
    [Serializable]
    public class IntPersistentProperty : PrefsPersistentProperty<int>
    {
        public IntPersistentProperty(int defaultValue, string key) : base(defaultValue, key)
        {
            Init();
        }

        protected override void Write(int value)
        {
            PlayerPrefs.SetInt(Key, value);
            PlayerPrefs.Save();
        }

        protected override int Read(int defaultValue)
        {
            return PlayerPrefs.GetInt(Key, defaultValue);
        }
    }

    [Serializable]
    public class FloatPersistentProperty : PrefsPersistentProperty<float>
    {
        public FloatPersistentProperty(float defaultValue, string key) : base(defaultValue, key)
        {
            Init();
        }

        protected override void Write(float value)
        {
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }

        protected override float Read(float defaultValue)
        {
            return PlayerPrefs.GetFloat(Key, defaultValue);
        }
    }

    [Serializable]
    public class StringPersistentProperty : PrefsPersistentProperty<string>
    {
        public StringPersistentProperty(string defaultValue, string key) : base(defaultValue, key)
        {
            Init();
        }

        protected override void Write(string value)
        {
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();
        }

        protected override string Read(string defaultValue)
        {
            return PlayerPrefs.GetString(Key, defaultValue);
        }
    }
}