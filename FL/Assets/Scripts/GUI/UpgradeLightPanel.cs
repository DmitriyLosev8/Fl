using UnityEngine;
using UnityEngine.UI;
using System;

public class UpgradeLightPanel : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _imageAfterClicking;
    [SerializeField] private GameObject _purchaseText;
    [SerializeField] private GameObject _purchasedText;
    [SerializeField] private GamePauser _pauseGame;

    private int _readyForPurchaseNumber = 0;
    private int _purchasedNumber = 1;

    public static Action UpgadeButtonClicked;

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
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
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
