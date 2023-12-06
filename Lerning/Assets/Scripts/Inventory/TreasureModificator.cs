using Inventory.Attribute;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    public class TreasureModificator : MonoBehaviour
    {
        [SerializeField, Treasure] private string _treasureId;
        [SerializeField] private int _count;
        [SerializeField] private bool _adding;

        [SerializeField] private UnityEvent _onSuccesfull;
        [SerializeField] private UnityEvent _onNotSuccesfull;

        public void OnModyfyTreasure()
        {
            if (TreasureData.I.ModifyTreasure(_treasureId, _count, _adding))
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
