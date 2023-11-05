using UnityEngine;

namespace SoundsAndUI.Windows
{
    public class OptionsWindow : AnimatedWindow
    {
        [SerializeField] private GameObject _mainMenuPrefab;

        public void OnOk()
        {
            _closeAction = () =>
            {
                Instantiate(_mainMenuPrefab, CanvasTransform);
            };

            TriggerClose();
        }
    }
}
