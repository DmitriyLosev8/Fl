using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.AuxiliaryComponents;
using Assets.Scripts.Containers;

namespace Assets.Scripts.GUI
{
    public class FinishLevelPanel : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _lightsCount;
        [SerializeField] private LevelContainer _levelSave;

        public static Action NextLevelButtonClicked;

        private void Start()
        {
            ShowLightOrbsCount();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            NextLevelButtonClicked?.Invoke();
        }

        private void ShowLightOrbsCount()
        {
            if (PlayerPrefs.HasKey(SavesTitles.LightOrb))
            {
                _lightsCount.text = PlayerPrefs.GetInt(SavesTitles.LightOrb).ToString();
            }
        }
    }
}