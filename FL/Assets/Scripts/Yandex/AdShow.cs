using System;
using UnityEngine;
using Agava.YandexGames;
using Assets.Scripts.AuxiliaryComponents;
using Assets.Scripts.GUI;

namespace Assets.Scripts.Yandex
{
    public class AdShow : MonoBehaviour
    {
        public static Action AdOpened;
        public static Action AdClosed;

        private Action _adRewarded;
        private Action<bool> _interstitialAdClosed;
        private Action<string> _addErrorOccured;
        private int _increasedAngle = 8;

        private void OnEnable()
        {
            UpgradeLightPanel.UpgadeButtonClicked += OnUpdgadeButtonClicked;
            FinishLevelPanel.NextLevelButtonClicked += OnNextLevelButtonClicked;
            _adRewarded += OnAddRewarded;
        }

        private void OnDisable()
        {
            UpgradeLightPanel.UpgadeButtonClicked -= OnUpdgadeButtonClicked;
            FinishLevelPanel.NextLevelButtonClicked -= OnNextLevelButtonClicked;
            _adRewarded -= OnAddRewarded;
        }

        private void OnUpdgadeButtonClicked()
        {
            VideoAd.Show(AdOpened, _adRewarded, AdClosed, _addErrorOccured);
        }

        private void OnNextLevelButtonClicked()
        {
            InterstitialAd.Show(AdOpened, _interstitialAdClosed, _addErrorOccured);
        }

        private void OnAddRewarded()
        {
            UnityEngine.PlayerPrefs.SetInt(SavesTitles.LaternAngle, _increasedAngle);
        }
    }
}