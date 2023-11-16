using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelContainer : MonoBehaviour
{
    public List<int> AvailableLevels;
   
    private int _countOfAvailableLevels;
    private List<int> _allLevels = new List<int>();
    private int _startLevel = 3;

    private void Start()
    {
        FillAllLevels();
        SetCountOfAvalaibleLevels();
        SetAvalaibleLevels();
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
   
    private void FillAllLevels()
    {
        int countOfLevels = 10;
        int currentLevel = 0;

        for (int i = 0; i < countOfLevels; i++)
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
}
