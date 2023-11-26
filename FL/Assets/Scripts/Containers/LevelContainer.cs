using UnityEngine;
using Assets.Scripts.AuxiliaryComponents;
using System.Collections.Generic;

namespace Assets.Scripts.Containers
{
    public class LevelContainer : MonoBehaviour
    {
        public List<int> AvailableLevels;

        private int _countOfAvailableLevels; 
        private int _startLevel = 3;
        private int _countOfLevels = 10;
        private int _currentLevel = 0;
        private List<int> _allLevels = new List<int>();

        private void Start()
        {
            FillAllLevels();
            SetCountOfAvalaibleLevels();
            SetAvalaibleLevels();
        }

        public void SetCountOfAvalaibleLevels()
        {
            if (PlayerPrefs.HasKey(SavesTitles.Level))
            {
                _countOfAvailableLevels = PlayerPrefs.GetInt(SavesTitles.Level);
            }       
            else
            {
                _countOfAvailableLevels = _startLevel;
                PlayerPrefs.SetInt(SavesTitles.Level, _countOfAvailableLevels);
            }
        }

        public void IncreaseLevel()
        {
            _countOfAvailableLevels++;
            PlayerPrefs.SetInt(SavesTitles.Level, _countOfAvailableLevels);
            PlayerPrefs.Save();
        }

        private void FillAllLevels()
        {
            for (int i = 0; i < _countOfLevels; i++)
            {
                _allLevels.Add(_currentLevel++);
            }
        }

        private void SetAvalaibleLevels()
        {
            for (int i = 0; i < _countOfAvailableLevels; i++)
            {
                AvailableLevels.Add(_allLevels[i]);
            }
        }
    }
}