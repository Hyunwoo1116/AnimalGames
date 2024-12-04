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
            LocalizationSettings.AvailableLocales.Locales.ForEach(locale =>
                {
                    Debug.Log($"LHWCode{locale.Identifier.Code}");
                    Debug.Log($"LHWEqualsCode{locale.Identifier.Code.Equals(locale.ToString())}");
                    Debug.Log(locale.Formatter.ToString().Equals(locale.ToString()));
                    Debug.Log($"LHW{locale.Formatter}");
                    Debug.Log(locale.LocaleName);
                    Debug.Log($"LHW{locale}");
                    Debug.Log($"LHW{locale.CustomFormatterCode}");
                });
            ;
            Debug.Log($"LHWLocale{locale}");
            Locale createLocale = Locale.CreateLocale(locale.ToString());
            //Locale findLocale = LocalizationSettings.AvailableLocales.Locales.Select(locale => locale).Where(locale => locale.Identifier.Code.Equals(locale.ToString())).First();
            LocalizationSettings.SelectedLocale = createLocale;
            return GameManager.Instance.SetLocale(locale);
        }
        public Locales GetCurrentLocale() => GameManager.Instance.GetLocale();
    }
}

