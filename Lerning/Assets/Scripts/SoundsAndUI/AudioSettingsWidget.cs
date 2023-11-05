using Core.Data.PersistentProperty;
using UnityEngine;
using UnityEngine.UI;
using Core;

namespace SoundsAndUI.UIElements
{
    public class AudioSettingsWidget : MonoBehaviour
    {
        [SerializeField] private SoundSettingsMode _mode;

        [SerializeField] private Slider _slider;
        [SerializeField] private Text _text;

        private FloatPersistentProperty _model;

        void Start()
        {
            _slider.onValueChanged.AddListener(OnSliderValueChanged);

            _model = SystemCore.SoundSettings.GetSoundProperty(_mode);

            _model.OnChanged += OnValueChanged;
            OnValueChanged(_model.Value, _model.Value);
        }

        private void OnSliderValueChanged(float value)
        {
            _model.Value = value;
        }

        private void OnValueChanged(float newValue, float oldValue)
        {

            var value = Mathf.Round(newValue * 100);
            _text.text = value.ToString();

            _slider.normalizedValue = newValue; 
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
            _model.OnChanged -= OnValueChanged;
        }
    }
}
