using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Containers;
using Assets.Scripts.DoorSystem;
using Assets.Scripts.SceneWork;
using Assets.Scripts.InteractiveObjects.Character;

namespace Assets.Scripts.GUI
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Slider _health;
        [SerializeField] private Slider _oxygen;
        [SerializeField] private List<IconOfDoor> _iconsOfDoor;
        [SerializeField] private TMP_Text _lightsCount;
        [SerializeField] private LightContainer _lightContainer;
        [SerializeField] private GameObject _finishLevelPanel;
        [SerializeField] private GameObject _tutorialPanel;

        public static Action TutorialPanelClosed;
        public static Action TutorialPanelOpened;
        public static Action FinishPanelOpened;

        private void Start()
        {
            _health.value = _player.Health;
            _oxygen.value = _player.Oxygen;
            _lightsCount.text = _lightContainer.Lights.ToString();
        }

        private void Update()
        {
            _health.value = _player.Health;
        }

        private void OnEnable()
        {
            LevelEnder.LevelEnded += OnLevelEnded;
            LightContainer.LightChanged += OnLightChanged;
            Oxygen.ValueChanged += OnOxygenChanged;
        }

        private void OnDisable()
        {
            LevelEnder.LevelEnded -= OnLevelEnded;
            LightContainer.LightChanged -= OnLightChanged;
            Oxygen.ValueChanged -= OnOxygenChanged;
        }

        public void CloseTutorialPanel()
        {
            _tutorialPanel.SetActive(false);
            TutorialPanelClosed?.Invoke();
        }

        public void OpenTutorialPanel()
        {
            _tutorialPanel.SetActive(true);
            TutorialPanelOpened?.Invoke();
        }

        private void OnLevelEnded()
        {
            _finishLevelPanel.SetActive(true);
            FinishPanelOpened?.Invoke();
        }

        private void OnLightChanged(int lightsOrbs)
        {
            _lightsCount.text = lightsOrbs.ToString();
        }

        private void OnOxygenChanged(float oxygen)
        {
            _oxygen.value = oxygen;
        }
    }
}