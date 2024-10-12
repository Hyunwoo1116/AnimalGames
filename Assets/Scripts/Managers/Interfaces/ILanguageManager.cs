using MoewMerge.Localization.Model;
using UnityEngine;

namespace MoewMerge.Managers.Interfaces
{
    public interface ILanguageManager 
    {
        public Locale SetLocale(Locale locale);
        public Locale GetCurrentLocale();
    }
}
