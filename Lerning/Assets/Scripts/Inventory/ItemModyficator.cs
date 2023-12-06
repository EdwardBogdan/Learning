using Inventory.Attribute;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    public class ItemModyficator : MonoBehaviour
    {
        [SerializeField, Items] private string _itemId;
        [SerializeField] private int _count;
        [SerializeField] private bool _adding;

        [SerializeField] private UnityEvent _onSuccesfull;
        [SerializeField] private UnityEvent _onNotSuccesfull;

        public void OnModyfyItem()
        {
            if (EquipmentData.I.ModifyItem(_itemId, _count, _adding))
            {
                _onSuccesfull?.Invoke();
            }
            else
            {
                _onNotSuccesfull?.Invoke();
            }
        }
        public void SetAddBool(bool value)
        {
            _adding = value;
        }
        public void SetCount(int value)
        {
            _count = value;
        }
    }
}
