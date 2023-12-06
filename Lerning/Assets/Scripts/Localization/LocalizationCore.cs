using Core.Data.PersistentProperty;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Localization
{
    public class LocalizationCore
    {
        public readonly static LocalizationCore I;

        private StringPersistentProperty _localeKey = new StringPersistentProperty("eng", "LanguageKey");

        internal readonly Dictionary<LocaleGroup, Dictionary<string, string>> localeBase = new();

        public Action OnLocaleChanged;

        public static string LanguageKey => I._localeKey.Value;

        static LocalizationCore()
        {
            I = new LocalizationCore();
        }
        private LocalizationCore()
        {
            LoadLocale(_localeKey.Value);
        }

        public static string GetText(LocaleGroup group, string key)
        {
            I.localeBase.TryGetValue(group, out Dictionary<string, string> dictinary);

            if (dictinary == null) return null;

            if (dictinary.TryGetValue(key, out string value))
            {
                return value;
            }
            else
            {
                return $"key_{key}_{I._localeKey.Value}";
            }
        }
        internal static void ChangeLocaleKey(string key)
        {
            I._localeKey.Value = key;

            I.LoadLocale(I._localeKey.Value);
        }
        private void LoadLocale(string localeKey)
        {
            LocaleDef _localeDef = Resources.Load<LocaleDef>($"Locale/{localeKey}");

            localeBase.Clear();

            var datas = _localeDef.GetDatas;

            foreach (LocaleData data in datas)
            {
                Dictionary<string, string> couples = new();

                foreach (TextData item in data.Locale)
                {
                    couples.Add(item.Key, item.Value);
                }

                localeBase.Add(data.Group, couples);
            }

            OnLocaleChanged?.Invoke();
        }
    }

    public enum LocaleGroup
    { 
        Button,
        Title,
        Dialog,
    }
    public enum Language
    {
        eng,
        rus,
        ukr,
    }

}
