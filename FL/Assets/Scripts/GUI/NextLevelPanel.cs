using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.Events;
using Agava.YandexGames.Samples;
using Agava.YandexGames;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevelPanel : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _lightsCount;
    [SerializeField] private LevelSave _levelSave;

    public static UnityAction NextLevelButtonClicked;
    public static UnityAction Awaked;

    private void Start()
    {
        ShowLightOrbsCount();
        Awaked?.Invoke();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        NextLevelButtonClicked?.Invoke();
    }

    private void ShowLightOrbsCount()
    {
        if (UnityEngine.PlayerPrefs.HasKey(KeySave.Light_Orb))
            _lightsCount.text = UnityEngine.PlayerPrefs.GetInt(KeySave.Light_Orb).ToString();
    }
}
