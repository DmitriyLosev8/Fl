using UnityEngine;

namespace Assets.Scripts.InteractiveObjects.Character
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _value;

        public float Value => _value;

        public void TakeDamage(float healthDamage)
        {
            _value -= healthDamage;
        }

        public void TakeEternalDamage(float damage)
        {
            _value -= damage * Time.deltaTime;
        }
    }
}