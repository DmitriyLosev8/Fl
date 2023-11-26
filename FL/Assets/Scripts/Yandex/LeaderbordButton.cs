using Agava.YandexGames.Samples;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Yandex
{
    public class LeaderbordButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Button _closeButton;
        [SerializeField] private LeaderboardDisplay _leaderbordDisplay;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        }

        private void OnButtonClick()
        {
            _leaderbordDisplay.gameObject.SetActive(true);
        }

        private void OnCloseButtonClick()
        {
            _leaderbordDisplay.gameObject.SetActive(false);
        }
    }
}