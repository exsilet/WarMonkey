using System;
using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

namespace UI
{
    public class Localization : MonoBehaviour
    {
        private const string Russian = "Russian";
        private const string English = "English";
        private const string Turkish = "Turkish";

        [SerializeField] private LeanLocalization _leanLanguage;
        
        private void Awake()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            ChangeLanguage();
#endif
        }

        private void ChangeLanguage()
        {
            string languageCode = YandexGamesSdk.Environment.i18n.lang;

            switch (languageCode)
            {
                case English:
                    _leanLanguage.SetCurrentLanguage(English);
                    break;
                case Russian:
                    _leanLanguage.SetCurrentLanguage(Russian);
                    break;
                case Turkish:
                    _leanLanguage.SetCurrentLanguage(Turkish);
                    break;
                
            }
        }
    }
}