using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(menuName = "Data/Inventory/Treasure Defs", fileName = "TreasureDefs")]
    internal class TreasureDefs : ScriptableObject
    {
        [SerializeField] private List<TreasureDef> _treasures;
        internal List<TreasureDef> Treasures => _treasures;
    }

    [Serializable]
    internal struct TreasureDef
    {
        [SerializeField] private string _id;

        [SerializeField] private Sprite _sprite;
        internal string Id => _id;
        internal Sprite Sprite => _sprite;

        internal bool IsVoid => string.IsNullOrEmpty(_id); //Do I need it?
    }
}
