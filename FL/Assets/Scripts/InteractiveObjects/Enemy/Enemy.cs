using UnityEngine;
using Assets.Scripts.InteractiveObjects.Character;

namespace Assets.Scripts.InteractiveObjects.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private bool _isScullPanelEnemy;
        [SerializeField] private int _oxygenDamageDelimiter = 2;
        [SerializeField] private int _lowDamage = 4;
        [SerializeField] private int _middleDamage = 8;
        [SerializeField] private int _highDamage = 10;
       
        private int _scullPanelDamage = 150;
        private float _healthDamage;
        private float _oxygenDamage;
        private float _lightDamage;
        private int _sendingDamage;
        private int _countOfDamages = 3;
        private int _lowLevels = 2;
        private int _highLevels = 6;


        private void Start()
        {
            ChoseRandomTypeOfDamage();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                SetValueOfDamage(player.Level);

                switch (_sendingDamage)
                {
                    case 0:
                        player.TakeHealthDamage(_healthDamage);
                        break;
                    case 1:
                        player.TakeOxygenDamage(_oxygenDamage);
                        break;
                    case 2:
                        player.TakeLightDamage(_lightDamage);
                        break;
                }
            }
        }

        private void ChoseRandomTypeOfDamage()
        { 
            _sendingDamage = Random.Range(0, _countOfDamages);
        }

        private void SetValueOfDamage(int level)
        {
            if (level <= _lowLevels)
            {
                _healthDamage = _lowDamage;
                _oxygenDamage = _lowDamage / _oxygenDamageDelimiter;
                _lightDamage = _lowDamage;
            }
            else if (level >= _highLevels)
            {
                _healthDamage = _highDamage;
                _oxygenDamage = _highDamage / _oxygenDamageDelimiter;
                _lightDamage = _highDamage;
            }
            else
            {
                _healthDamage = _middleDamage;
                _oxygenDamage = _middleDamage / _oxygenDamageDelimiter;
                _lightDamage = _middleDamage;
            }

            if (_isScullPanelEnemy)
            {
                _healthDamage = _scullPanelDamage;
                _oxygenDamage = _scullPanelDamage;
                _lightDamage = _scullPanelDamage;
            }
        }
    }
}