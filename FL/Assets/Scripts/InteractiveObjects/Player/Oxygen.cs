using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Oxygen : MonoBehaviour
{
    [SerializeField] private float _value;

    public static event UnityAction<float> valueChanged;

    public float Value => _value;

    public void Losing(float damage)
    {
        _value -= damage * Time.deltaTime;
        valueChanged?.Invoke(_value);
    }

    public void TakeDamage(float damage)
    {
        if (_value > 0)
        {
            _value -= damage;
            valueChanged?.Invoke(_value);
        }

        if (_value < 0)
            _value = 0;
    }

    public void RaiseValue(float value)
    {
        int maxOxygen = 100;

        if (_value < maxOxygen)
        {
            _value += value;
            valueChanged?.Invoke(_value);
        }
    }
}
