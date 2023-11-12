using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class GamePauser : MonoBehaviour
{
    public static bool IsPaused;
   
    [SerializeField] private GameObject _joyStick;
    [SerializeField] AudioSource _music;

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

    public void Resume()
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
