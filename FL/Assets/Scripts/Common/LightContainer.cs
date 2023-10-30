using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using Agava.YandexGames.Samples;
using UnityEngine;
using UnityEngine.Events;

public class LightContainer : MonoBehaviour
{
    private int _lights;
    private int _startLights = 0;

    public int Lights => _lights;

    public static UnityAction<int> LightChanged;

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

    //private void OnLoadSuccess(string cloudLights)
    //{
    //    _lights = JsonUtility.FromJson<int>(cloudLights);
    //    Agava.YandexGames.PlayerPrefs.SetInt(KeySave.Light_Orb, _lights);
    //}

    //public void LoadLights()
    //{
    //    PlayerAccount.GetCloudSaveData(OnLoadSuccess);
    //}
   
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
