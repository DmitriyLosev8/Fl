using System;
using System.Collections;
using Agava.YandexGames.Samples;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private LevelSave _levelSave;
    [SerializeField] private PauseGame _pauseGame;
    [SerializeField] private GameObject _nextLevelPanel;

    private int _sceneToLoad = 1;

    public static UnityAction LevelStarted;

    private void Start()
    {
        if (_pauseGame != null)
            _pauseGame.ResumeGame();
    }

    private void OnEnable()

    {
        LevelChanger.LevelEnded += OnLevelEnded;
        NextLevelPanel.NextLevelButtonClicked += OnLevelChangedToNext;
        TutorialPanel.LoadMainMenuButtonClicked += LoadMainMenu;
        Player.Died += OnPlayerDied;
        LevelButton.Clicked += OnLevelButtonClicked;
    }


    private void OnDisable()
    {
        LevelChanger.LevelEnded -= OnLevelEnded;
        NextLevelPanel.NextLevelButtonClicked -= OnLevelChangedToNext;
        TutorialPanel.LoadMainMenuButtonClicked -= LoadMainMenu;
        Player.Died -= OnPlayerDied;
        LevelButton.Clicked -= OnLevelButtonClicked;
    }

    private void OnLevelEnded()
    {
        _nextLevelPanel.SetActive(true);
        _pauseGame.Pause();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(_sceneToLoad); 
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }

    private void LoadMainMenu()
    {
        if (_sceneToLoad != 0)
            _sceneToLoad = 0;

        _levelSave.SetCountOfAvalaibleLevels();
        LoadScene();
    }

    private void LoadCurrenScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnLevelChangedToNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        _nextLevelPanel.SetActive(false);
        _levelSave.IncreaseLevel();   
    }

    private void OnPlayerDied()
    {
        StartCoroutine(WaitOfPlayersDying()); 
    }

    private void SaveCurrentLevel()
    {
        UnityEngine.PlayerPrefs.SetInt(KeySave.Level, SceneManager.GetActiveScene().buildIndex);
    }

    private void OnLevelButtonClicked(int levelButtonId)
    {
        for (int i = 0; i < _levelSave.AvailableLevels.Count; i++)
        {
            if (levelButtonId == _levelSave.AvailableLevels[i])
            {
                _sceneToLoad = levelButtonId;
                LoadScene();
            }
        }
    }

    private IEnumerator WaitOfPlayersDying()
    {
        float delay = 0.5f;
        yield return new WaitForSeconds(delay);
        LoadCurrenScene();
    }
}
