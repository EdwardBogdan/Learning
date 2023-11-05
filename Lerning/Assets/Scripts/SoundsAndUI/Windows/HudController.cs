using Core;
using SoundsAndUI.UIElements;
using UnityEngine;

namespace SoundsAndUI.Windows
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private float healthForPixel = 0.25f;
        [SerializeField] private ProgressBarWidget _healthBar;

        private void Start()
        {
            PlayerCore.PlayerCharacteristics.Health.OnChanged += OnHealthChanged;
            PlayerCore.PlayerCharacteristics.MaxHealth.OnChanged += OnMaxHealthChanged;

            OnHealthChanged(PlayerCore.PlayerCharacteristics.Health.Value, 0);
            OnMaxHealthChanged(PlayerCore.PlayerCharacteristics.MaxHealth.Value, 0);
        }

        private void RebuildInventory()
        { 
            
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            int max = PlayerCore.PlayerCharacteristics.MaxHealth.Value;
            float value = (float)newValue / max;
            _healthBar.SetProgress(value);
        }
        private void OnMaxHealthChanged(int newValue, int oldValue)
        {
            var rect = _healthBar.GetComponent<RectTransform>();
            rect.sizeDelta = new(newValue * healthForPixel, 32);
            int currentHealth = PlayerCore.PlayerCharacteristics.Health.Value;
            float value = (float)currentHealth / newValue;
            _healthBar.SetProgress(value);
        }

        private void OnDestroy()
        {
            PlayerCore.PlayerCharacteristics.Health.OnChanged -= OnHealthChanged;
            PlayerCore.PlayerCharacteristics.MaxHealth.OnChanged -= OnMaxHealthChanged;
        }
    }
}
