using UnityEngine;

namespace Localization
{
    public class LocaleItemWidget : MonoBehaviour
    {
        [SerializeField] private Language language;

        public Language LanguageKey => language;

        public void OnChanger()
        {
            LocalizationCore.ChangeLocaleKey(language.ToString());
        }
    }
}
