using UnityEngine;
using UnityEngine.UI;
using Agava.YandexGames;

namespace Assets.Scripts.Yandex
{
    public class YandexAuthorizationPanel : MonoBehaviour
    {
        [SerializeField] private Button _authorizationButton;
        [SerializeField] private Button _closeAuthorizatButton;

        private void OnEnable()
        {
            _authorizationButton.onClick.AddListener(OnAuthorizationButtonClick);
            _closeAuthorizatButton.onClick.AddListener(OnCloseAuthorizationButtonClick);
        }

        private void OnDisable()
        {
            _authorizationButton.onClick.RemoveListener(OnAuthorizationButtonClick);
            _closeAuthorizatButton.onClick.RemoveListener(OnCloseAuthorizationButtonClick);
        }

        private void OnAuthorizationButtonClick()
        {
            PlayerAccount.Authorize();
            gameObject.SetActive(false);
        }

        private void OnCloseAuthorizationButtonClick()
        {
            gameObject.SetActive(false);
        }
    }
}