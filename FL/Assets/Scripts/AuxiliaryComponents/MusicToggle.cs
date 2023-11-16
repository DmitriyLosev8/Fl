using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    [SerializeField] private AudioSource _music;
    [SerializeField] private Toggle _musicToggle;

    private void Start()
    {
        TurnSound();
    }

    private void OnEnable()
    {
        _musicToggle.onValueChanged.AddListener(OnSetMusicValue);
    }

    private void OnDisable()
    {
        _musicToggle.onValueChanged.RemoveListener(OnSetMusicValue);
    }

    private void OnSetMusicValue(bool isOn)
    {
        int turnedOn = 1;
        int turnedOff = 0;

        if (isOn)
            UnityEngine.PlayerPrefs.SetInt(KeySave.Music, turnedOn);
        else
            UnityEngine.PlayerPrefs.SetInt(KeySave.Music, turnedOff);

        TurnSound();
    }


    private void TurnSound()
    {
        int turnedOn = 1;

        if (UnityEngine.PlayerPrefs.HasKey(KeySave.Music))
        {
            if (UnityEngine.PlayerPrefs.GetInt(KeySave.Music) == turnedOn)
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
}
