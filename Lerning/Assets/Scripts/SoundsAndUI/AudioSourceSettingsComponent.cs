using Core.Data.PersistentProperty;
using UnityEngine;
using Core;

namespace SoundsAndUI.Settings
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceSettingsComponent : MonoBehaviour
    {
        [SerializeField] private SoundSettingsMode _mode;

        private AudioSource _source;
        private FloatPersistentProperty _model;

        private void Start()
        {
            _source = GetComponent<AudioSource>();

            _model = SystemCore.SoundSettings.GetSoundProperty(_mode);

            _model.OnChanged += OnSoundSettingChanged;

            _source.volume = _model.Value;
        }

        private void OnSoundSettingChanged(float newValue, float oldValue)
        {
            _source.volume = newValue;
        }

        private void OnDestroy()
        {
            if(_model != null)
            _model.OnChanged -= OnSoundSettingChanged;
        }
    }
}
