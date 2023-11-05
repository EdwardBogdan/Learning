using Core;
using UnityEngine;

namespace SoundsAndUI.UIElements
{
    public class HudEquipments : MonoBehaviour
    {
        [SerializeField] private RectTransform _border;
        [SerializeField] private Transform _container;
        [SerializeField] private EquipmentWidget _prefab;

        internal int itemCount;

        internal static HudEquipments Hud;

        internal delegate void ItemBorderCloser();

        internal event ItemBorderCloser OnCloser;

        private EquipmentWidget[] _activedWidgets;

        private void Start()
        {
            Hud = this;
            var items = PlayerCore.PlayerItemsData;
            itemCount = items.Length;

            foreach (var item in items)
            {
                GameObject itemCell = Instantiate(_prefab.gameObject, _container);
                var widget = itemCell.GetComponent<EquipmentWidget>();
                widget.Set(item);
            }

            RebuildEquipmentPanel();
        }
        public void ChooseItem(int index)
        {
            if (index > _activedWidgets.Length) return;

            index--;

            OnCloser?.Invoke();
            OnCloser = null;

            EquipmentWidget widget = _activedWidgets[index];
            widget.ActivateItemBorder();
        }

        [ContextMenu("Rebuild Border")]
        internal void RebuildEquipmentPanel()
        {
            if (itemCount == 0)
            {
                _border.sizeDelta = new(0, 0);
                _activedWidgets = new EquipmentWidget[0];
                return;
            }

            float width = SystemCore.HudSettings.EquipmentBorder.WidhtBorder;

            width += SystemCore.HudSettings.EquipmentBorder.InsideSpace * itemCount;

            _border.sizeDelta = new(width, SystemCore.HudSettings.EquipmentBorder.HeightBorder);

            _activedWidgets = new EquipmentWidget[itemCount];

            int count = 0;

            foreach (Transform child in _container)
            {
                if (child.gameObject.activeSelf)
                {
                    _activedWidgets[count] = child.GetComponent<EquipmentWidget>();
                    count++;
                }
            }
        }
    }
}
