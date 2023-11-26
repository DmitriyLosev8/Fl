using System;
using UnityEngine;

namespace Assets.Scripts.InteractiveObjects.Character
{
    public class Oxygen : MonoBehaviour
    {
        [SerializeField] private float _value;

        private int _maxOxygen = 100;

        public static event Action<float> ValueChanged;

        public float Value => _value;

        public void TakeEternalDamage(float damage)
        {
            _value -= damage * Time.deltaTime;
            ValueChanged?.Invoke(_value);
        }

        public void TakeDamage(float damage)
        {
            if (_value <= 0)
            {
                _value = 0;
            }
            else
            {
                _value -= damage;
                ValueChanged?.Invoke(_value);
            }
        }

        public void RaiseValue(float value)
        {
            if (_value < _maxOxygen)
            {
                _value += value;
                ValueChanged?.Invoke(_value);
            }
        }
    }
}