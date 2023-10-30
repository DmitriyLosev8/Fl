using Agava.WebUtility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _levelsPanel;
    [SerializeField] private GameObject _UpgradesPanel;
    //[SerializeField] private PauseGame _pauseGame;

    public static event UnityAction PlayButtonClicked;

    private void OnEnable()
    {
        //Application.focusChanged += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        //Application.focusChanged -= OnInBackgroundChange;
    }



    public void OnPlayButtonClicked()
    {
        _levelsPanel.SetActive(true);
    }

    public void OnCloseButtonClicked()
    {
        _levelsPanel.SetActive(false);
    }
   
    public void OnShopButtonClicked()
    {
        _UpgradesPanel.SetActive(true);
    }

    public void OnCloseShopButtonClicked()
    {
        _UpgradesPanel.SetActive(false);
    }
}