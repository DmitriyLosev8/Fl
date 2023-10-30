using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] private bool _isScullPanelEnemy;

    private float _healthDamage;
    private float _oxygenDamage;
    private float _lightDamage;
    private int _sendingDamage;

    private void Start()
    {
        ChoseRandomDamage();
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

    private void ChoseRandomDamage()
    {
        int countOfDamages = 3;
        _sendingDamage = Random.Range(0, countOfDamages);
    }

    private void SetValueOfDamage(int level)
    {
        int oxygenDamageDelimiter = 2;
        int lowDamage = 4;
        int middleDamage = 8;
        int highDamage = 10;
        int scullPanelDamage = 150;
           
        if (level == 1 || level == 2)
        {
            _healthDamage = lowDamage;
            _oxygenDamage = lowDamage / oxygenDamageDelimiter;
            _lightDamage = lowDamage;
        }

        if (level == 3 || level == 4 || level == 5)
        {
            _healthDamage = middleDamage;
            _oxygenDamage = lowDamage / oxygenDamageDelimiter;
            _lightDamage = middleDamage;
        }

        if (level >= 6)
        {
            _healthDamage = highDamage;
            _oxygenDamage = lowDamage / oxygenDamageDelimiter;
            _lightDamage = highDamage;
        }

        if (_isScullPanelEnemy)
        {
            _healthDamage = scullPanelDamage;
            _oxygenDamage = scullPanelDamage;
            _lightDamage = scullPanelDamage;
        }
    }
}
