using System;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] private float _value;

    public event Action<float> HealthChanged;

    public float Value => _value;

    public void TakeDamage(float healthDamage)
    {
        _value -= healthDamage;
        HealthChanged?.Invoke(_value);
    }

    public void TakeEternalDamage(float damage)
    {
        _value -= damage * Time.deltaTime;
        HealthChanged?.Invoke(_value);
    }
}
