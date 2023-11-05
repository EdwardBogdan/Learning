using UnityEngine;
using System.Collections.Generic;
using System;

namespace CustomColorPack
{
    [CreateAssetMenu(menuName = "Data/Color/CustomColorStorage", fileName = "CustomColorStorage")]
    public class CustomColorStorage : ScriptableObject
    {
        [SerializeField] private List<ColorData> _colorData;

        private static CustomColorStorage _instance;

        public List<ColorData> ColorsBase => _colorData;        
        public static CustomColorStorage I => _instance == null ? LoadBase() : _instance;

        public static ColorData GetDataById(string id)
        {
            List<ColorData> colors = I.ColorsBase;
            foreach (ColorData data in colors)
            {
                if (data.Id != id) continue;
                return data;
            }
            
            return default;
        }
        public static ColorData GetDataByColor(Color color)
        {
            List<ColorData> colors = I.ColorsBase;
            foreach (ColorData data in colors)
            {
                if (data.Color != color) continue;
                return data;
            }

            return default;
        }
        public static List<string> GetAllId()
        {
            List<ColorData> datas = I.ColorsBase;

            List<string> list = new();

            foreach (ColorData item in datas)
            {
                list.Add(item.Id);
            }
            return list;
        }
        private static CustomColorStorage LoadBase()
        {
            return _instance = Resources.Load<CustomColorStorage>("ColorCustomBase");
        }

        [Serializable]
        public struct ColorData
        {
            [SerializeField] private string _id;
            [SerializeField] private Color _color;

            public string Id => _id;
            public Color Color => _color;
        }
    }
}
