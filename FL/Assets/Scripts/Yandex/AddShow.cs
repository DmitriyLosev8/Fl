using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames.Samples;
using Agava.YandexGames;
using UnityEngine;
using System;

public class AddShow : MonoBehaviour
{
    public static Action AdOpened;
    public static Action AdClosed;
   
    private Action _adRewarded;
    private Action<bool> _interstitialAdClosed;
    private Action<string> _addErrorOccured;

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
        int angle = 8;
       
        UnityEngine.PlayerPrefs.SetInt(KeySave.LaternAngle, angle);
    }
}
