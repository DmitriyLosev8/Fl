using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames.Samples;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class AddShow : MonoBehaviour
{
    private PauseGame _pauseGame;

    private Action _adOpened;
    private Action _adClosed;
    private Action _adRewarded;
    private Action<bool> _interstitialAdClosed;
    private Action<string> _addErrorOccured;

    private void OnEnable()
    {
        UpgradeLightPanel.UpgadeButtonClicked += OnUpdgadeButtonClicked;
         NextLevelPanel.NextLevelButtonClicked += OnNextLevelButtonClicked;
        _adOpened += OnAddOpened;
        _adClosed += OnAddClosed;
        _adRewarded += OnAddRewarded;
    }

    private void OnDisable()
    {
        UpgradeLightPanel.UpgadeButtonClicked -= OnUpdgadeButtonClicked;
         NextLevelPanel.NextLevelButtonClicked -= OnNextLevelButtonClicked;
        _adOpened -= OnAddOpened;
        _adClosed -= OnAddClosed;
        _adRewarded -= OnAddRewarded;
    }

    private void OnUpdgadeButtonClicked()
    {
        VideoAd.Show(_adOpened, _adRewarded, _adClosed, _addErrorOccured);
    }

    private void OnNextLevelButtonClicked()
    {
        InterstitialAd.Show(_adOpened, _interstitialAdClosed, _addErrorOccured); 
    }

    private void OnAddOpened()
    {
        _pauseGame.Pause();
    }

    private void OnAddRewarded()
    {
        int angle = 8;
       
        UnityEngine.PlayerPrefs.SetInt(KeySave.LaternAngle, angle);
    }

    private void OnAddClosed()
    {
        _pauseGame.Resume();
    }
}
