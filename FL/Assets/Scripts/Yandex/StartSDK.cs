using UnityEngine;
using System.Collections;
using Agava.YandexGames;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Yandex
{
    public class StartSDK : MonoBehaviour
    {
        private int _mainMenu = 1;

        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            LoadMainMenu();
            yield break;
#endif
            yield return YandexGamesSdk.Initialize(LoadMainMenu);

            YandexGamesSdk.GameReady();
        }

        private void LoadMainMenu()
        {
            SceneManager.LoadScene(_mainMenu);
        }
    }
}