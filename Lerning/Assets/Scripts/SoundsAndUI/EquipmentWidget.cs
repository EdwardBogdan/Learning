using Core;
using Core.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace SoundsAndUI.UIElements
{
    public class EquipmentWidget : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Text _text;
        [SerializeField] private GameObject _border;

        private EquipmentClass _item;

        internal EquipmentClass Item => _item;

        public void Set(EquipmentClass item)
        {
            _item = item;
            _image.sprite = item.Icon;
            item.SubscribeCurrentValue(ChangeCount, true);

            int value = item.CurrentValue;

            _text.text = $"x{value}";

            if (value <= 0)
            {
                gameObject.SetActive(false);
                HudEquipments.Hud.itemCount--;
                HudEquipments.Hud.RebuildEquipmentPanel();
            }
            else if (gameObject.activeSelf == false && value > 0)
            {
                gameObject.SetActive(true);
                HudEquipments.Hud.itemCount++;
                HudEquipments.Hud.RebuildEquipmentPanel();
            }
        }

        private void ChangeCount(int newValue, int oldValue)
        {
            _text.text = $"x{newValue}";

            if (newValue <= 0)
            {
                gameObject.SetActive(false);
                HudEquipments.Hud.itemCount--;
                HudEquipments.Hud.RebuildEquipmentPanel();
                HudEquipments.Hud.ChooseItem(1);
                PlayerCore.PlayerEquipmentFunctional.ChooseItem(1);
            }
            else if (gameObject.activeSelf == false && newValue > 0)
            {
                gameObject.SetActive(true);
                HudEquipments.Hud.itemCount++;
                HudEquipments.Hud.RebuildEquipmentPanel();
                HudEquipments.Hud.ChooseItem(1);
                PlayerCore.PlayerEquipmentFunctional.ChooseItem(1);
            }
        }
        internal void ActivateItemBorder()
        {
            _border.gameObject.SetActive(true);
            HudEquipments.Hud.OnCloser += () =>
            {
                _border.gameObject.SetActive(false);
            };
        }

        private void OnDestroy()
        {
            if (_item == null) return;

            _item.SubscribeCurrentValue(ChangeCount, false);
        }
    }
}
