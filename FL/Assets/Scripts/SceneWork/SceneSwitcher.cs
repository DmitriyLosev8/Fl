using System;
using System.Collections;
using Agava.YandexGames.Samples;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private LevelSave _levelSave;
    [SerializeField] private GamePauser _pauseGame;
    [SerializeField] private GameObject _nextLevelPanel;

    private int _sceneToLoad = 2;

    public static UnityAction LevelStarted;

    private void Start()
    {
        if (_pauseGame != null)
            _pauseGame.Resume();
    }

    private void OnEnable()

    {
        LevelEnder.LevelEnded += OnLevelEnded;
        NextLevelPanel.NextLevelButtonClicked += OnLevelChangedToNext;
        TutorialPanel.LoadMainMenuButtonClicked += OnLoadMainMenu;
        Player.Died += OnPlayerDied;
        LevelButton.Clicked += OnLevelButtonClicked;
    }

    private void OnDisable()
    {
        LevelEnder.LevelEnded -= OnLevelEnded;
        NextLevelPanel.NextLevelButtonClicked -= OnLevelChangedToNext;
        TutorialPanel.LoadMainMenuButtonClicked -= OnLoadMainMenu;
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

    private void OnLoadMainMenu()
    {
        if (_sceneToLoad != 1)
            _sceneToLoad = 1;

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
