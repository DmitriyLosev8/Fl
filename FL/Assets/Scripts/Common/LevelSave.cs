using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSave : MonoBehaviour
{
    private int _countOfAvailableLevels;
    private List<int> _allLevels = new List<int>();
    private int _startLevel = 2;

    public List<int> AvailableLevels;

    private void Start()
    {
        FillAllLevels();
        SetCountOfAvalaibleLevels();
        SetAvalaibleLevels();
    }

    private void FillAllLevels()
    {
        int CountOfLevels = 9;
        int currentLevel = 0;

        for (int i = 0; i < CountOfLevels; i++)
        {
            _allLevels.Add(currentLevel++);
        }
    }

    private void SetAvalaibleLevels()
    {
        for(int i = 0; i < _countOfAvailableLevels; i++)
        {
            AvailableLevels.Add(_allLevels[i]);
        }
    }

    public void SetCountOfAvalaibleLevels()
    {
        if (PlayerPrefs.HasKey(KeySave.Level))
            _countOfAvailableLevels = UnityEngine.PlayerPrefs.GetInt(KeySave.Level);
        else
        {
            _countOfAvailableLevels = _startLevel;
            UnityEngine.PlayerPrefs.SetInt(KeySave.Level, _countOfAvailableLevels);
        }
    }

    public void IncreaseLevel()
    {
        _countOfAvailableLevels++;
        UnityEngine.PlayerPrefs.SetInt(KeySave.Level, _countOfAvailableLevels);
        UnityEngine.PlayerPrefs.Save();
    }
}
