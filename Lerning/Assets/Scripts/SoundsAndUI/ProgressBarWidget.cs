using UnityEngine;
using UnityEngine.UI;

namespace SoundsAndUI.UIElements
{
    public class ProgressBarWidget : MonoBehaviour
    {
        [SerializeField] private Image _bar;

        public void SetProgress(float progress)
        {
            _bar.fillAmount = progress;
        }

        public void SerColor(Color color)
        {
            _bar.color = color;
        }
    }
}
