using UnityEngine;
using Assets.Scripts.AuxiliaryComponents;
using System.Collections.Generic;

namespace Assets.Scripts.GUI
{
    public class LevelsPanel : MonoBehaviour
    {
        [SerializeField] private List<LevelButton> _levelButtons;

        private int _openedLevels;
        private int _mainMenuSceneNumber = 2;

        private void Start()
        {
            ShowAvailableLevels();
        }

        private void ShowAvailableLevels()
        {      
            if (PlayerPrefs.HasKey(SavesTitles.Level))
            {
                _openedLevels = PlayerPrefs.GetInt(SavesTitles.Level);
            }

            for (int i = 0; i < _openedLevels - _mainMenuSceneNumber; i++)
            {
                _levelButtons[i].SetWhiteColor();
            }
        }
    }
}