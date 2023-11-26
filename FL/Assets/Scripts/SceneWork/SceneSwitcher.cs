using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.Scripts.Containers;
using Assets.Scripts.GUI;
using Assets.Scripts.InteractiveObjects.Character;

namespace Assets.Scripts.SceneWork
{
    public class SceneSwitcher : MonoBehaviour
    {
        [SerializeField] private LevelContainer _levelSave;
        [SerializeField] private GamePauser _pauseGame;
        [SerializeField] private GameObject _nextLevelPanel;

        private int _sceneToLoad = 2;
        private int _nextLevelNumber = 1;
        private float _delayBeforDieOfPlayer = 0.5f;

        public static Action LevelStarted;

        private void OnEnable()
        {
            FinishLevelPanel.NextLevelButtonClicked += OnLevelChangedToNext;
            TutorialPanel.LoadMainMenuButtonClicked += OnLoadMainMenu;
            Player.Died += OnPlayerDied;
            LevelButton.Clicked += OnLevelButtonClicked;
        }

        private void OnDisable()
        {
            FinishLevelPanel.NextLevelButtonClicked -= OnLevelChangedToNext;
            TutorialPanel.LoadMainMenuButtonClicked -= OnLoadMainMenu;
            Player.Died -= OnPlayerDied;
            LevelButton.Clicked -= OnLevelButtonClicked;
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(_sceneToLoad);
        }

        private void OnLoadMainMenu()
        {
            if (_sceneToLoad != 1)
            {
                _sceneToLoad = 1;
            }
                
            _levelSave.SetCountOfAvalaibleLevels();
            LoadScene();
        }

        private void LoadCurrenScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnLevelChangedToNext()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + _nextLevelNumber);
            _nextLevelPanel.SetActive(false);
            _levelSave.IncreaseLevel();
        }

        private void OnPlayerDied()
        {
            StartCoroutine(WaitOfPlayersDying());
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
            yield return new WaitForSeconds(_delayBeforDieOfPlayer);
            LoadCurrenScene();
        }
    }
}