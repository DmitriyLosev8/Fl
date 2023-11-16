using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickEnabler : MonoBehaviour
{
    [SerializeField] private GameObject _joyStick;


    private void Start()
    {
        if (Application.isMobilePlatform)
            EnableJoistick();
    }

    private void EnableJoistick()
    {
        _joyStick.SetActive(true);
    }
}
