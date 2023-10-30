using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsPanel : MonoBehaviour
{
    [SerializeField] private List<LevelButton> _levelButtons;

    private int _openedLevels;

    private void Start()
    {
        Paint();
    }

    private void Paint()
    {
        int mainMenu = 1;

        if (UnityEngine.PlayerPrefs.HasKey(KeySave.Level))
        {
            _openedLevels = UnityEngine.PlayerPrefs.GetInt(KeySave.Level);  
        }
            
       for(int i = 0; i < _openedLevels - mainMenu; i++)
        {
            _levelButtons[i].SetWhiteColor();
        }
    }
}

