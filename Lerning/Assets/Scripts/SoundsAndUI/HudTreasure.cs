using Core;
using Inventory;
using System.Collections;
using UnityEngine;

namespace SoundsAndUI.UIElements
{
    public class HudTreasure : MonoBehaviour
    {
        //Field for check
        [SerializeField] internal int valueChanging = 0;

        [SerializeField] private Animator _animator;
        [SerializeField] private RectTransform _border;
        [SerializeField] private Transform _container;
        [SerializeField] private TreasureWidget _prefab;

        private static readonly int Show = Animator.StringToHash("Show");

        internal static HudTreasure Hud;

        internal bool isShowed = false;

        private Coroutine waiter;

        public void IsShowed(string value)
        {
            switch (value)
            {
                case "true":
                    isShowed = true;
                    break;
                case "false":
                    isShowed = true;
                    break;
                default:
                    break;
            }
        }

        private void Start()
        {
            Hud = this;
            
            var items = TreasureData.I.Treasures;

            foreach (var item in items)
            {
                GameObject itemCell = Instantiate(_prefab.gameObject, _container);
                var widget = itemCell.GetComponent<TreasureWidget>();
                widget.Set(item);
            }

            RebuildBorder();
        }
        [ContextMenu("Rebuild Border")]
        internal void RebuildBorder()
        {
            Core.Settings.HudSettings.ItemBorderData border = SystemCore.HudSettings.TreasureBorder;

            float height = border.HeightBorder;

            height += border.InsideSpace * TreasureData.I.Treasures.Length;

            _border.sizeDelta = new(border.WidhtBorder, height);
        }
        internal void CheckShow(int newValue, int oldValue)
        {
            if (waiter == null)
            {
                StartCoroutine(Waiter());
            }
        }
        private IEnumerator Waiter()
        {
            _animator.SetBool(Show, true);

            while (valueChanging > 0)
            {
                yield return new WaitForSeconds(1);
            }

            _animator.SetBool(Show, false);
        }
    }
}
