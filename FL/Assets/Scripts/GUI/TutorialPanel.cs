using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.GUI
{
    public class TutorialPanel : MonoBehaviour
    {
        public static UnityAction LoadMainMenuButtonClicked;

        public void OnLoadMainMenuButtonClicked()
        {
            Time.timeScale = 1;
            LoadMainMenuButtonClicked?.Invoke();
        }
    }
}