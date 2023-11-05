using UnityEngine;

namespace Core.Inventory
{
    [CreateAssetMenu(menuName = "Data/Inventory/Treasury Data", fileName = "TreasuryData")]
    public class TreasuryData : ScriptableObject
    {
        [SerializeField] private string _keySaved = "saved";
        [SerializeField] private TreasureClass[] _treasures;

        internal TreasureClass[] Treasures => _treasures;

        internal string KeySaved => _keySaved;

        private static TreasuryData _instance;
        internal static TreasuryData I => _instance == null ? LoadSingleton() : _instance;
        private static TreasuryData LoadSingleton()
        {
            return _instance = Resources.Load<TreasuryData>("InventoryData/TreasuryData");
        }

        [ContextMenu("Recreate Treasury Data")]
        internal void Recreate()
        {
            var defList = InventorySettings.I.TreasureDefs.Treasures;

            int count = defList.Count;

            I._treasures = new TreasureClass[count];

            for (int x = 0; x < count; x++)
            {
                var item = defList[x];

                TreasureClass data = new(item);
                data.SavedValueModel.Value = 0;

                I._treasures[x] = data;
            }
        }

        [ContextMenu("Reload Treasury Data")]
        internal void Reload()
        {
            foreach (var item in _treasures)
            {
                item.Reload();
            }
        }

        [ContextMenu("Resave Treasury Data")]
        internal void Resave()
        {
            foreach (var item in _treasures)
            {
                item.Resave();
            }
        }
        internal bool ModifyTreasure(string id, int value, bool isAddOperation)
        {
            var item = GetTreasure(id);
            int currentValue = item.CurrentValueModel.Value;

            if (isAddOperation)
            {
                item.CurrentValueModel.Value = value;
            }
            else
            {
                if (currentValue < value)
                {
                    return false;
                }

                item.CurrentValueModel.Value -= value;
            }

            return true;
        }
        internal TreasureClass GetTreasure(string id)
        {
            var inventory = _treasures;

            foreach (var item in inventory)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        private void OnValidate()
        {
            foreach (var item in _treasures)
            {
                item.Validate();
            }
        }
    }
}