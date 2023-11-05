using System;
using UnityEngine;

namespace Core.Settings
{
    [CreateAssetMenu(menuName = "Data/Settings/Hud Settings", fileName = "HudSettings")]
    public class HudSettings : ScriptableObject
    {
        [SerializeField] private Color _colorTreasureSleep;
        [SerializeField] private Color _colorTreasureAdd;
        [SerializeField] private Color _colorTreasureRemove;

        [SerializeField] private FontStyle _baseTreasureFont;
        [SerializeField] private FontStyle _changingTreasureFont;

        [SerializeField] private float _treasureChangeDelay = 0.2f;

        [SerializeField] private ItemBorderData _equipmentBorder;
        [SerializeField] private ItemBorderData _treasureBorder;

        public Color ColorTreasureSleep => _colorTreasureSleep;
        public Color ColorTreasureAdd => _colorTreasureAdd;
        public Color ColorTreasureRemove => _colorTreasureRemove;
        public FontStyle BaseTreasureFont => _baseTreasureFont;
        public FontStyle ChangingTreasureFont => _changingTreasureFont;
        public float TreasureChangeDelay => _treasureChangeDelay;
        public ItemBorderData EquipmentBorder => _equipmentBorder;
        public ItemBorderData TreasureBorder => _treasureBorder;

        private static HudSettings _instance;
        internal static HudSettings I => _instance == null ? LoadSingleton() : _instance;
        private static HudSettings LoadSingleton()
        {
            return _instance = Resources.Load<HudSettings>("Settings/HudSettings");
        }

        [Serializable]
        public class ItemBorderData
        {
            [SerializeField] private float _heightBorder;
            [SerializeField] private float _insideSpace;
            [SerializeField] private float _widhtBorder;

            public float HeightBorder => _heightBorder;
            public float InsideSpace => _insideSpace;
            public float WidhtBorder => _widhtBorder;
        }
    }
}
