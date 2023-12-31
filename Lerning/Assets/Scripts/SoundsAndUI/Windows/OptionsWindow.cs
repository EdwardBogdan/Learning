using UnityEngine;

namespace SoundsAndUI.Windows
{
    public class OptionsWindow : AnimatedWindow
    {
        [SerializeField] private GameObject _mainMenuPrefab;
        [SerializeField] private GameObject _languageMenuPrefab;

        public void OnOk()
        {
            _closeAction = () =>
            {
                Instantiate(_mainMenuPrefab, CanvasTransform);
            };

            TriggerClose();
        }

        public void OnLanguage()
        {
            _closeAction = () =>
            {
                Instantiate(_languageMenuPrefab, CanvasTransform);
            };

            TriggerClose();
        }
    }
}
