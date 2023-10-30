using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames.Samples;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using TMPro;

public class UpgradeLightPanel : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _imageAfterClicking;
    [SerializeField] private GameObject _purchaseText;
    [SerializeField] private GameObject _purchasedText;
    [SerializeField] private PauseGame _pauseGame;

    private int _readyForPurchaseNumber = 0;
    private int _purchasedNumber = 1;

    public static UnityAction UpgadeButtonClicked;

    private void Start()
    {
        if (UnityEngine.PlayerPrefs.HasKey(KeySave.UpgradeButton))
        {
            if (UnityEngine.PlayerPrefs.GetInt(KeySave.UpgradeButton) != _readyForPurchaseNumber)
                SetDisabled();
        }
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
        Application.focusChanged += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
        Application.focusChanged -= OnInBackgroundChange;
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        bool isOn;

        if (inBackground)
        {
             _pauseGame.ResumeGame();
            isOn = false;
        }
        else
        {
              _pauseGame.Pause();
            isOn = true;
        }

        AudioListener.pause = isOn;
        AudioListener.volume = isOn ? 0f : 1f;
    }

    private void OnButtonClick()
    {
        SetDisabled();
        UnityEngine.PlayerPrefs.SetInt(KeySave.UpgradeButton, _purchasedNumber);
        UpgadeButtonClicked?.Invoke();
    }

    private void SetDisabled()
    {
        _button.image.sprite = _imageAfterClicking;
        _purchaseText.SetActive(false);
        _purchasedText.SetActive(true);
        _button.enabled = false;
    }
}
