using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScullPanel : MonoBehaviour
{
    [SerializeField] private Light _leftEye;
    [SerializeField] private Light _rightEye;
    [SerializeField] private GameObject _deadlyEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            TurnLightOn();
            _deadlyEnemy.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            TurnLightOff();
            _deadlyEnemy.SetActive(false);
        }
    }

    private void TurnLightOn()
    {
        int valueOfLightOn = 30;
        _leftEye.intensity = valueOfLightOn;
        _rightEye.intensity = valueOfLightOn;
    }

    private void TurnLightOff()
    {
        int valueOfLightOff = 0;
        _leftEye.intensity = valueOfLightOff;
        _rightEye.intensity = valueOfLightOff;
    }
}
