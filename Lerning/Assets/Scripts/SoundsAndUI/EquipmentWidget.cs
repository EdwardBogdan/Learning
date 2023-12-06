using Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace SoundsAndUI.UIElements
{
    internal class EquipmentWidget : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Text _text;

        private EquipmentClass _item;
        internal EquipmentClass Item => _item;

        internal void Set(EquipmentClass item)
        {
            _item = item;
            _image.sprite = item.Icon;
            item.SubscribeCurrentValue(ChangeCount, true);

            EquipmentData.I.OnChoosed +=OnChoosed;

            int value = item.CurrentValue;

            _text.text = $"x{value}";
        }
        internal void OnChoosed(EquipmentClass item)
        {
            if (_item != item) return;

            HudEquipments.Hud.SetIconChoosedItem(item.Icon);
        }

        private void ChangeCount(int newValue, int oldValue)
        {
            _text.text = $"x{newValue}";
        }

        private void OnDestroy()
        {
            if (_item == null) return;

            _item.SubscribeCurrentValue(ChangeCount, false);
            EquipmentData.I.OnChoosed -= OnChoosed;
        }
    }
}
