using UnityEngine;

namespace Core.Settings
{
    [CreateAssetMenu(menuName = "Data/Settings/Save Slot Settings", fileName = "SaveSlotSettings")]
    public class SaveSlotSettings : ScriptableObject
    {
        public int _slot = 0;
        public int Slot => _slot;

        private static SaveSlotSettings _instance;
        internal static SaveSlotSettings I => _instance == null ? LoadSingleton() : _instance;

        public void ChangeSlot(int slot)
        {
            _slot = slot;
        }

        private static SaveSlotSettings LoadSingleton()
        {
            return _instance = Resources.Load<SaveSlotSettings>("Settings/SaveSlotSettings");
        }
    }
}
