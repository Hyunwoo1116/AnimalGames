using MoewMerge.Localization.Model;
using MoewMerge.Managers.Interfaces;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace MoewMerge.Managers
{
    public class LanguageManager : MonoBehaviour, ILanguageManager
    {
        public Locales SetLocale(Locales locale)
        {
            Locale createLocale = Locale.CreateLocale(locale.ToString());
            LocalizationSettings.SelectedLocale = createLocale;
            return GameManager.Instance.SetLocale(locale);
        }
        public Locales GetCurrentLocale() => GameManager.Instance.GetLocale();
    }
}

