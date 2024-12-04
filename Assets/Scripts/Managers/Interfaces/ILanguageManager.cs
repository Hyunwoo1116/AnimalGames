using MoewMerge.Localization.Model;
using UnityEngine;

namespace MoewMerge.Managers.Interfaces
{
    public interface ILanguageManager 
    {
        public Locales SetLocale(Locales locale);
        public Locales GetCurrentLocale();
    }
}
