using MoewMerge.Localization.Model;
using MoewMerge.Managers.Interfaces;
using UnityEngine;

namespace MoewMerge.Managers
{
    public class LanguageManager : MonoBehaviour, ILanguageManager
    {
        public Locale SetLocale(Locale locale) => GameManager.Instance.SetLocale(locale);
        

        public Locale GetCurrentLocale() => GameManager.Instance.GetLocale();
        

    }
}

