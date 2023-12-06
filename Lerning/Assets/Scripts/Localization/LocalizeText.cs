using UnityEngine;
using UnityEngine.UI;

namespace Localization
{
    public class LocalizeText : MonoBehaviour
    {
        [SerializeField] private LocaleGroup _group;
        [SerializeField] private string _key;
        [SerializeField] private bool _capitalaze;
        [SerializeField] private Text _text;

        private void Awake()
        {
            LocalizationCore.I.OnLocaleChanged += OnLocaleChanged;
            Localize();
        }

        private void OnLocaleChanged()
        {
            Localize();
        }

        private void Localize()
        {
            string localized = LocalizationCore.GetText(_group, _key);
            _text.text = _capitalaze ? localized.ToUpper() : localized;
        }

        private void OnDestroy()
        {
            LocalizationCore.I.OnLocaleChanged -= OnLocaleChanged;
        }
    }
}
