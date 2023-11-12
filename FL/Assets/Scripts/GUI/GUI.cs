using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GUI : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _health;
    [SerializeField] private Slider _oxygen;
    [SerializeField] private List<IconOfDoor> _iconsOfDoor;
    [SerializeField] private AudioSource _music;
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private TMP_Text _lightsCount;
    [SerializeField] private LightContainer _lightContainer;
    [SerializeField] private GameObject _joyStick;
    [SerializeField] private GamePauser _pauseGame;
    [SerializeField] private GameObject _nextLevelPanel;
    [SerializeField] private GameObject _tutorialPanel;

    private void Start()
    {
        _health.value = _player.Health;
        _oxygen.value = _player.Oxygen;
        _lightsCount.text = _lightContainer.Lights.ToString();
        
        if (Application.isMobilePlatform)
            EnableJoistick();

        TurnSound();
    }

    private void Update()
    {
        _health.value = _player.Health;
    }

    private void OnEnable()
    {
        Application.focusChanged += OnInBackgroundChange;
        LightContainer.LightChanged += OnLightChanged;
        Oxygen.valueChanged += OnOxygenChanged;
        _musicToggle.onValueChanged.AddListener(OnSetMusicValue);
    }

    private void OnDisable()
    {
        Application.focusChanged -= OnInBackgroundChange;
        LightContainer.LightChanged -= OnLightChanged;
        Oxygen.valueChanged -= OnOxygenChanged;
        _musicToggle.onValueChanged.RemoveListener(OnSetMusicValue);
    }

    public void CloseTutorialPanel()
    {
        _tutorialPanel.SetActive(false);
        _pauseGame.Resume();
    }

    public void OnSetMusicValue(bool isOn)
    {
        int turnedOn = 1;
        int turnedOff = 0;

        if (isOn)
            UnityEngine.PlayerPrefs.SetInt(KeySave.Music, turnedOn);
        else
            UnityEngine.PlayerPrefs.SetInt(KeySave.Music, turnedOff);

        TurnSound();
    }

    public void OpenTutorialPanel()
    {
        _tutorialPanel.SetActive(true);
        _pauseGame.Pause();
    }

    private void OnLevelEnded()
    {
        _nextLevelPanel.SetActive(true);
        _pauseGame.Pause();
    }

    private void EnableJoistick()
    {
        _joyStick.SetActive(true);
    }

    private void OnLightChanged(int lightsOrbs)
    {
        _lightsCount.text = lightsOrbs.ToString();
    }

    private void OnOxygenChanged(float oxygen)
    {
        _oxygen.value = oxygen;
    }

    private void TurnSound()
    {
        int turnedOn = 1;

        if (UnityEngine.PlayerPrefs.HasKey(KeySave.Music))
        {
            if(UnityEngine.PlayerPrefs.GetInt(KeySave.Music) == turnedOn)
            {
                _music.Play();
                _musicToggle.isOn = true;
            }
            else
            {
                _music.Stop();
                _musicToggle.isOn = false;
            }        
        }
    }

    private void OnInBackgroundChange(bool inBackground) 
    {
        bool isOn;

        if (inBackground)
        {
            _pauseGame.Resume();
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
}
