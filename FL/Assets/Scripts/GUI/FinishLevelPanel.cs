using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FinishLevelPanel : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _lightsCount;
    [SerializeField] private LevelContainer _levelSave;

    public static Action NextLevelButtonClicked;

    private void Start()
    {
        ShowLightOrbsCount();
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
