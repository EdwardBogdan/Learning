using Core.Data.PersistentProperty;
using UnityEngine;

namespace Core.Settings
{
    [CreateAssetMenu(menuName = "Data/Settings/Save Slot Settings", fileName = "SaveSlotSettings")]
    public class SaveSlotSettings : ScriptableObject
    {
        [SerializeField] private IntPersistentProperty _slot;
        [SerializeField][Range(1,3)]private int _slotCount;
        public static int SlotID => I._slot.Value;

        private static SaveSlotSettings _instance;
        internal static SaveSlotSettings I => _instance == null ? LoadSingleton() : _instance;

        public static void ChangeSlot(int slot)
        {
            slot = Mathf.Clamp(slot, 1, I._slotCount);
            I._slot.Value = slot;
        }

        private void OnValidate()
        {
            _slot.Validate();
            ChangeSlot(_slot.Value);
        }
        private static SaveSlotSettings LoadSingleton()
        {
            return _instance = Resources.Load<SaveSlotSettings>("Settings/SaveSlotSettings");
        }
    }
}
