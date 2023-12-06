using Core;
using Inventory;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SoundsAndUI.UIElements
{
    public class TreasureWidget : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Text _text;


        private TreasureClass _item;

        private Coroutine _countCoroutine;

        private int countDelta;

        private int finishValue;

        private int multy;

        public void Set(TreasureClass item)
        {
            _item = item;
            _image.sprite = item.Icon;
            _text.text = $"x{item.CurrentValue}";
            _text.color = SystemCore.HudSettings.ColorTreasureSleep;
            item.SubscribeCurrentValue(ChangeCount, true);
            item.SubscribeCurrentValue(HudTreasure.Hud.CheckShow, true);
        }

        private void ChangeCount(int newValue, int oldValue)
        {
            finishValue = newValue;

            multy = newValue > countDelta ? 1 : -1;

            _text.color = multy == 1 ? SystemCore.HudSettings.ColorTreasureAdd : SystemCore.HudSettings.ColorTreasureRemove;

            if (_countCoroutine == null)
            {
                countDelta = oldValue;
                _countCoroutine = StartCoroutine(Counter());
            }
        }

        private void OnDestroy()
        {
            if (_item == null) return;

            _item.SubscribeCurrentValue(ChangeCount, false);
            _item.SubscribeCurrentValue(HudTreasure.Hud.CheckShow, false);
        }

        private IEnumerator Counter()
        {
            HudTreasure.Hud.valueChanging++;

            while (!HudTreasure.Hud.isShowed)
            {
                yield return new WaitForSeconds(0.5f);
            }

            int size = _text.fontSize;

            _text.fontStyle = SystemCore.HudSettings.ChangingTreasureFont;

            _text.fontSize = size + 1;

            while (countDelta != finishValue)
            {
                countDelta += multy;

                _text.text = $"x{countDelta}";

                yield return new WaitForSeconds(SystemCore.HudSettings.TreasureChangeDelay);
            }

            _countCoroutine = null;
            _text.fontStyle = SystemCore.HudSettings.BaseTreasureFont;
            _text.fontSize = size;
            _text.color = SystemCore.HudSettings.ColorTreasureSleep;
            HudTreasure.Hud.valueChanging--;
        }
    }
}
