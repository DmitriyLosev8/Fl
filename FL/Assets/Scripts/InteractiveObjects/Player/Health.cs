using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _value;

    public event UnityAction<float> HealthChanged;

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
