using System;
using UnityEngine;
using Assets.Scripts.AuxiliaryComponents;

namespace Assets.Scripts.Containers
{
    public class LightContainer : MonoBehaviour
    {
        private int _lights;
        private int _startLights = 0;

        public static Action<int> LightChanged;

        public int Lights => _lights;

        private void Start()
        {
            if (PlayerPrefs.HasKey(SavesTitles.LightOrb))
            {
                _lights = PlayerPrefs.GetInt(SavesTitles.LightOrb);
            }       
            else
            {
                _lights = _startLights;
                PlayerPrefs.SetInt(SavesTitles.LightOrb, _lights);
                PlayerPrefs.Save();
            }

            LightChanged?.Invoke(_lights);
        }

        public void ApplyLights(int light)
        {
            _lights += light;
            LightChanged?.Invoke(_lights);
            PlayerPrefs.SetInt(SavesTitles.LightOrb, _lights);
            PlayerPrefs.Save();
        }

        public void LoseLights(int light)
        {
            _lights -= light;
            LightChanged?.Invoke(_lights);
        }
    }
}