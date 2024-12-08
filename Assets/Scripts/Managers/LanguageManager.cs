using MoewMerge.Localization.Model;
using MoewMerge.Managers.Interfaces;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using Zenject;

namespace MoewMerge.Managers
{
    public class LanguageManager : MonoBehaviour, ILanguageManager
    {
        [Inject] IGameManager GameManager;
        public Locales SetLocale(Locales locale)
        {
            Locale createLocale = Locale.CreateLocale(locale.ToString());
            LocalizationSettings.SelectedLocale = createLocale;
            return GameManager.SetLocale(locale);
        }
        public Locales GetCurrentLocale() => GameManager.GetLocale();
    }
}

