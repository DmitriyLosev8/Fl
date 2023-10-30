using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using Agava.YandexGames.Samples;

public class StartSDK : MonoBehaviour
{
    [SerializeField] LanguageSwitcher _languageSwitcher;


    private void Awake()
    {
        StartCoroutine(Start());
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        yield return YandexGamesSdk.Initialize();

        _languageSwitcher.DetermineLanguage();
        
        YandexGamesSdk.GameReady();

        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.StartAuthorizationPolling(1500);
    }
}
