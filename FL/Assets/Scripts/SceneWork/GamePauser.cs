using UnityEngine;
using Assets.Scripts.GUI;
using Assets.Scripts.Yandex;

namespace Assets.Scripts.SceneWork
{
    public class GamePauser : MonoBehaviour
    {
        public static bool IsPaused;

        [SerializeField] private GameObject _joyStick;
        [SerializeField] private AudioSource _music;

        private void OnEnable()
        {
            Application.focusChanged += OnInBackgroundChange;
            UI.TutorialPanelClosed += Resume;
            UI.TutorialPanelOpened += Pause;
            UI.FinishPanelOpened += Pause;
            AdShow.AdOpened += Pause;
            AdShow.AdClosed += Resume;
            LevelEnder.LevelEnded += Pause;
        }

        private void OnDisable()
        {
            Application.focusChanged -= OnInBackgroundChange;
            UI.TutorialPanelClosed -= Resume;
            UI.TutorialPanelOpened -= Pause;
            UI.FinishPanelOpened -= Pause;
            AdShow.AdOpened -= Pause;
            AdShow.AdClosed -= Resume;
            LevelEnder.LevelEnded -= Pause;
        }

        private void Pause()
        {
            if (_joyStick != null)
            {
                if (Application.isMobilePlatform)
                {
                    _joyStick.SetActive(false);
                }         
            }

            if (_music != null)
            {
                _music.Pause();
            }    

            IsPaused = true;
            Time.timeScale = 0;
        }

        private void Resume()
        {
            if (IsPaused)
            {
                if (_joyStick != null)
                {
                    if (Application.isMobilePlatform)
                    {
                        _joyStick.SetActive(true);
                    }         
                }

                if (_music != null)
                {
                    _music.UnPause();
                }
                    
                IsPaused = false;
                Time.timeScale = 1;
            }
        }

        private void OnInBackgroundChange(bool inBackground)
        {
            bool isOn;

            if (inBackground)
            {
                Resume();
                isOn = false;
            }
            else
            {
                Pause();
                isOn = true;
            }

            AudioListener.pause = isOn;
            AudioListener.volume = isOn ? 0f : 1f;
        }
    }
}