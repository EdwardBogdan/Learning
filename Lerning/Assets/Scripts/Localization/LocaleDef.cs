using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Localization
{
    [CreateAssetMenu(menuName = "Data/LocaleDef", fileName = "LocaleDef")]
    public class LocaleDef : ScriptableObject
    {
        [SerializeField] private LocaleData[] _base;

        internal LocaleData[] GetDatas => _base;

        [ContextMenu("Update locale")]
        private void UpdateLocale()
        {
            foreach (var item in _base)
            {
                item.UpdateLocale();
            }
        }
    }

    [Serializable]
    internal class TextData
    {
        public string Key;
        public string Value;

        public TextData(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    [Serializable]
    internal class LocaleData
    {
        [SerializeField] private LocaleGroup _group;
        [SerializeField] private string _url;

        [SerializeField] private List<TextData> locale;

        public LocaleGroup Group => _group;
        public List<TextData> Locale => locale;

        private UnityWebRequest _request;

        public void UpdateLocale()
        {
            if (_request == null)
            {
                _request = UnityWebRequest.Get(_url);
                _request.SendWebRequest().completed += OnDataLoaded;
            }
        }
        private void OnDataLoaded(AsyncOperation operation)
        {
            if (operation.isDone)
            {
                var rows = _request.downloadHandler.text.Split('\n');
                locale.Clear();
                foreach (var row in rows)
                {
                    AddLocaleItem(row, locale);
                }
                _request = null;
            }
        }

        private static void AddLocaleItem(string row, List<TextData> mas)
        {
            try
            {
                var parts = row.Split('\t');
                mas.Add(new(parts[0], parts[1]));
            }
            catch (Exception e)
            {
                Debug.LogError($"Can`t parse row: {row}. \n {e}");
            }
        }
    }
}
