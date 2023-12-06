using Core;
using Core.Characters;
using SoundsAndUI.UIElements;
using System.Collections;
using UnityEngine;

namespace SoundsAndUI.Windows
{
    public class HudHealtBar : MonoBehaviour
    {
        [SerializeField] private float healthForPixel = 0.25f;
        [SerializeField] private ProgressBarWidget _healthBar;
        [SerializeField] private float _brightTime;
        [SerializeField] private Color colorBase;
        [SerializeField] private Color colorChange;

        private void Start()
        {
            PlayerCharacteristics player = PlayerCharacteristics.I;

            player.Health.OnChanged += OnHealthChanged;
            player.MaxHealth.OnChanged += OnMaxHealthChanged;

            OnHealthChanged(player.Health.Value, 0);
            OnMaxHealthChanged(player.MaxHealth.Value, 0);
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            StartCoroutine(ColorChanger());
            int max = PlayerCharacteristics.I.MaxHealth.Value;
            float value = (float)newValue / max;
            _healthBar.SetProgress(value);
        }
        private void OnMaxHealthChanged(int newValue, int oldValue)
        {
            var rect = _healthBar.GetComponent<RectTransform>();
            rect.sizeDelta = new(newValue * healthForPixel, 32);
            int currentHealth = PlayerCharacteristics.I.Health.Value;
            float value = (float)currentHealth / newValue;
            _healthBar.SetProgress(value);
        }

        private void OnDestroy()
        {
            PlayerCharacteristics.I.Health.OnChanged -= OnHealthChanged;
            PlayerCharacteristics.I.MaxHealth.OnChanged -= OnMaxHealthChanged;
        }

        private IEnumerator ColorChanger()
        {
            _healthBar.SerColor(colorChange);

            yield return new WaitForSeconds(_brightTime);

            _healthBar.SerColor(colorBase);
        }
    }
}
