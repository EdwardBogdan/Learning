using Localization;
using System;
using UnityEngine;

namespace SoundsAndUI.Windows
{
    public class LanguageMenuWindow : AnimatedWindow
    {
        [SerializeField] private GameObject _optionsPrefab;

        [SerializeField] private RectTransform _container;

        private Action OnCloseBorder;

        protected override void Start()
        {
            base.Start();

            foreach (Transform item in _container.transform)
            {
                if(item.TryGetComponent(out LocaleItemWidget component))
                {
                    if (component.LanguageKey.ToString() == LocalizationCore.LanguageKey)
                    {
                        ChangeBorder(item.gameObject);
                    }
                }
            }
        }

        public void ChangeBorder(GameObject obj)
        {
            OnCloseBorder?.Invoke();

            GameObject border = obj.transform.Find("Border").gameObject;

            border.SetActive(true);

            OnCloseBorder = () =>
            {
                border.SetActive(false);
            };
        }

        public void OnOk()
        {
            _closeAction = () =>
            {
                Instantiate(_optionsPrefab, CanvasTransform);
            };

            TriggerClose();
        }
    }
}
