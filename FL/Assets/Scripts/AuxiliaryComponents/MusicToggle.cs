using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.AuxiliaryComponents
{
    public class MusicToggle : MonoBehaviour
    {
        [SerializeField] private AudioSource _music;
        [SerializeField] private Toggle _musicToggle;

        private int  _turnedOn = 1;
        private int _turnedOff = 0;

        private void Start()
        {
            SwitchMusic();
        }

        private void OnEnable()
        {
            _musicToggle.onValueChanged.AddListener(OnSetMusicValue);
        }

        private void OnDisable()
        {
            _musicToggle.onValueChanged.RemoveListener(OnSetMusicValue);
        }

        private void OnSetMusicValue(bool isOn)
        {
            if (isOn)
            {
                PlayerPrefs.SetInt(SavesTitles.Music, _turnedOn);
            }
            else
            {
                PlayerPrefs.SetInt(SavesTitles.Music, _turnedOff);
            }         
           
            SwitchMusic();
        }

        private void SwitchMusic()
        {          
            if (PlayerPrefs.HasKey(SavesTitles.Music))
            {
                if (PlayerPrefs.GetInt(SavesTitles.Music) == _turnedOn)
                {
                    TurnMusicOn();
                }
                else
                {
                    TurnMusicOff();
                }
            }
        }

        private void TurnMusicOn()
        {
            _music.Play();
            _musicToggle.isOn = true;
        }

        private void TurnMusicOff()
        {
            _music.Stop();
            _musicToggle.isOn = false;
        }
    }
}