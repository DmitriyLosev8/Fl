using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject _joyStick;
    [SerializeField] AudioSource _music;

    public static bool IsPaused;

    public void Pause()
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

    public void ResumeGame()
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
}
