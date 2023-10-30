using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean;
using Lean.Localization;
using Agava.YandexGames;
using Agava.YandexGames.Samples;

public class LanguageSwitcher : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;

    private string _russian = "Russian";
    private string _english = "English";
    private string _turkish = "Turkish";

    public void DetermineLanguage()  
    {
        string currentLanguage = YandexGamesSdk.Environment.i18n.lang;

        Debug.Log("Проверка языка");

        switch (currentLanguage)
        {
            case "en":
                _leanLocalization.SetCurrentLanguage(_english);
                break;
            case "tr":
                _leanLocalization.SetCurrentLanguage(_turkish);
                break;
            case "ru":
                _leanLocalization.SetCurrentLanguage(_russian);
                break;
        }
    }
}
