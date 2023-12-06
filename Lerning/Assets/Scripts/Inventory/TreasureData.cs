using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(menuName = "Data/Inventory/Treasury Data", fileName = "TreasuryData")]
    public class TreasureData : ScriptableObject
    {
        [SerializeField] private string _keySaved = "saved";
        [SerializeField] private TreasureClass[] _treasures;
        [SerializeField] private int _maxValue;

        internal TreasureClass[] Treasures => _treasures;

        internal string KeySaved => _keySaved;

        internal bool ModifyTreasure(string id, int value, bool isAddOperation)
        {
            var item = GetTreasure(id);
            int currentValue = item.CurrentValueModel.Value;

            if (isAddOperation)
            {
                item.CurrentValueModel.Value += value;
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


        [ContextMenu("Recreate Treasury Data")]
        private void Recreate()
        {
            var defList = InventoryDefs.I.TreasureDefs.Treasures;

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
        private void Reload()
        {
            foreach (var item in _treasures)
            {
                item.Reload();
            }
        }

        [ContextMenu("Resave Treasury Data")]
        private void Resave()
        {
            foreach (var item in _treasures)
            {
                item.Resave();
            }
        }

        #region Editor
#if UNITY_EDITOR
        public int MaxValue { get => _maxValue; set { _maxValue = value; } }
        public TreasureClass[] EditTreasures => _treasures;
        public void EditRecreate()
        {
            Recreate();
        }
        public void EditReload()
        {
            Reload();
        }
        public void EditResave()
        {
            Resave();
        }
        private void OnValidate()
        {
            foreach (var item in _treasures)
            {
                item.Validate();
            }
        }
#endif
        #endregion

        #region Singleton
        private static TreasureData _instance;
        internal static TreasureData I => _instance == null ? LoadSingleton() : _instance;
        private static TreasureData LoadSingleton()
        {
            return _instance = Resources.Load<TreasureData>("InventoryData/Data_Treasury");
        }
        #endregion
    }
}