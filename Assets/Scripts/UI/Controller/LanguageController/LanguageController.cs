using MoewMerge.Localization.Model;
using MoewMerge.Managers.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Editor;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MoewMerge.UI.LanguageController
{
    public class LanguageController : MonoBehaviour
    {
        public Locale LanguageType;

        private Toggle toggle;
        public Toggle LanguageToggle => toggle ??= this.GetComponent<Toggle>();

        private RawImage checkMark;
        public RawImage CheckMark => checkMark ??= this.transform.GetChild(0).GetComponent<RawImage>();
        [Inject]
        private ILanguageManager languageManager;
        private void OnEnable()
        {
            LanguageToggle.onValueChanged.RemoveAllListeners();
            LanguageToggle.onValueChanged.AddListener(ChangeCurrentLanguage);
            LanguageToggle.isOn = languageManager.GetCurrentLocale() == LanguageType;
        }

        private void ChangeCurrentLanguage(bool value)
        {
            CheckMark.color = value ? Color.white : Color.clear;
            if (value)
                languageManager.SetLocale(LanguageType);
        }
    }
}














