using UnityEngine;


public class GamePauser : MonoBehaviour
{
    public static bool IsPaused;
   
    [SerializeField] private GameObject _joyStick;
    [SerializeField] AudioSource _music;

    private void OnEnable()
    {
        Application.focusChanged += OnInBackgroundChange;
        GUI.TutorialPanelClosed += Resume;
        GUI.TutorialPanelOpened += Pause;
        GUI.FinishPanelOpened += Pause;
        AddShow.AdOpened += Pause;
        AddShow.AdClosed += Resume;
        LevelEnder.LevelEnded +=Pause;
    }

    private void OnDisable()
    {
        Application.focusChanged -= OnInBackgroundChange;
        GUI.TutorialPanelClosed -= Resume;
        GUI.TutorialPanelOpened -= Pause;
        GUI.FinishPanelOpened -= Pause;
        AddShow.AdOpened -= Pause;
        AddShow.AdClosed -= Resume;
        LevelEnder.LevelEnded -= Pause;
    }

    private void Pause()
    {
      if(_joyStick != null)
       {
          if (Application.isMobilePlatform)
             _joyStick.SetActive(false);
       }
           
       if(_music != null)
            _music.Pause();

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
                    _joyStick.SetActive(true);
            }

            if (_music != null)
                _music.UnPause();

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
