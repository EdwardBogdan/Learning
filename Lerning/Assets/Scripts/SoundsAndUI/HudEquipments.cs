using Core;
using Inventory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SoundsAndUI.UIElements
{
    public class HudEquipments : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _iconBorder;
        [SerializeField] private RectTransform _border;
        [SerializeField] private Transform _container;
        [SerializeField] private EquipmentWidget _prefab;

        internal static HudEquipments Hud;

        private void Start()
        {
            Hud = this;
            RebuildEquipmentPanel();
            EquipmentData.I.OnResorted += RebuildEquipmentPanel;

            var sprite = EquipmentData.I.ChoosedItem.Icon;

            SetIconChoosedItem(sprite);
        }

        [ContextMenu("Rebuild Border")]
        internal void RebuildEquipmentPanel()
        {
            foreach (Transform child in _container)
            {
                Destroy(child.gameObject);
            }

            List<EquipmentClass> items = EquipmentData.I.CurrentItems;

            int itemCount = items.Count;

            if (itemCount <= 0)
            {
                _iconBorder.SetActive(false);
                _border.gameObject.SetActive(false);
                return;
            }

            _iconBorder.SetActive(true);
            _border.gameObject.SetActive(true);

            foreach (var item in items)
            {
                GameObject itemCell = Instantiate(_prefab.gameObject, _container);
                var widget = itemCell.GetComponent<EquipmentWidget>();
                widget.Set(item);
            }

            float width = SystemCore.HudSettings.EquipmentBorder.WidhtBorder;

            width += SystemCore.HudSettings.EquipmentBorder.InsideSpace * itemCount;

            _border.sizeDelta = new(width, SystemCore.HudSettings.EquipmentBorder.HeightBorder);
        }
        internal void SetIconChoosedItem(Sprite icon)
        {
            _icon.sprite = icon;
        }


        private void OnDestroy()
        {
            EquipmentData.I.OnResorted -= RebuildEquipmentPanel;
        }
    }
}
