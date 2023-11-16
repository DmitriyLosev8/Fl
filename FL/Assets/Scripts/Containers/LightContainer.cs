using System;
using UnityEngine;

public class LightContainer : MonoBehaviour
{
    private int _lights;
    private int _startLights = 0;

    public static Action<int> LightChanged;
   
    public int Lights => _lights;
   
    private void Start()
    {
        if (UnityEngine.PlayerPrefs.HasKey(KeySave.Light_Orb))
            _lights = UnityEngine.PlayerPrefs.GetInt(KeySave.Light_Orb);
        else
        {
            _lights = _startLights;
            UnityEngine.PlayerPrefs.SetInt(KeySave.Light_Orb, _lights);
            UnityEngine.PlayerPrefs.Save();
        }

        LightChanged?.Invoke(_lights);
    }

    public void ApplyLights(int light)
    {
        _lights += light;
        LightChanged?.Invoke(_lights);
        UnityEngine.PlayerPrefs.SetInt(KeySave.Light_Orb, _lights);
        UnityEngine.PlayerPrefs.Save();
    }

    public void LoseLights(int light)
    {
        _lights -= light;
        LightChanged?.Invoke(_lights);
    }
}
