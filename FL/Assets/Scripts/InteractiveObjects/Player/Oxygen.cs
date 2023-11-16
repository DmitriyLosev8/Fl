using System;
using UnityEngine;


public class Oxygen : MonoBehaviour
{
    [SerializeField] private float _value;

    public static event Action<float> ValueChanged;

    public float Value => _value;

    public void Losing(float damage)
    {
        _value -= damage * Time.deltaTime;
        ValueChanged?.Invoke(_value);
    }

    public void TakeDamage(float damage)
    {
        if (_value > 0)
        {
            _value -= damage;
            ValueChanged?.Invoke(_value);
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
            ValueChanged?.Invoke(_value);
        }
    }
}
